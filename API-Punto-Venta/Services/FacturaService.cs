using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace API_Punto_Venta.Services
{
    public class FacturaService : IFacturaService
    {
        PuntoVentaContext context;
        private readonly ILogger<FacturaService> logger;

        public FacturaService(PuntoVentaContext dbcontext, ILogger<FacturaService> logger)
        {
            context = dbcontext;
            this.logger = logger;
        }

        public IEnumerable<Factura> GetAll()
        {
            if (context.Facturas.Any())
                return context.Facturas.Include(x => x.FacturaDetalles).ThenInclude(x => x.Pros);
            else throw new NotFoundException(Constants.NONREGIST);
        }

        public Factura? GetFactura(int id)
        {
            return context.Facturas.Find(id)
                    is Factura factura
                        ? factura
                        : throw new NotFoundException(Constants.NONFACT);
        }

        public async Task<IResult> Save(Factura factura)
        {
        //Parametros
        var valIva = Convert.ToDouble(context.Parametrosgs.First(x => x.ParNemonico == "IVAV").ParValor);
            ICollection<FacturaDetalle> facturaDet = new List<FacturaDetalle>();

            //Crear el secuencial de la factura
            if (!context.Facturas.Any())
                factura.FacId = 1;
            else
                factura.FacId = context.Facturas.Max(x => x.FacId) + 1;

            ICollection<FacturaDetalle> facturaDetalles = factura.FacturaDetalles;
            if (facturaDetalles.Any())
            {
                double subtotal = 0;
                double iva = 0;
                double descuento = 0;
                double total = 0;
                int idFacdetalle = 0;

                if (!context.FacturaDetalles.Any())
                    idFacdetalle = 1;
                else
                    idFacdetalle = context.FacturaDetalles.Max(x => x.FadId);

                foreach (FacturaDetalle factelement in facturaDetalles)
                {

                    var Productos = factelement.Pros;
                    if (Productos.Any())
                    {
                        foreach(Producto producto in Productos)
                        {
                            var actualPro = context.Productos.Find(producto.ProId);
                            if(actualPro != null)
                            {
                                //Stock
                                actualPro.ProStock = actualPro.ProStock - producto.ProCantidad;
                                if(actualPro.ProStock < 0)
                                {
                                    throw new BusinessException(Constants.NOTSTOCK + actualPro.ProNombre);
                                }

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
                                ICollection<Producto>? Pros = new List<Producto>
                                {
                                    actualPro.Copy()
                                };
                                factelement.Pros = Pros;

                                idFacdetalle = idFacdetalle + 1;
                                factelement.FadId = idFacdetalle;

                                facturaDet.Add(item: factelement.Copy());
                                context.Entry(actualPro).State = EntityState.Detached;
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

                context.Facturas.Add(factura);
                await context.SaveChangesAsync();
                return Results.Created($"{factura.FacId}", factura);
            }
            throw new BusinessException(Constants.MULTIPLENOTFOUND);
        }

    }

    public interface IFacturaService
    {
        IEnumerable<Factura> GetAll();
        Task<IResult> Save(Factura factura);
    }
}
