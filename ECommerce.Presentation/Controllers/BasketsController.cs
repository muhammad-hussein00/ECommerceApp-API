using ECommerce.Services.Abstraction;
using ECommerceShared.DTOs.BasketDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        #region Get basket

        // {{BaseUrl}}/api/Baskets?basketId=Basket 01
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasket(string basketId)
        {
            var basket = await _basketService.GetBasket(basketId);

            return Ok(basket);
        }
        #endregion

        #region Create or update basket

        // {{BaseUrl}}/api/Baskets
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basketDTO)
        {
            var createdOrUpdatedBasket = await _basketService.CreateOrUpdateBasket(basketDTO);
            return Ok(createdOrUpdatedBasket);
        }
        #endregion

        #region Delete

        // {{BaseUrl}}/api/Baskets/Basket 01
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket([FromRoute] string id)
        {
            var isDeleted = await _basketService.DeleteBasket(id);
            return Ok(isDeleted);
        } 
        #endregion
    }
}
