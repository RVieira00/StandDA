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
    public class ViaturaViewModel : BindableBase
    {

        public ObservableCollection<Viatura> Viaturas { get; set; }
        public ViaturaService ViaturaService { get; set; }

        public VendaViewModel VendaViewModel { get; set; }

        public ViaturaViewModel()
        {
            Viatura = new Viatura();
            Viaturas = new ObservableCollection<Viatura>();
            ViaturaService = new ViaturaService(new UnitOfWork());
            VendaViewModel = new VendaViewModel();
        }

        private string _marcaName;
        public string MarcaName
        {
            get { return _marcaName; }
            set
            {
                Set(ref _marcaName, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _tipoName;
        public string TipoName
        {
            get { return _tipoName; }
            set
            {
                Set(ref _tipoName, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private Viatura _viatura;
        public Viatura Viatura
        {
            get { return _viatura; }
            set
            {
                _viatura = value;
                TipoName = _viatura.Tipo?.Nome;
                MarcaName = _viatura.Marca?.Nome;
            }
        }
        public bool Valid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(MarcaName) && !string.IsNullOrWhiteSpace(TipoName);
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        internal async Task<Viatura> AddViaturaAsync()
        {
            return await ViaturaService.AddViaturaAsync(MarcaName, TipoName, Viatura);
        }

        internal async Task<Viatura> UpsertAsync()
        {
            Viatura res = null;

            using (var uow = new UnitOfWork())
            {
                res = await uow.ViaturaRepository.UpsertAsync(_viatura);
                await uow.SaveAsync();
            }

            return res;
        }

        internal async void DeleteAsync(Viatura e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.ViaturaRepository.Delete(e);
                Viaturas.Remove(e);
                await uow.SaveAsync();
            }
        }

        internal async void VenderAsync()
        {
            using (var uow = new UnitOfWork())
            {
                uow.ViaturaRepository.Delete(_viatura);
                await uow.SaveAsync();
            }
        }

        public async void LoadAsync(int id)
        {
            using (var uow = new UnitOfWork())
            {
                Viatura = await uow.ViaturaRepository.FindByIdAsync(id);
            }
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                VendaViewModel.LoadAllAsync();
                var lista_carros = await uow.ViaturaRepository.FindAllAsync();
                Viaturas.Clear();

                foreach (var carro in lista_carros)
                {
                    if (VendaViewModel.viaturas.Contains(carro.Id)) // verificar se o carro já foi vendido
                        continue;
                    
                    var lista_extras = await uow.ViaturaExtraRepository.FindByViaturaIdAsync(carro.Id);

                    foreach(var extra in lista_extras)
                    {
                        ViaturaExtra viaturaExtra = new ViaturaExtra();
                        viaturaExtra.Viatura = carro;
                        viaturaExtra.ViaturaId = carro.Id;
                        viaturaExtra.Extra = extra;
                        viaturaExtra.ExtraId = extra.Id;

                        carro.ViaturasExtras.Add(viaturaExtra);
                    }

                    Viaturas.Add(carro);
                }
            }
        }

        internal async Task<ObservableCollection<Tipo>> LoadTiposByNameStartWithAsync(string text)
        {
            ObservableCollection<Tipo> res;

            using (var uow = new UnitOfWork())
            {
                var list = await uow.TipoRepository.FindAllByNameStartWithAsync(text);
                res = new ObservableCollection<Tipo>(list);
            }

            return res;
        }

        internal async Task<ObservableCollection<Marca>> LoadMarcasByNameStartWithAsync(string text)
        {
            ObservableCollection<Marca> res;

            using (var uow = new UnitOfWork())
            {
                var list = await uow.MarcaRepository.FindAllByNameStartWithAsync(text);
                res = new ObservableCollection<Marca>(list);
            }

            return res;
        }
    }
}
