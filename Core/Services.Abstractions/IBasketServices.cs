using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.BasketDTOS;

namespace Services.Abstractions
{
    public interface IBasketServices
    {
        Task<BasketDto?> GetBasketAsync(string Key);
        Task<BasketDto?> CreateOrUpdateBasketAsync(BasketDto basket);
        Task<bool?> DeleteBasketAsync(string Key);
    }
}
