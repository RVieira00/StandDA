using Microsoft.EntityFrameworkCore;
using Stand.Domain.Models;
using Stand.Domain.Repository;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Infrastructure.Repositories
{
    public class VendaRepository : Repository<Venda>, IVendaRepository
    {
        public VendaRepository(StandDbContext dbContext) : base(dbContext)
        {

        }
        
        public Task<Venda> FindByInfoAsync(int nif, string matricula)
        {
            return _dbContext.Vendas.SingleOrDefaultAsync(p => p.Cliente.Id == nif && p.Viatura.Matricula == matricula);
        }

        public override async Task<Venda> FindOrCreateAsync(Venda e)
        {
            var venda = await FindByIdAsync(e.Id);

            if (venda == null)
            {
                venda = Create(e);
                await _dbContext.SaveChangesAsync();
            }

            return venda;
        }

        public override async Task<Venda> UpsertAsync(Venda e)
        {
            Venda venda = null;
            Venda existing = await FindByIdAsync(e.Id);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    venda = Create(e);
                }
                else
                {
                    venda = Update(e);
                }

            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                venda = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return venda;
        }
    }
}
