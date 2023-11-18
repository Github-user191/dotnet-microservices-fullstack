using AutoMapper;
using Discount.API.Dtos;
using Discount.API.Entities;

namespace Catalog.API.Profiles {
    public class CouponsProfile : Profile {

        public CouponsProfile() {
            // .ReverseMap() adds bidirectional mapping between classes
            CreateMap<Coupon, CouponReadDto>().ReverseMap();
            CreateMap<CouponCreateDto, Coupon>().ReverseMap();
            CreateMap<CouponUpdateDto, Coupon>().ReverseMap();
        }
    }
}
