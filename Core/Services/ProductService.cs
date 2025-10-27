using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.DTOS;
using Shared.Enums;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetReposityory<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }

        public async Task<PagintedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
           //var Products = await _unitOfWork.GetReposityory<Product, int>().GetAllAsync();
           // return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
           var Specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Repo = _unitOfWork.GetReposityory<Product, int>();
            var AllProducts = await Repo.GetAllAsync(Specifications); //Product
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(AllProducts);
            var ProductCount=AllProducts.Count();
            var CountSpec = new ProductCountSpecifications(queryParams);
            var TotalCount=await Repo.CountAsync(CountSpec);

            return new PagintedResult<ProductDto>(ProductCount, queryParams.pageIndex, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetReposityory<ProductType,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>,IEnumerable < TypeDto >> (Types);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int Id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(Id);
            var Product = await _unitOfWork.GetReposityory<Product,int>().GetByIdAsync(Specifications);
            if(Product is null)
            {
                throw new ProductNotFoundException(Id);
            }
            return _mapper.Map<Product, ProductDto>(Product);

        }
    }
}
