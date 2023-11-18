using AutoMapper;
using Discount.API.Dtos;
using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase {

        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName) {
            var coupon = await _repository.GetDiscount(productName);

            return Ok(_mapper.Map<Coupon, CouponReadDto>(coupon));
        }

        [HttpPost]
        public async Task<ActionResult<CouponReadDto>> CreateDiscount([FromBody] CouponCreateDto request) {
            var coupon = _mapper.Map<CouponCreateDto, Coupon>(request);

            await _repository.CreateDiscount(coupon);
    
            return CreatedAtRoute(nameof(GetDiscount), new {ProductName= coupon.ProductName}, coupon);
        }

        [HttpPut]
        public async Task<ActionResult<CouponReadDto>> UpdateDiscount([FromBody] CouponUpdateDto request) {
            
            var coupon = _mapper.Map<CouponUpdateDto, Coupon>(request);
            return Ok(await _repository.UpdateDiscount(coupon));
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName) {
            return Ok(await _repository.DeleteDiscount(productName));
        }
        
    }
}