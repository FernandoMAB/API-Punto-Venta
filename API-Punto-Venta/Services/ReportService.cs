using API_Punto_Venta.Context;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using AutoMapper;

namespace API_Punto_Venta.Services
{
    public class ReportService : IReportService
    {
        private readonly PuntoVentaContext _context;
        private readonly IMapper _mapper;

        public ReportService(PuntoVentaContext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }

        public IEnumerable<DashBoard> Get(DateTime startDate, DateTime endDate)
        {
            var endDateExclusive = endDate.AddDays(1);
            ICollection<DashBoard> reportes = new List<DashBoard>();
            var productos = _context.Productos.ToList();
            foreach (Producto actualProd in productos)
            {
                var facturas = _context.FacturaDetalles.Where(x => x.ProId == actualProd.ProId)
                    .Where(x => x.FadFecha >= startDate && x.FadFecha < endDateExclusive)
                    .Sum(x => x.FadCantidad);
                var total = _context.FacturaDetalles.Where(x => x.ProId == actualProd.ProId)
                    .Where(x => x.FadFecha >= startDate && x.FadFecha < endDateExclusive)
                    .Sum(x => x.FadTotal);
                if (facturas > 0)
                {
                    reportes.Add(new DashBoard()
                    {
                        Valor = facturas,
                        Int1 = actualProd.ProId,
                        Propiedad = actualProd.ProNombre,
                        Double1 = actualProd.ProPrecio,
                        Double2 = total
                    });
                }
            }
            return reportes.OrderBy(x => x.Int1);
        }

        public IEnumerable<DashBoard> GetStatistics()
        {
            if (!_context.Statistics.Any()) throw new NotFoundException(Constants.NONREGIST);
            var statistics = _context.Statistics.ToList();

            return statistics.Select(item => _mapper.Map<DashBoard>(item)).OrderBy(x => x.Int1)
                .ThenBy(x => x.Propiedad).ToList();
        }

        public IEnumerable<DashBoard> GetSalesStatistics(DateTime startDate, DateTime endDate)
        {
            var endDateExclusive = endDate.AddDays(1);
            var productos = _context.Productos.ToList();
            ICollection<DashBoard> reportes = new List<DashBoard>();

            foreach (var product in productos)
            {
                var productSold = _context.Kardices.Where(x => x.ProId == product.ProId)
                    .Where(x => x.KarFecha >= startDate && x.KarFecha < endDateExclusive)
                    .Sum(x => x.KarEntCantidad);
                if (productSold > 0)
                {
                    reportes.Add(new DashBoard()
                    {
                        Int1 = product.ProId,
                        Propiedad = product.ProNombre,
                        String1 = product.ProCodBarras,
                        Int2 = product.ProStock,
                        Double1 = product.ProPrecio,
                        Valor = productSold
                    });
                }
            }

            return reportes.OrderBy(x => x.Int1);
        }
    }

    public interface IReportService
    {
        IEnumerable<DashBoard> Get(DateTime startDate, DateTime endDate);
        IEnumerable<DashBoard> GetStatistics();
        IEnumerable<DashBoard> GetSalesStatistics(DateTime startDate, DateTime endDate);
    }
}
