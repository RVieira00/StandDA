using Stand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Stand.Domain.Services
{
    public class VendaService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public VendaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Venda> AddVendaAsync(string FuncionarioNome, string ClienteNome, string Matricula, Venda venda)
        {
            var funcionarioUpdated = await _unitOfWork.FuncionarioRepository.FindByNameAsync(FuncionarioNome);
            await _unitOfWork.SaveAsync();

            var clienteUpdated = await _unitOfWork.ClienteRepository.FindByNameAsync(ClienteNome);
            await _unitOfWork.SaveAsync();

            var carroUpdated = await _unitOfWork.ViaturaRepository.FindByMatriculaAsync(Matricula);
            await _unitOfWork.SaveAsync();

            venda.Funcionario = funcionarioUpdated;
            venda.Cliente = clienteUpdated;
            venda.Viatura = carroUpdated;
            var vendaUpdated = await _unitOfWork.VendaRepository.UpsertAsync(venda);
            await _unitOfWork.SaveAsync();

            return vendaUpdated;
        }

    }
}
