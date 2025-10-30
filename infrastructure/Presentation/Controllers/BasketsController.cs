using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOS.BasketDTOS;

namespace Presentation.Controllers
{
    
    public class BasketsController(IServiceManager _serviceManager): APIBaseController
    {

        #region Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(String Key)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(Basket);
        } 
        #endregion
        //-------------------------------------------

        #region Create Or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        } 
        #endregion

        //---------------------------------------------

        #region Delete Basket
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool?>> DeleteBasket(String Key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        } 
        #endregion

    }
}
