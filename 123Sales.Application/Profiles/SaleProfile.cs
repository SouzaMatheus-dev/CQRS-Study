using _123Sales.Application.Dtos;
using _123Sales.Domain.Entities;
using AutoMapper;

namespace _123Sales.Application.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

            CreateMap<SaleDto, Sale>();
        }
    }
}