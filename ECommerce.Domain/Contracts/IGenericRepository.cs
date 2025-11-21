using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications);
        public Task<TEntity?> GetByIdAsync(Tkey id);
        public Task<TEntity?> GetByIdAsync(ISpecifications<TEntity,Tkey> specifications);
        public Task AddAsync(TEntity entity);
        public void Update(TEntity entity);
        public void DeleteAsync(TEntity entity);
        public Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications);
    }
}
