using API_Punto_Venta.Dtos;
using API_Punto_Venta.Models;
using AutoMapper;

namespace API_Punto_Venta.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Kardex, KardexDto>().ReverseMap();
            CreateMap<Producto, ProductDto>().ReverseMap();
            CreateMap<Statistic, DashBoard>()
                .ForMember(dest => dest.Int1,
                    opt => opt.MapFrom(s => s.EstYear))
                .ForMember(dest => dest.String1,
                    opt => opt.MapFrom(s => s.EstMonth))
                .ForMember(dest => dest.Propiedad,
                    opt => opt.MapFrom(s => s.EstWeek+ "(" + s.EstYear + ")"))
                .ForMember(dest => dest.Valor,
                    opt => opt.MapFrom(s => s.EstTotalSold));
        }
    }
}
