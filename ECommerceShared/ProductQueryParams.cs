using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShared
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public ProductSortingOptions? Sorting {  get; set; }
    }
}
