
using Microsoft.EntityFrameworkCore;
using Stand.Domain.Models;
using Stand.Domain.Repository;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Infrastructure.Repositories
{

    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(StandDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Cliente>> FindAllByNameStartWithAsync(string text)
        {
            return await _dbContext.Clientes
                .Where(x => x.Nome.StartsWith(text))
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public Task<Cliente> FindByIdAsync(int id)
        {
            return _dbContext.Clientes.SingleOrDefaultAsync(p => p.Id == id);
        }

        public Task<Cliente> FindByNameAsync(string name)
        {
            return _dbContext.Clientes.SingleOrDefaultAsync(p => p.Nome == name);
        }

        public override async Task<Cliente> FindOrCreateAsync(Cliente e)
        {
            var marca = await FindByNameAsync(e.Nome);

            if (marca == null)
            {
                marca = Create(e);
                await _dbContext.SaveChangesAsync();
            }

            return marca;
        }

        public async override Task<Cliente> UpsertAsync(Cliente e)
        {
            Cliente cliente = null;
            Cliente existing = await FindByNameAsync(e.Nome);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    cliente = Create(e);
                }
                else
                {
                    cliente = Update(e);
                }

            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                cliente = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return cliente;
        }
    }
}
