using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModels;

namespace Services.Specifications
{
    public class OrderSpecification :BaseSpecification<Order,Guid>
    {
        public OrderSpecification(Guid id) : base(x => x.Id == id)
        {
           AddInclude( o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
        }

        public OrderSpecification(string UserEmail) : base(x => x.UserEmail == UserEmail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
            AddOrderBy(o => o.OrderDate);
        }
    }
}
