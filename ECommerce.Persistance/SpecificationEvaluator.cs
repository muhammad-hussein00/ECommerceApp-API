using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistance
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> entryPoint, ISpecifications<TEntity,TKey> specifications)
            where TEntity : BaseEntity<TKey>
        {
            var query = entryPoint; // _storeContext.products
            if(specifications != null)
            {
                if(specifications.Criteria != null)
                {
                    query = query.Where(specifications.Criteria);
                }
                if(specifications.IncludeExpressions is not null)
                {
                    query = specifications.IncludeExpressions.Aggregate(query, (currentQuery, includeExcpression) =>
                                                                                currentQuery.Include(includeExcpression));
                }
            }
            return query;
        }
    }
}
