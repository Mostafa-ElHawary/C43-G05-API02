using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketNotFoundExcpetion(string id) : NotFoundExcpetion($"Basket With Id {id} is Not Found")
    {
    }
}
