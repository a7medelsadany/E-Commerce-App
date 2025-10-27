﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProductModule;
using Shared;

namespace Services.Specifications
{
    public class ProductCountSpecifications(ProductQueryParams queryParams) : BaseSpecifications<Product, int>
        (P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId) && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.Contains(queryParams.SearchValue.ToLower())))
    {

    }
    
    
}
