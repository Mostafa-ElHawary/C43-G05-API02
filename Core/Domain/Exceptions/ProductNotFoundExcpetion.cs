using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class ProductNotFoundExcpetion(int id) 
        : NotFoundExcpetion($"Product with id {id} not found")
    {
    }
}
