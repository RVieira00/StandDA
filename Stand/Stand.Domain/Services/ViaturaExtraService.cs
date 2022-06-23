using Stand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stand.Domain.Services
{
    public class ViaturaExtraService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public ViaturaExtraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ViaturaExtra> AddViaturaExtraAsync(string ExtraNome, string matricula, ViaturaExtra viaturaExtra)
        {
            
            var extraUpdated = await _unitOfWork.ExtraRepository.FindByNameAsync(ExtraNome);
            await _unitOfWork.SaveAsync();


            var viaturaUpdated = await _unitOfWork.ViaturaRepository.FindByMatriculaAsync(matricula);
            await _unitOfWork.SaveAsync();

            viaturaExtra.Extra = extraUpdated;
            viaturaExtra.Viatura = viaturaUpdated;
            var viaturaextraUpdated = await _unitOfWork.ViaturaExtraRepository.UpsertAsync(viaturaExtra);
            await _unitOfWork.SaveAsync();

            return viaturaextraUpdated;
        }
    }
}