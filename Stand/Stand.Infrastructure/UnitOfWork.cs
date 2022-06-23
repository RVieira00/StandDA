using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Stand.Domain;
using Stand.Domain.Repository;
using Stand.Infrastructure.Repositories;

namespace Stand.Infrastructure
{

    public class UnitOfWork : IUnitOfWork
    {
        private StandDbContext _dbContext;

        public UnitOfWork()
        {
            _dbContext = new StandDbContext();
            _dbContext.Database.EnsureCreated();

        }

        public IExtraRepository ExtraRepository => new ExtraRepository(_dbContext);

        public IMarcaRepository MarcaRepository => new MarcaRepository(_dbContext);

        public IViaturaRepository ViaturaRepository => new ViaturaRepository(_dbContext);

        public ITipoRepository TipoRepository => new TipoRepository(_dbContext);

        public IClienteRepository ClienteRepository => new ClienteRepository(_dbContext);

        public IVendaRepository VendaRepository => new VendaRepository(_dbContext);

        public IViaturaExtraRepository ViaturaExtraRepository => new ViaturaExtraRepository(_dbContext);

        public IFuncionarioRepository FuncionarioRepository => new FuncionarioRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}