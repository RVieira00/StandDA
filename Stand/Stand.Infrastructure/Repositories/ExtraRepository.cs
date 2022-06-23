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
    public class ExtraRepository : Repository<Extra>, IExtraRepository
    {
        public ExtraRepository(StandDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Extra> FindByNameAsync(string name)
        {
            return _dbContext.Extras.SingleOrDefaultAsync(c => c.Nome == name);
        }

        public override async Task<Extra> FindOrCreateAsync(Extra e)
        {
            var extra = await FindByNameAsync(e.Nome);

            if (extra == null)
            {
                extra = Create(e);
                await _dbContext.SaveChangesAsync();
            }

            return extra;
        }

        public override async Task<Extra> UpsertAsync(Extra e)
        {
            Extra extra = null;
            Extra existing = await FindByNameAsync(e.Nome);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    extra = Create(e);
                }
                else
                {
                    extra = Update(e);
                }
            }

            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

                await _dbContext.SaveChangesAsync();
                
                return extra;
        }

        public async Task<List<Extra>> FindAllByNameStartWithAsync(string name)
        {
            return await _dbContext.Extras
                .Where(c => c.Nome.StartsWith(name))
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }
    }
}