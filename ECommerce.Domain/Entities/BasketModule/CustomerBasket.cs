using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } = null!; // Created from front-end [Guid]
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
