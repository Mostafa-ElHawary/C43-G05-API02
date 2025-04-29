using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using Services.Abstractions;
using Services.Specifications;
using Shared.OrdersModels;

namespace Services
{
    public class OrderService(IMapper mapper,
        IBasketRepository basketRepository,
        IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest, string userEmail)
        {
            var address = mapper.Map<Address>(orderRequest.ShipToAddress);
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);

            if (basket == null) throw new BasketNotFoundExcpetion(orderRequest.BasketId);

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id);
                if (product == null) throw new ProductNotFoundExcpetion(item.Id);

                var orderItem = new OrderItem(

                    new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), item.Quantity, product.Price
                    );

                orderItems.Add(orderItem);

            }

            // delivery method
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(orderRequest.DeliveryMethodId);
            if (deliveryMethod == null) throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);
            //subTotal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            //Create payment Intent Id
         


            var order = new Order(userEmail, address, orderItems, deliveryMethod, subtotal, "");
           await unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
            var count = await unitOfWork.SaveChangesAsync();
            if (count == 0) throw new OrderCreateBadRequestException();
           var result =  mapper.Map<OrderResultDto>(order);
            return result;
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            // GetAllDeliveryMethods
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            // mapper
            var result = mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
            return result;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);

            //GetOrderByIdAsync
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(id);
            if (order == null) throw new OrderNotFoundException(id);
            // mapper
            var result = mapper.Map<OrderResultDto>(order);
            return result;
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string UserEmail)
        {
            // GetOrdersByUserEmailAsync
            var spec = new OrderSpecification(UserEmail);
            var orders = unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            //if (orders == null) throw new OrderNotFoundException(email);
            // mapper
            var result =  mapper.Map<IEnumerable<OrderResultDto>>(orders);
            return result;
        }
    }
}
