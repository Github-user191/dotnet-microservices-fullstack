using AutoMapper;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using Catalog.API.Dtos;

namespace Catalog.API.Controllers {

    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase {

        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;
        private readonly IMapper _mapper;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger, IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProducts() {
            var products = await _repository.GetProducts();

            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        [HttpGet("{id:length(24)}", Name = "GetProductById")]
        public async Task<ActionResult<ProductReadDto>> GetProductById(string id) {
            var product = await _repository.GetProduct(id);

            if (product == null) {
                _logger.LogError($"Product with id {id}, not found");
                return NotFound();
            }

            return Ok(product);

        }

        [Route("[action]/{category}", Name = "GetProductsByCategory")]
        [HttpGet]
        public async Task<ActionResult<ProductReadDto>> GetProductsByCategory(string category) {
            var products = await _repository.GetProductsByCategory(category);

            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));

        }

        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> CreateProduct([FromBody] ProductCreateDto request) {

            Console.WriteLine($"--> Request to create {request}");
            var product = _mapper.Map<Product>(request);
            Console.WriteLine($"Request mapped to entity {product}");
            await _repository.CreateProduct(product);

            var productReadDto = _mapper.Map<ProductReadDto>(product);
            Console.WriteLine($"Entity mapped to read dto {productReadDto}");

            return CreatedAtRoute(nameof(GetProductById),
                new { Id = productReadDto.Id }, productReadDto);


        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto request) {
            return Ok(await _repository.UpdateProduct(_mapper.Map<Product>(request)));

        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProductById(string id) {
            return Ok(await _repository.DeleteProduct(id));
        }
    }
}
