using Stand.Domain.Models;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain.Repository
{
    public interface IMarcaRepository : IRepository<Marca>
    {
        Task<Marca> FindByNameAsync(string name);
        Task<List<Marca>> FindAllByNameStartWithAsync(string name);
    }
}
