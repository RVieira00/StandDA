using Microsoft.EntityFrameworkCore;
using Stand.Domain.Models;
using Stand.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Infrastructure.Repositories
{
    public class ViaturaExtraRepository : Repository<ViaturaExtra>, IViaturaExtraRepository
    {
        public ViaturaExtraRepository(StandDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ViaturaExtra> FindByExtraAsync(int ExtraId)
        {
            return _dbContext.ViaturaExtras.SingleOrDefaultAsync(p => p.ExtraId == ExtraId);
        }

        public async Task<List<Extra>> FindByViaturaIdAsync(int id)
        {
            return await _dbContext.Extras
                .Where(c => c.Id == id)
                .ToListAsync();
        }

        // @param o parametro que é passado como argumento é o nome do extra
        public Task<ViaturaExtra> FindByNameAsync(string name)
        {
            return _dbContext.ViaturaExtras.SingleOrDefaultAsync(p => p.Extra.Nome == name);
        }

        public Task<ViaturaExtra> FindByViaturaAsync(int ViaturaId)
        {
            return _dbContext.ViaturaExtras.SingleOrDefaultAsync(p => p.ViaturaId == ViaturaId);
        }

        public override async Task<ViaturaExtra> FindOrCreateAsync(ViaturaExtra e)
        {
            var viaturaExtra = await FindByIdAsync(e.Id);

            if (viaturaExtra == null)
            {
                viaturaExtra = Create(e);
                await _dbContext.SaveChangesAsync();
            }

            return viaturaExtra;

        }

        public override async Task<ViaturaExtra> UpsertAsync(ViaturaExtra e)
        {
            ViaturaExtra viaturaExtra = null;
            ViaturaExtra existing = await FindByIdAsync(e.Id);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    viaturaExtra = Create(e);
                }
                else
                {
                    viaturaExtra = Update(e);
                }

            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                viaturaExtra = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return viaturaExtra;
        }
    }
}
