using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,IBasketRepository basketRepository,UserManager<ApplicationUser> userManager,IConfiguration configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketServices> _LazyBasketService = new Lazy<IBasketServices>(() => new BasketServices(basketRepository, mapper));
        private readonly Lazy<IAuthenticationServices> _LazyAuthenticationService = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager,configuration));
        
        public IProductService ProductService => _LazyProductService.Value ;

        public IBasketServices BasketService => _LazyBasketService.Value;

        public IAuthenticationServices AuthenticationServices => _LazyAuthenticationService.Value;
    }
}
