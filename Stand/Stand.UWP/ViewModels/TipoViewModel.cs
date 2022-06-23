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
    public class TipoViewModel : BindableBase
    {
        public ObservableCollection<Tipo> Tipos { get; set; }

        private Tipo _tipo;
        public Tipo Tipo
        {
            get { return _tipo; }
            set
            {
                _tipo = value;
                TipoNome = _tipo?.Nome;
            }
        }

        private string _tipoNome;
        public string TipoNome
        {
            get { return _tipoNome; }
            set
            {
                Set(ref _tipoNome, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }

        }

        public bool Valid
        {
            get
            {
                bool res = !string.IsNullOrWhiteSpace(TipoNome);
                return res;
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        public TipoViewModel()
        {
            Tipo = new Tipo();
            Tipos = new ObservableCollection<Tipo>();
        }

        internal async Task<Tipo> UpsertAsync()
        {
            Tipo res = null;

            using (var uow = new UnitOfWork())
            {
                Tipo.Nome = TipoNome;
                res = await uow.TipoRepository.UpsertAsync(Tipo);
                await uow.SaveAsync();
            }

            return res;
        }

        internal async void DeleteAsync(Tipo e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.TipoRepository.Delete(e);
                Tipos.Remove(e);
                await uow.SaveAsync();
            }
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.TipoRepository.FindAllAsync();
                Tipos.Clear();

                foreach (var item in list)
                {
                    Tipos.Add(item);
                }
            }
        }
    }
}
