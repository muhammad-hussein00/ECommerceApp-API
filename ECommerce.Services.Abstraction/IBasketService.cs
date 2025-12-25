using ECommerceShared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstraction
{
    public interface IBasketService
    {
        Task<BasketDTO> CreateOrUpdateBasket(BasketDTO basketDTO);
        Task<bool> DeleteBasket(string basketId);
        Task<BasketDTO> GetBasket(string basketId);
    }
}
