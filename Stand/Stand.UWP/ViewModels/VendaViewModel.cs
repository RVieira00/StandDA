using Stand.Domain.Models;
using Stand.Domain.Services;
using Stand.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stand.UWP.ViewModels
{
    public class VendaViewModel : BindableBase
    {
        public ObservableCollection<Venda> vendas { get; set; }
        public Funcionario Funcionario { get; }
        public FuncionarioViewModel FuncionarioViewModel { get; set; }
        public VendaService VendaService { get; set; }
        
        public List<int> viaturas { get; set; } // id das Viaturas que já foram vendidas.

        public VendaViewModel()
        {
            Venda = new Venda();
            vendas = new ObservableCollection<Venda>();
            Funcionario = new Funcionario();
            FuncionarioViewModel = new FuncionarioViewModel();
            VendaService = new VendaService(new UnitOfWork());
            viaturas = new List<int>();
        }



        private string _funcionarioNome = App.FuncionarioViewModel.LoggedFuncionario.Nome;
        public string FuncionarioNome
        {
            get { return _funcionarioNome; }
            set
            {
                Set(ref _funcionarioNome, value);
                //OnPropertyChanged(nameof(Valid));
                //OnPropertyChanged(nameof(Invalid));
            }
        }

        private DateTime _data = DateTime.Now;

        public DateTime Data
        {
            get { return _data; }
            set { 
                Set(ref _data, value); 
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private string _clienteNome;
        public string ClienteNome
        {
            get { return _clienteNome; }
            set
            {
                Set(ref _clienteNome, value);
                //OnPropertyChanged(nameof(Valid));
                //OnPropertyChanged(nameof(Invalid));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private string _matriculaCarro;
        public string MatriculaCarro
        {
            get { return _matriculaCarro; }
            set { Set(ref _matriculaCarro, value);
}
        }


        private Venda _venda;
        public Venda Venda
        {
            get { return _venda; }
            set
            {
                _venda = value;
            }
        }


        internal async Task<Venda> AddVendaAsync()
        {
            return await VendaService.AddVendaAsync(FuncionarioNome, ClienteNome, MatriculaCarro, Venda);
        }


        internal async Task<Venda> UpsertAsync(Venda e)
        {
            Venda res = null;

            using (var uow = new UnitOfWork())
            {
                res = await uow.VendaRepository.UpsertAsync(e);
                await uow.SaveAsync();
            }

            return res;
        }

        internal async void DeleteAsync(Venda e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.VendaRepository.Delete(e);
                vendas.Remove(e);
                await uow.SaveAsync();
            }
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.VendaRepository.FindAllAsync();
                vendas.Clear();

                foreach (var item in list)
                {
                    item.Cliente = await uow.ClienteRepository.FindByIdAsync(item.ClienteId);
                    item.Funcionario = await uow.FuncionarioRepository.FindByIdAsync(item.FuncionarioId);
                    item.Viatura = await uow.ViaturaRepository.FindByIdAsync(item.ViaturaId);
                    item.Viatura.Marca = await uow.MarcaRepository.FindByIdAsync(item.Viatura.MarcaId);
                    vendas.Add(item);
                    viaturas.Add(item.ViaturaId);
                }
            }
        }

        internal async Task<ObservableCollection<Cliente>> LoadCleintesByNameStartWithAsync(string text)
        {
            ObservableCollection<Cliente> res;

            using (var uow = new UnitOfWork())
            {
                var list = await uow.ClienteRepository.FindAllByNameStartWithAsync(text);
                res = new ObservableCollection<Cliente>(list);
            }

            return res;
        }
    }
}
