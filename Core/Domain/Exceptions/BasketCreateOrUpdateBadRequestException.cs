using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketCreateOrUpdateBadRequestException() :
        BadRequestExceptions("Invalide Operation When Create Or Update Basket")
    {
    }
}
