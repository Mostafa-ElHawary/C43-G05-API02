using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecificationsParamters SpecParams)
        {
            // Call the service to get all products
            var products = await serviceManager.ProductService.GetAllProductAsync(SpecParams);
            if (products == null)
            {
                return BadRequest();
            }
            return Ok(products);
            //return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            // Call the service to get all products
            var product = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // Get all brands

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            // Call the service to get all products
            var brands = await serviceManager.ProductService.GetAllBrandsAsync();
            if (brands == null)
            {
                return BadRequest();
            }
            return Ok(brands);
        }

        // Get all types
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            // Call the service to get all products
            var types = await serviceManager.ProductService.GetAllTypesAsync();
            if (types == null)
            {
                return BadRequest();
            }
            return Ok(types);
        }
    }
}
