using Stand.Domain.Models;
using Stand.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stand.UWP.ViewModels
{
    public class ClienteViewModel : BindableBase
    {
        public ObservableCollection<Cliente> Clientes { get; set; }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set
            {
                _cliente = value;
            }
        }


        public bool Valid
        {
            get
            {
                bool res = _cliente != null;
                return res;
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        public ClienteViewModel()
        {
            Cliente = new Cliente();
            Clientes = new ObservableCollection<Cliente>();
        }

        internal async Task<Cliente> UpsertAsync()
        {
            Cliente res = null;

            using (var uow = new UnitOfWork())
            {
                //Cliente.Nome = ClienteNome;
                res = await uow.ClienteRepository.UpsertAsync(Cliente);
                await uow.SaveAsync();
            }

            return res;
        }

        internal async void DeleteAsync(Cliente e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.ClienteRepository.Delete(e);
                Clientes.Remove(e);
                await uow.SaveAsync();
            }
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.ClienteRepository.FindAllAsync();
                Clientes.Clear();

                foreach (var item in list)
                {
                    Clientes.Add(item);
                }
            }
        }
    }
}

