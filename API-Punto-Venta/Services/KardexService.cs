using API_Punto_Venta.Context;
using API_Punto_Venta.Dtos;
using API_Punto_Venta.Exceptions;
using API_Punto_Venta.Models;
using API_Punto_Venta.Util;
using AutoMapper;

namespace API_Punto_Venta.Services
{
    public class KardexService : IKardexService
    {
        private readonly PuntoVentaContext _context;
        private readonly ILogger<KardexService> _logger;
        private readonly IMapper _mapper;

        public KardexService(PuntoVentaContext dbcontext, ILogger<KardexService> logger, IMapper mapper)
        {
            _context = dbcontext;
            _logger = logger;
            _mapper = mapper;
        }

        public ICollection<KardexDto> GetKardex(int id)
        {
            _logger.LogDebug("Start service get kardex by product id");
            if (_context.Kardices.Where(x => x.ProId == id).ToList() is ICollection<Kardex> kardex)
            {

                return kardex.Select(item => _mapper.Map<KardexDto>(item)).ToList();
            }
            throw new NotFoundException(Constants.NONKARD);
        }
    }

    public interface IKardexService
    {
        ICollection<KardexDto>? GetKardex(int id);
    }
}
