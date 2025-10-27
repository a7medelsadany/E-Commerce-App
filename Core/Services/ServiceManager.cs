using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,IBasketRepository basketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketServices> _LazyBasketService = new Lazy<IBasketServices>(() => new BasketServices(basketRepository, mapper));
        
        public IProductService ProductService => _LazyProductService.Value ;

        public IBasketServices BasketService => _LazyBasketService.Value;
    }
}
