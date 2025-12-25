using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using ECommerce.Services.Abstraction;
using ECommerceShared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        #region Create or update basket
        public async Task<BasketDTO> CreateOrUpdateBasket(BasketDTO basketDTO)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basketDTO);
            var basket = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);

            return _mapper.Map<BasketDTO>(basket);
        }
        #endregion

        #region Delete basket
        public async Task<bool> DeleteBasket(string basketId) => await _basketRepository.DeleteBasket(basketId);

        #endregion

        #region Get basket
        public async Task<BasketDTO> GetBasket(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return _mapper.Map<BasketDTO>(basket);
        } 
        #endregion
    }
}
