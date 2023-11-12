using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Entities;

namespace Catalog.API.Profiles {
    public class ProductsProfile : Profile {

        public ProductsProfile() {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
