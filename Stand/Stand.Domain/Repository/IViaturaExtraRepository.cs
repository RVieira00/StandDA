using Stand.Domain.Models;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain.Repository
{
    public interface IViaturaExtraRepository : IRepository<ViaturaExtra>
    {
        Task<ViaturaExtra> FindByNameAsync(string name);
        Task<List<Extra>> FindByViaturaIdAsync(int ViaturaId);
        Task<ViaturaExtra> FindByExtraAsync(int ExtraId);
    }
}
