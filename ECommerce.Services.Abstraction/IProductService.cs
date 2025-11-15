using ECommerceShared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstraction
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAllProductAsync();
        public Task<ProductDTO> GetProductByIdAsync(int id);
        public Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        public Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
        
    }
}
