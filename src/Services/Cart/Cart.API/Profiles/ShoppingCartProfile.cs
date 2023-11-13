using AutoMapper;
using Cart.API.Dtos;
using Cart.API.Entities;

namespace Cart.API.Profiles {
    public class ShoppingCartProfile : Profile {
        
        public ShoppingCartProfile() {
            CreateMap<ShoppingCart, ShoppingCartReadDto>();
        }
    }
}