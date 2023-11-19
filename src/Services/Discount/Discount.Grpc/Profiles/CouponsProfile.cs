using AutoMapper;
using Discount.Grpc.Dtos;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Catalog.API.Profiles {
    public class CouponsProfile : Profile {

        public CouponsProfile() {
            // .ReverseMap() adds bidirectional mapping between classes
            CreateMap<Coupon, CouponReadDto>().ReverseMap();
            CreateMap<CouponCreateDto, Coupon>().ReverseMap();
            CreateMap<CouponUpdateDto, Coupon>().ReverseMap();

            // Map Coupon POCO into a gRPC response
            CreateMap<Coupon, CouponResponse>().ReverseMap();
        }
    }
}
