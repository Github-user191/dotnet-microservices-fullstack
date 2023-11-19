using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services {
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase {

        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponResponse> GetDiscount(GetDiscountRequest request, ServerCallContext context) {

            var coupon = await _repository.GetDiscount(request.ProductName);
            
            if(coupon == null) {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName = {request.ProductName} does not exist"));
            }

            _logger.LogInformation("Discount retrieved for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            // Map Coupon entity into a CouponResponse for the gRPC service schema
            var couponResponse = _mapper.Map<Coupon, CouponResponse>(coupon);
            return couponResponse;
        }

        public override async Task<CouponResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context) {
            // Map from CouponResponse to a Coupon model to call repository methods
            var coupon = _mapper.Map<CouponResponse, Coupon>(request.Coupon);

            await _repository.CreateDiscount(coupon);
            _logger.LogInformation("Discount is created successfully. ProductName: {ProductName}", coupon.ProductName);

            // Map back from a Coupon model to a CouponResponse to return for the gRPC method
            var couponResponse = _mapper.Map<Coupon, CouponResponse>(coupon);

            return couponResponse;
        }

        public override async Task<CouponResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context) {
            // Map from CouponResponse to a Coupon model to call repository methods
            var coupon = _mapper.Map<CouponResponse, Coupon>(request.Coupon);

            await _repository.UpdateDiscount(coupon);
            _logger.LogInformation("Discount is updated successfully. ProductName: {ProductName}", coupon.ProductName);

            // Map back from a Coupon model to a CouponResponse to return for the gRPC method
            var couponResponse = _mapper.Map<Coupon, CouponResponse>(coupon);

            return couponResponse;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context) {
            var success = await _repository.DeleteDiscount(request.ProductName);

            var response = new DeleteDiscountResponse {
                Success = success
            };

            return response;
        }


    }
}