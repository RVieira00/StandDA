using Stand.Domain.Models;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> FindByNameAsync(string name);
        Task<Cliente> FindByIdAsync(int id);
        Task<List<Cliente>> FindAllByNameStartWithAsync(string text);
    }
}
