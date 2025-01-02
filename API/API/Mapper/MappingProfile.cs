using API.Contracts;
using API.Entities;
using AutoMapper;

namespace API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
