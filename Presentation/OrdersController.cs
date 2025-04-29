using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.OrdersModels;

namespace Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequestDto orderRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.OrderService.CreateOrderAsync(orderRequest, email);
            return Ok(result);
        }

        // GetOrders
        [HttpGet]
        //[Route("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.GetOrdersByUserEmailAsync(email);
            return Ok(result);
        }

        //GetOrderById
        [HttpGet("{id}")]
        //[Route("GetOrderById")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(result);
        }
        // Get all Delivery method
        [HttpGet("GetAllDeliveryMethods")]
        public async Task<IActionResult> GetAllDeliveryMethods()
        {
            var result = await serviceManager.OrderService.GetAllDeliveryMethods();
            return Ok(result);
        }

    }
}
