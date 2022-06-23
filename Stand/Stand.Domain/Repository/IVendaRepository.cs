using Stand.Domain.Models;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain.Repository
{
    public interface IVendaRepository : IRepository<Venda>
    {
        Task<Venda> FindByInfoAsync(int nif, string matricula);
    }
}
