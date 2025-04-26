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
    public class BasketController(IServiceManager service) : ControllerBase 
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBasketById(string id)
        {
            var basket = await service.BasketService.GetBasketAsync(id);
           
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateBasket([FromBody] BasketDto basketDto)
        {
            var updatedBasket = await service.BasketService.UpdateBasketAsync(basketDto);
          
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            var result = await service.BasketService.DeleteBasketAsync(id);
           
            return NoContent();
        }
    }
}
