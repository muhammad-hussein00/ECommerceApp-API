using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Persistance.DbContexts;
using ECommerce.Persistance.GenericRepository;

namespace ECommerce.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly Dictionary<Type, Object> _repositories = [];
        public UnitOfWork(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            if(_repositories.ContainsKey(typeof(TEntity)))
                return (IGenericRepository<TEntity, Tkey>) _repositories[typeof(TEntity)];
            else
            {
                var newRepo = new GenericRepository<TEntity, Tkey>(_storeDbContext);
                _repositories.Add(typeof(TEntity), newRepo);
                return newRepo;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _storeDbContext.SaveChangesAsync();
        }
    }
}
