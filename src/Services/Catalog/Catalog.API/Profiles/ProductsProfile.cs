using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Entities;

namespace Catalog.API.Profiles {
    public class ProductsProfile : Profile {

        public ProductsProfile() {
            CreateMap<Product, ProductReadDto>().ReverseMap();;
            CreateMap<ProductCreateDto, Product>().ReverseMap();;
            CreateMap<ProductUpdateDto, Product>().ReverseMap();;
        }
    }
}
