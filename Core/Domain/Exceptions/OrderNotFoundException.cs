using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderNotFoundException(Guid id)
        : NotFoundExcpetion($"Order with id {id} Is Not Found")
    {
    }
}
