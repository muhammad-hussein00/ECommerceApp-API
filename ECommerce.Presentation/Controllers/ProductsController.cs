using ECommerce.Services.Abstraction;
using ECommerceShared;
using ECommerceShared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProductsAsync([FromQuery] ProductQueryParams queryParams)
        {
            var products = await _productService.GetAllProductAsync(queryParams);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductByIdAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrandsAsync()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypesAsync()
        {
            var types = await _productService.GetAllTypesAsync();
            return Ok(types);
        }

    }
}
