using AutoMapper;
using Cart.API.Dtos;
using Cart.API.Entities;
using Cart.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers {

    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CartController : ControllerBase {
        private ICartRepository _repository;
        private IMapper _mapper;

        public CartController(ICartRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
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
            return Ok(await _repository.UpdateCart(_mapper.Map<ShoppingCart>(cart)));
        }

        [HttpDelete("{userName}", Name = "DeleteCart")]
        public async Task<IActionResult> DeleteCart(string userName) {
            await _repository.DeleteCart(userName);

            return Ok();
        }
    }
}