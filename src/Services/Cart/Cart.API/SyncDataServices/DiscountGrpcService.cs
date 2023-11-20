using Discount.Grpc.Protos;


// Client for the gRPC communication
// We can call the GetDiscount method to communicate with the gRPC server (Discount)
// to get the latest discounts on items in the cart when we add/update the cart
namespace Cart.API.SyncDataServices {
    public class DiscountGrpcService : IDiscountGrpcService {
        private readonly DiscountProtoService.DiscountProtoServiceClient _service;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient service) {
            _service = service;
        }

        public async Task<CouponResponse> GetDiscount(string productName) {
            var request = new GetDiscountRequest { ProductName = productName};
        
            // This is calling our gRPC Server methods in our Discount gRPC project
            // using compiled proto resources
            CouponResponse response = await _service.GetDiscountAsync(request);

            Console.WriteLine($"--> Performing gRPC call for {request}");
            Console.WriteLine($"--> Discount value: {response}");
            return response;
        }
    }
} 