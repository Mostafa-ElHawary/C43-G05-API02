using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;

namespace Domain.Models.OrderModels
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
        }
        public Order(string userEmail, Address shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; }
        public Address ShippingAddress { get; set; }

        public  ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }

        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        public decimal Subtotal { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public string PaymentIntentId { get; set; }

    }


}
