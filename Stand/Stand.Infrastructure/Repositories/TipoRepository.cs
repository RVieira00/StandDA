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
    public class TipoRepository : Repository<Tipo>, ITipoRepository
    {

        public TipoRepository(StandDbContext dbContext) : base(dbContext)
        {

        }
        public Task<Tipo> FindByNameAsync(string name)
        {
            return _dbContext.Tipos.SingleOrDefaultAsync(p => p.Nome == name);
        }

        public override async Task<Tipo> FindOrCreateAsync(Tipo e)
        {
            var tipo = await FindByNameAsync(e.Nome);

            if (tipo == null)
            {
                tipo = Create(e);
                await _dbContext.SaveChangesAsync();
            }

            return tipo;
        }

        public override async Task<Tipo> UpsertAsync(Tipo e)
        {
            Tipo tipo = null;
            Tipo existing = await FindByNameAsync(e.Nome);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    tipo = Create(e);
                }
                else
                {
                    tipo = Update(e);
                }

            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                tipo = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return tipo;
        }

        public override async Task<List<Tipo>> FindAllAsync()
        {
            return await _dbContext.Tipos
                .Include(c => c.Viaturas)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<List<Tipo>> FindAllByNameStartWithAsync(string name)
        {
            return await _dbContext.Tipos
                .Where(x => x.Nome.StartsWith(name))
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }
    }
}
