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
        public ProductWithTypeAndBrandSpecification(int? brandId, int? typeId) :base(P => (!brandId.HasValue ||P.ProductBrandId == brandId.Value)
                                                                                        &&(!typeId.HasValue ||P.ProductTypeId == typeId.Value))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
            
        }
        public ProductWithTypeAndBrandSpecification(int id):base(X => X.Id == id) 
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
