using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstractions;
using Shared;
using Shared.ErrorModels;

namespace Presentation
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType<ProductResultDto>(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductResultDto>))]
        [ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType<ErrorDetails>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [Cache(100)]
        [Authorize]
        public async Task<ActionResult<PaginationResponse<ProductResultDto>>> GetAllProducts([FromQuery] ProductSpecificationsParamters SpecParams)
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
        [ProducesResponseType<ProductResultDto>(StatusCodes.Status200OK, Type = typeof(ProductResultDto))]
        [ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType<ErrorDetails>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType<ErrorDetails>(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
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
        [ProducesResponseType<BrandResultDto>(StatusCodes.Status200OK, Type = typeof(BrandResultDto))]
        [ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType<ErrorDetails>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandResultDto>> GetAllBrands()
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
        [ProducesResponseType<TypeResultDto>(StatusCodes.Status200OK, Type = typeof(TypeResultDto))]
        [ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType<ErrorDetails>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<TypeResultDto>> GetAllTypes()
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
