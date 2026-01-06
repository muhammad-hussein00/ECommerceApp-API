using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Exceptions
{
    public abstract class NotFoundException(string message):Exception(message)
    {
    }
    public sealed class ProductNotFoundException(int id):NotFoundException($"Priduct with id: {id} is not found.") { }
    public sealed class BasketNotFoundException(string id):NotFoundException($"Basket with id : {id} is not found.") { }
}
