using ECommerce.Domain.Entities.ProductModule;
using ECommerceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications.ProductSpecifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product, int>
    {
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams) :base(P => (!queryParams.BrandId.HasValue ||P.ProductBrandId == queryParams.BrandId.Value)
                                                                                        &&(!queryParams.TypeId.HasValue ||P.ProductTypeId == queryParams.TypeId.Value)
                                                                                        &&(string.IsNullOrEmpty(queryParams.Search) || P.Name.ToLower().Contains(queryParams.Search.ToLower())))
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
