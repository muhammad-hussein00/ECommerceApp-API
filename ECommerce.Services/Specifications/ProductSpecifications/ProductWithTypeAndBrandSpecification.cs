using ECommerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications.ProductSpecifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product, int>
    {
        public ProductWithTypeAndBrandSpecification():base()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
