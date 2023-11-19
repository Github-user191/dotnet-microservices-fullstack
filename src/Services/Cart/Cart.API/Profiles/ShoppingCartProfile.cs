using AutoMapper;
using Cart.API.Dtos;
using Cart.API.Entities;

namespace Cart.API.Profiles {
    public class ShoppingCartProfile : Profile {
        
        public ShoppingCartProfile() {
            // When we want to convert a DTO to a model
            CreateMap<ShoppingCart, ShoppingCartUpdateDto>().ReverseMap();;
            CreateMap<ShoppingCart, ShoppingCartReadDto>().ReverseMap();;
            // When we want to convert a model to an update DTO
            CreateMap<ShoppingCartUpdateDto, ShoppingCart>().ReverseMap();;

        }
    }
}