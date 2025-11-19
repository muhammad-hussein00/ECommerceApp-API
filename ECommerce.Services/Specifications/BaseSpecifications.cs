using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    internal abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        protected BaseSpecifications(Expression<Func<TEntity,bool>> criteriaExp)
        {
            Criteria = criteriaExp;
        }
        #region Criteria
        public Expression<Func<TEntity, bool>> Criteria { get; }

        #endregion 
        
        #region Include
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExp)
        {
            IncludeExpressions.Add(includeExp);
        }
        #endregion

        #region Ordering
        public Expression<Func<TEntity, object>> OrderBy { private set; get; }
        public Expression<Func<TEntity, object>> OrderByDescending { private set; get; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp)
        {
            OrderBy = orderByExp;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExp)
        {
            OrderByDescending = orderByDescendingExp;
        }
        #endregion

        #region Pagination
        public int Skip { private set; get; }
        public int Take { private set; get; }
        public bool IsPaginated { private set; get; }
        protected void ApplyPagination(int pageIndex, int pageSize)
        {
            IsPaginated = true;
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
        }
        #endregion
    }
}
