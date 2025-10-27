using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Services.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> inputQuery,
            ISpecifications<TEntity,TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            //step01
            var query = inputQuery;
            //step 02 Apply Criteria

            if (specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }
            //--------------------------------

            //step 03 Apply Sorting
            if (specifications.OrderBy is not null)
            {
                query=query.OrderBy(specifications.OrderBy);
            }
            if(specifications.OrderByDescending is not null)
            {
                query=query.OrderByDescending(specifications.OrderByDescending);
            }
            //--------------------------------

            // step 04 Apply Includes
            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                query = specifications.IncludeExpressions
                    .Aggregate(query, (currentQuery, expression) => currentQuery.Include(expression));
            }
            //--------------------------------
            if(specifications.IsPaginated)
            {
                query=query.Skip(specifications.Skip).Take(specifications.Take);
            }

            return query;
        }
    }
}
