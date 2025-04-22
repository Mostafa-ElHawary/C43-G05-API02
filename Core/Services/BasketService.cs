using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            if (basket is null) throw new BasketNotFoundExcpetion(id);
            var basketDto = mapper.Map<BasketDto>(basket);
            return basketDto;

        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
           var flag = await basketRepository.DeleteBasketAsync(id); 
            return flag;    
        }


        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)

        {
            var basketDto = mapper.Map<CustomerBasket>(basket);
           var  customerBasket = await basketRepository.UpdateBasketAsync(basketDto);
            if (customerBasket is null) throw new Exception($"Can't Update Or Create Basket {basket.Id}");
            return mapper.Map<BasketDto>(customerBasket); 
        }
    }
}
