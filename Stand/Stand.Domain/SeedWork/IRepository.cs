using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain.SeedWork
{
    public interface IRepository<T> where T : Entity
    {
        T Create(T e);
        T Update(T e);
        T Delete(T e);

        Task<T> UpsertAsync(T e);
        Task<T> FindOrCreateAsync(T e);
        Task<T> FindByIdAsync(int id);
        Task<List<T>> FindAllAsync();
    }
}
