using Microsoft.EntityFrameworkCore;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly StandDbContext _dbContext;

        public Repository(StandDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Create(T e)
        {
            return _dbContext.Set<T>().Add(e).Entity; // retorna uma entidade
        }

        public T Delete(T e)
        {
            return _dbContext.Set<T>().Remove(e).Entity;
        }

        public virtual async Task<List<T>> FindAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public T Update(T e)
        {
            _dbContext.Entry(e).State = EntityState.Modified;

            return _dbContext.Set<T>().Update(e).Entity;
        }

        public abstract Task<T> UpsertAsync(T e);
        public abstract Task<T> FindOrCreateAsync(T e);
    }
}
