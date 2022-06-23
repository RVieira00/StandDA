using Stand.Domain.Models;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain.Repository
{
    public interface ITipoRepository : IRepository<Tipo>
    {
        Task<Tipo> FindByNameAsync(string name);
        Task<List<Tipo>> FindAllByNameStartWithAsync(string name);
    }
}
