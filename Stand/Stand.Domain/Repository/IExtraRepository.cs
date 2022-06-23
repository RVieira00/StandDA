using Stand.Domain.Models;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain.Repository
{
    public interface IExtraRepository : IRepository<Extra>
    {
        Task<Extra> FindByNameAsync(string name);
        Task<List<Extra>> FindAllByNameStartWithAsync(string name);

    }
}
