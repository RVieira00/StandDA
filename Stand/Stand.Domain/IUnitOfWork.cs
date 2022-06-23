using Stand.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IExtraRepository ExtraRepository { get; }
        IMarcaRepository MarcaRepository { get; }
        IViaturaRepository ViaturaRepository { get; }
        ITipoRepository TipoRepository { get; }
        IClienteRepository ClienteRepository { get; }
        IVendaRepository VendaRepository { get; }
        IViaturaExtraRepository ViaturaExtraRepository { get; }
        IFuncionarioRepository FuncionarioRepository { get; }

        Task SaveAsync();
    }
}
