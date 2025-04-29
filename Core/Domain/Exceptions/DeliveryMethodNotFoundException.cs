using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public  class DeliveryMethodNotFoundException(int id)
        : NotFoundExcpetion($"Delivery Method with id {id} Is Not Found")
        {
        }
    }
