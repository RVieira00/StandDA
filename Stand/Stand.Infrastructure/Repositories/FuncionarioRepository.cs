using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using Stand.Domain.Models;
using Stand.Domain.Repository;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Stand.Infrastructure.Repositories
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(StandDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Funcionario> FindByEmail(string email)
        {
            return await _dbContext.Funcionarios.SingleOrDefaultAsync(p => p.Email == email);
        }

        public async Task<Funcionario> FindByNameAsync(string name)
        {
            return await _dbContext.Funcionarios.SingleOrDefaultAsync(p => p.Nome == name);
        }

        public async Task<Funcionario> FindByUsername(string username)
        {
            return await _dbContext.Funcionarios
                           .SingleOrDefaultAsync(x => x.Nome == username);
        }

        public async Task<Funcionario> FindByEmailAndPassword(string email, string password)
        {
            return await _dbContext.Funcionarios
               .Include(x => x.Vendas)
               .SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public override async Task<Funcionario> FindOrCreateAsync(Funcionario e)
        {
            var funcionario = await FindByIdAsync(e.Id);

            if (funcionario == null)
            {
                funcionario = Create(e);
                await _dbContext.SaveChangesAsync();
            }

            return funcionario;
        }

        public override async Task<Funcionario> UpsertAsync(Funcionario e)
        {
            Funcionario funcionario = null;
            Funcionario existing = await FindByIdAsync(e.Id);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    funcionario = Create(e);
                }
                else
                {
                    funcionario = Update(e);
                }

            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                funcionario = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return funcionario;
        }
    }
}
