using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Services.Abstraction;
using ECommerce.Services.Exceptions;
using ECommerce.Services.Specifications.ProductSpecifications;
using ECommerceShared;
using ECommerceShared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(brands);
        }
        
        public async Task<PaginatedResult<ProductDTO>> GetAllProductAsync(ProductQueryParams queryParams)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            //GetAllProductsWithTypeAndBrands
            var spec = new ProductWithTypeAndBrandSpecification(queryParams);
            
            var products = await repo.GetAllAsync(spec);

            var specCount = new ProductWithCountSpecification(queryParams);
            var totalCount = await repo.CountAsync(specCount);
            var dataToReturn = _mapper.Map<IEnumerable<ProductDTO>>(products);
            var countDataToReturn = dataToReturn.Count();
            return new PaginatedResult<ProductDTO>(queryParams.PageIndex, countDataToReturn,
                                                    totalCount, dataToReturn);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(types);
        }
        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);

            if (product == null)
                throw new ProductNotFoundException(id);

            return _mapper.Map<ProductDTO>(product);
        }
    }
}
