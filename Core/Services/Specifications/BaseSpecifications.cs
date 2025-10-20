﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        //---------------------------------------------
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByAsc)
        => OrderBy = OrderByAsc;

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set;}
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> OrderByDesc) => OrderByDescending = OrderByDesc;
        //---------------------------------------------
        protected void AddInclude(Expression<Func<TEntity,object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
    }
}
