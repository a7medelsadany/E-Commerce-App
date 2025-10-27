using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Exceptions;
using Services.Abstractions;
using Shared.DTOS.BasketDTOS;

namespace Services
{
    public class BasketServices(IBasketRepository _repository, IMapper _mapper) : IBasketServices
    {
        public async Task<BasketDto?> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket=_mapper.Map<BasketDto,CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = _repository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreatedOrUpdatedBasket is not null)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("Can Not Update Or Delete Basket!!");
            }
        }

        //-------------------------------------------
        
        public async Task<bool?> DeleteBasketAsync(string Key)
            =>await _repository.DeleteBasketAsync(Key);
        //-------------------------------------------
        
        public async Task<BasketDto?> GetBasketAsync(string Key)
        {
            var Basket=await _repository.GetBasketAsync(Key);
            if(Basket is not null)
            {
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            }
            else
            {
                throw new BasketNotFoundExceptions(Key);
            }
        }

        
    }
}
