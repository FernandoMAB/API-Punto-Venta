using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using Microsoft.EntityFrameworkCore;

namespace API_Punto_Venta.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly PuntoVentaContext _context;
        private readonly ILogger<FacturaService> _logger;

        public FacturaService(PuntoVentaContext dbcontext, ILogger<FacturaService> logger)
        {
            _context = dbcontext;
            _logger = logger;
        }

        public IEnumerable<Factura> GetAll()
        {
            _logger.LogDebug("Start Factura Service get all");
            if (_context.Facturas.Any())
                return _context.Facturas.Include(x => x.FacturaDetalles).ThenInclude(x => x.Pros);
            else throw new NotFoundException(Constants.NONREGIST);
        }

        public Factura GetFactura(int id)
        {
            if(_context.Facturas.Find(id) is { } factura)
            {
                ICollection<FacturaDetalle> facturasDetalles = _context.FacturaDetalles.Where(x => x.FacId == factura.FacId).ToList();
                foreach(FacturaDetalle factelement in facturasDetalles)
                {
                    ICollection<Producto> productos = new List<Producto>();
                    Producto prod = _context.Productos.First(x => x.ProId == factelement.ProId);
                    if (prod is { } producto)
                    {
                        productos.Add(producto);
                        factelement.Pros = productos;
                    }
                }
                factura.FacturaDetalles = facturasDetalles;
                return factura;
            }
            throw new NotFoundException(Constants.NONFACT);
        }

        public async Task<IResult> Save(Factura factura)
        {
            //Parametros
            var valIva = Convert.ToDouble(_context.Parametrosgs.First(x => x.ParNemonico == "IVAV").ParValor);
            ICollection<FacturaDetalle> facturaDet = new List<FacturaDetalle>();

            //Crear el secuencial de la factura
            if (!_context.Facturas.Any())
                factura.FacId = 1;
            else
                factura.FacId = _context.Facturas.Max(x => x.FacId) + 1;

            Factura facturaToKardex = factura.Copy();

            ICollection<FacturaDetalle> facturaDetalles = factura.FacturaDetalles;
            if (facturaDetalles.Any())
            {
                double subtotal = 0;
                double iva = 0;
                double descuento = 0;
                double total = 0;
                int idFacdetalle;

                if (!_context.FacturaDetalles.Any())
                    idFacdetalle = 1;
                else
                    idFacdetalle = _context.FacturaDetalles.Max(x => x.FadId);

                foreach (FacturaDetalle factelement in facturaDetalles)
                {

                    var productos = factelement.Pros;
                    if (productos.Any())
                    {
                        foreach(Producto producto in productos)
                        {
                            var actualPro = _context.Productos.Find(producto.ProId);
                            if(actualPro != null)
                            {
                                factelement.FadPrecioUnit = 
                                    (double)actualPro.ProPrecio;
                                factelement.FadCantidad = producto.ProCantidad;

                                var venta = factelement.FadPrecioUnit * factelement.FadCantidad;
                                //Descuento
                                factelement.FadDescuento = 
                                    (double)(actualPro.ProDescuento * venta);

                                descuento = 
                                    (double)(descuento + factelement.FadDescuento); 
                                //Subtotal
                                factelement.FadSubtotal = 
                                    (venta - factelement.FadDescuento)/(1+(valIva/100));

                                subtotal = 
                                    (double)(subtotal + factelement.FadSubtotal);
                                //IVA
                                factelement.FadIva = 
                                    (double)(factelement.FadSubtotal * (valIva/100));

                                iva = (double)(iva + factelement.FadIva);
                                //TOTAL
                                factelement.FadTotal = 
                                    (double)(factelement.FadSubtotal + factelement.FadIva);
                                total = (double)(total + factelement.FadTotal);

                                //redondear
                                factelement.FadDescuento = Math.Round((double)factelement.FadDescuento, Constants.ROUND_VAL);
                                factelement.FadSubtotal = Math.Round((double)factelement.FadSubtotal, Constants.ROUND_VAL);
                                factelement.FadIva = Math.Round((double)factelement.FadIva, Constants.ROUND_VAL);
                                factelement.FadTotal = Math.Round((double)factelement.FadTotal, Constants.ROUND_VAL);
                                //fin redondear


                                factelement.FadFecha = DateTime.Now;
                                factelement.FacId = factura.FacId;
                                ICollection<Producto>? pros = new List<Producto>
                                {
                                    actualPro.Copy()
                                };
                                factelement.Pros = null;
                                factelement.ProId = actualPro.ProId;

                                idFacdetalle++;
                                factelement.FadId = idFacdetalle;

                                facturaDet.Add(item: factelement.Copy());
                                _context.Entry(actualPro).State = EntityState.Detached;
                            }
                            else
                            {
                                throw new BusinessException(Constants.NONPROD);
                            }
                        }
                    }
                    else
                    {
                        throw new BusinessException(@"No existen productos");
                    }

                }
                factura.FacDescuen = Math.Round(descuento, Constants.ROUND_VAL);
                factura.FacSubtotal = Math.Round(subtotal, Constants.ROUND_VAL);
                factura.FacIva = Math.Round(iva, Constants.ROUND_VAL);
                factura.FacTotal = Math.Round(total, Constants.ROUND_VAL);
                factura.FacturaDetalles = facturaDet;

                _context.Facturas.Add(factura);
                //Chequear el Stock de los productos
                await BajarStock(facturaToKardex);

                await _context.SaveChangesAsync();
                return Results.Created($"{factura.FacId}", factura);
            }
            throw new BusinessException(Constants.MULTIPLENOTFOUND);
        }

        public async Task BajarStock (Factura factura)
        {
            int karId = 0;
            if (!_context.Kardices.Any())
                karId = 1;
            else
                karId = _context.Kardices.Max(x => x.KarId);
            if (factura.FacturaDetalles is ICollection<FacturaDetalle> facturasDetalles)
            {
                foreach(FacturaDetalle factelement in facturasDetalles)
                {
                    ICollection<Producto>? productos = factelement.Pros; 
                    foreach (Producto prodActual in productos)
                    {
                        if (prodActual.ProId != null
                            && prodActual.ProId != 0
                            && prodActual.ProCantidad != null
                            && prodActual.ProCantidad != 0)
                        {
                            var prodToUpdate = _context.Productos.Find(prodActual.ProId);
                            prodToUpdate.ProStock -= prodActual.ProCantidad;
                            karId++;
                            _context.Kardices.Add(ProcesoVentaKardex(prodToUpdate, karId, factura.FacId, (int)prodActual.ProCantidad));
                            if (prodToUpdate.ProStock < 0)
                            {
                                throw new BusinessException(Constants.NOTSTOCK + prodToUpdate.ProNombre);
                            }
                        }
                        else
                        {
                            throw new BusinessException(Constants.NOTCANT);
                        }    
                    }

                }
                await _context.SaveChangesAsync();
            }
        }

        private Kardex ProcesoVentaKardex(Producto producto, int karId, int facId, int cantidad)
        {
            Kardex kardex = _context.Kardices.Where(x => x.ProId == producto.ProId).OrderByDescending(x => x.KarId).First();
            double karSalPrecioTotal = 0;
            karSalPrecioTotal = Math.Round((double)(kardex.KarBalPrecio * cantidad), Constants.ROUND_VAL);


            return new Kardex()
            {
                KarId = karId,
                KarFecha = DateTime.Now,
                KarDetalle = producto.ProDetalle,
                KarSalCantidad = cantidad,
                KarEntCantidad = 0,
                KarSalPrecio = kardex.KarBalPrecio,
                KarEntPrecio = 0,
                KarEntPreTotal = 0,
                KarSalPreTotal = karSalPrecioTotal,
                KarBalCantidad = kardex.KarBalCantidad - cantidad,
                KarBalPrecioTotal = kardex.KarBalPrecioTotal - karSalPrecioTotal,
                KarBalPrecio = kardex.KarBalPrecio,
                KarEstado = "V",
                ProId = producto.ProId,
                FacId = facId
            };
        }
    }

    public interface IFacturaService
    {
        IEnumerable<Factura> GetAll();
        Factura GetFactura(int id);
        Task<IResult> Save(Factura factura);
        Task BajarStock(Factura factura);
    }
}
