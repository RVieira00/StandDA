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
    public class MarcaRepository : Repository<Marca>, IMarcaRepository
    {
        public MarcaRepository(StandDbContext dbContext) : base(dbContext)
        {

        }

        public Task<Marca> FindByNameAsync(string name)
        {
            return _dbContext.Marcas.SingleOrDefaultAsync(c => c.Nome == name);
        }

        public override async Task<Marca> FindOrCreateAsync(Marca e)
        {
            var marca = await FindByNameAsync(e.Nome);

            if (marca == null)
            {
                marca = Create(e);
                await _dbContext.SaveChangesAsync();
            }

            return marca;
        }

        public override async Task<Marca> UpsertAsync(Marca e)
        {
            Marca marca = null;
            Marca existing = await FindByNameAsync(e.Nome);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    marca = Create(e);
                }
                else
                {
                    marca = Update(e);
                }

            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                marca = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return marca;
        }

        public override async Task<List<Marca>> FindAllAsync()
        {
            return await _dbContext.Marcas
                .Include(c => c.Viaturas)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        /// <summary>
        /// Usado para fazer sugestões ao pesquisar a marca
        /// Poderá ser utilizado para outros fins
        /// </summary>
        /// <param name="name">
        /// Input proveniente do utilizador
        /// </param>
        /// <returns></returns>
        public async Task<List<Marca>> FindAllByNameStartWithAsync(string name)
        {
            return await _dbContext.Marcas
                .Where(c => c.Nome.StartsWith(name))
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }
    }
}
