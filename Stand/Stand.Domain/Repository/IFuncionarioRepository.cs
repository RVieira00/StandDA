using Stand.Domain.Models;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain.Repository
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        Task<Funcionario> FindByUsername(string username);
        Task<Funcionario> FindByNameAsync(string name);
        Task<Funcionario> FindByEmail(string email);
        Task<Funcionario> FindByEmailAndPassword(string email, string password);
    }
}
