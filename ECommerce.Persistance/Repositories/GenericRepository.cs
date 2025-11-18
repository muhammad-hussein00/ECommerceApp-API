using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistance.GenericRepository
{
    internal class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _storeDbContext;

        public GenericRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _storeDbContext.Set<TEntity>().ToListAsync();
        public async Task<TEntity?> GetByIdAsync(Tkey id) => await _storeDbContext.Set<TEntity>().FindAsync(id);
        public async Task AddAsync(TEntity entity) => await _storeDbContext.Set<TEntity>().AddAsync(entity);
        public void DeleteAsync(TEntity entity) => _storeDbContext.Set<TEntity>().Remove(entity);   
        public void Update(TEntity entity) => _storeDbContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications)
        {
            var query = SpecificationEvaluator.CreateQuery(_storeDbContext.Set<TEntity>(), specifications);
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications)
        {
            var query = SpecificationEvaluator.CreateQuery(_storeDbContext.Set<TEntity>(), specifications);
            return await query.FirstOrDefaultAsync();
        }
    }
}
