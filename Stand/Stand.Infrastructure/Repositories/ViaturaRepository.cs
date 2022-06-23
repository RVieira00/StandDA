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
    public class ViaturaRepository : Repository<Viatura>, IViaturaRepository
    {
        public ViaturaRepository(StandDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Viatura> FindByMarcaAsync(string marca)
        {
            return _dbContext.Viaturas.SingleOrDefaultAsync(p => p.Marca.Nome == marca);
        }

        public Task<Viatura> FindByMatriculaAsync(string matricula)
        {
            return _dbContext.Viaturas.SingleOrDefaultAsync(p => p.Matricula == matricula);
        }

        public override async Task<Viatura> FindOrCreateAsync(Viatura e)
        {
            var viatura = await FindByIdAsync(e.Id);

            if (viatura == null)
            {
                viatura = Create(e);
                await _dbContext.SaveChangesAsync();
            }

            return viatura;
        }

        public override async Task<Viatura> UpsertAsync(Viatura e)
        {
            Viatura viatura = null;
            Viatura existing = await FindByIdAsync(e.Id);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    viatura = Create(e);
                }
                else
                {
                    viatura = Update(e);
                }

            }

            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                viatura = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return viatura;
        }

        public async Task<List<Viatura>> FindAllByMatriculaStartWithAsync(string name)
        {
            return await _dbContext.Viaturas
                .Where(c => c.Matricula.StartsWith(name))
                .OrderBy(c => c.Matricula)
                .ToListAsync();
        }

        public override async Task<List<Viatura>> FindAllAsync()
        {
            return await _dbContext.Viaturas
                .Include(p => p.Marca)
                .Include(t => t.Tipo)
                .Include(p => p.ViaturasExtras)
                    .ThenInclude(p => p.Extra)
                .OrderBy(p => p.Matricula)
                .ToListAsync();
        }

        public Task<List<Viatura>> FindAllByMarcaStartWithAsync(int marcaId, string name)
        {
            return _dbContext.Viaturas
                .Where(p => p.Matricula.StartsWith(name) && (marcaId == 0 || p.MarcaId == marcaId))
                .OrderBy(p => p.Matricula)
                .ToListAsync();
        }

        public Task<List<Viatura>> FindAllByTipoStartWithAsync(int tipoId, string name)
        {
            return _dbContext.Viaturas
                .Where(p => p.Matricula.StartsWith(name) && (tipoId == 0 || p.TipoId == tipoId))
                .OrderBy(p => p.Matricula)
                .ToListAsync();
        }
    }
}
