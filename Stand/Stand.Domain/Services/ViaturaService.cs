using Stand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Stand.Domain.Services
{
    public class ViaturaService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public ViaturaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Viatura> AddViaturaAsync(string MarcaName, string TipoName, Viatura Viatura)
        {
            var marca = new Marca(MarcaName);
            var marcaUpdated = await _unitOfWork.MarcaRepository.FindOrCreateAsync(marca);
            await _unitOfWork.SaveAsync();

            var tipo = new Tipo(TipoName);
            var tipoUpdated = await _unitOfWork.TipoRepository.FindOrCreateAsync(tipo);
            await _unitOfWork.SaveAsync();

            Viatura.Marca = marcaUpdated;
            Viatura.Tipo = tipoUpdated;
            var viaturaUpdated = await _unitOfWork.ViaturaRepository.UpsertAsync(Viatura);
            await _unitOfWork.SaveAsync();

            return viaturaUpdated;
        }
    }
}
