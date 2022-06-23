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
    public class MarcaViewModel : BindableBase
    {
        public ObservableCollection<Marca> Marcas { get; set; }

        private Marca _marca;
        public Marca Marca
        {
            get { return _marca; }
            set
            {
                _marca = value;
                MarcaName = _marca?.Nome;
            }
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

        public bool Valid
        {
            get
            {
                bool res = !string.IsNullOrWhiteSpace(MarcaName);
                return res;
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        public MarcaViewModel()
        {
            Marca = new Marca();
            Marcas = new ObservableCollection<Marca>();
        }

        internal async Task<Marca> UpsertAsync()
        {
            Marca res = null;

            using (var uow = new UnitOfWork())
            {
                Marca.Nome = MarcaName;
                res = await uow.MarcaRepository.UpsertAsync(Marca);
                await uow.SaveAsync();
            }

            return res;
        }

        internal async void DeleteAsync(Marca e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.MarcaRepository.Delete(e);
                Marcas.Remove(e);
                await uow.SaveAsync();
            }
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.MarcaRepository.FindAllAsync();
                Marcas.Clear();

                foreach (var item in list)
                {
                    Marcas.Add(item);
                }
            }
        }
    }
}