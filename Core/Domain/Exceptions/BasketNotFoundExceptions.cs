using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketNotFoundExceptions(string Id):NotFoundException($"Basket with Id:{Id} is Not Found")
    {
    }
}
