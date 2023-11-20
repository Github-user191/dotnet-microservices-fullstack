using Discount.Grpc.Protos;

namespace Cart.API.SyncDataServices {
    public interface IDiscountGrpcService {
        Task<CouponResponse> GetDiscount(string productName);
    }
}