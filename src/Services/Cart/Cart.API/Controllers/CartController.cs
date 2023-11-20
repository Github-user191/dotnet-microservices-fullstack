using AutoMapper;
using Cart.API.Dtos;
using Cart.API.Entities;
using Cart.API.Repositories;
using Cart.API.SyncDataServices;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers {

    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CartController : ControllerBase {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDiscountGrpcService _discountGrpcService;

        public CartController(ICartRepository repository, IMapper mapper, IDiscountGrpcService discountGrpcService) {
            _repository = repository;
            _mapper = mapper;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        public async Task<ActionResult<ShoppingCart>> GetCart(string userName) {
            var cart = await _repository.GetCart(userName);
            
            Console.WriteLine($"--> Getting cart items for {userName}");

            //var mapped = _mapper.Map<ShoppingCartReadDto>(cart);
            Console.WriteLine(cart);
            //cart.Items.ForEach(i => Console.WriteLine(i));
            return Ok(cart ?? new ShoppingCart(userName));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCartUpdateDto>> UpdateCart([FromBody] ShoppingCartUpdateDto cart) {
            
            // Communicate with Discount gRPC to calculate latest product prices
            foreach(var item in cart.Items) {
                // For every cart item, perform inter service gRPC call
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                // Deduct coupon value from each item
                item.Price -= coupon.Amount;
            }
            
            return Ok(await _repository.UpdateCart(_mapper.Map<ShoppingCart>(cart)));
        }

        [HttpDelete("{userName}", Name = "DeleteCart")]
        public async Task<IActionResult> DeleteCart(string userName) {
            await _repository.DeleteCart(userName);

            return Ok();
        }
    }
}