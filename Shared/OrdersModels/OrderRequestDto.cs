using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrdersModels
{
    public class OrderRequestDto
    {
        //Basket id
        public string BasketId { get; set; }
        public AddressDto ShipToAddress { get; set; }

        //delivery method
        public int DeliveryMethodId { get; set; }
    }
}
