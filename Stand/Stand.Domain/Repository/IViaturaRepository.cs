using Stand.Domain.Models;
using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain.Repository
{
    public interface IViaturaRepository : IRepository<Viatura>
    {
        Task<Viatura> FindByMatriculaAsync(string matricula);
        Task<Viatura> FindByMarcaAsync(string marca);
        Task<List<Viatura>> FindAllByMatriculaStartWithAsync(string name);
    }
}
