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
    public class ExtrasViewModel : BindableBase
    {
        public ObservableCollection<Extra> Extras { get; set; }

        private Extra _extra;
        public Extra Extra
        {
            get { return _extra; }
            set
            {
                _extra = value;
                ExtraNome = _extra?.Nome;
            }
        }

        private string _extraName;
        public string ExtraNome
        {
            get { return _extraName; }
            set
            {
                Set(ref _extraName, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }

        }

        public bool Valid
        {
            get
            {
                bool res = !string.IsNullOrWhiteSpace(ExtraNome);
                return res;
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        public ExtrasViewModel()
        {
            Extra = new Extra();
            Extras = new ObservableCollection<Extra>();
        }

        internal async Task<Extra> UpsertAsync()
        {
            Extra res = null;

            using (var uow = new UnitOfWork())
            {
                Extra.Nome = ExtraNome;
                res = await uow.ExtraRepository.UpsertAsync(Extra);
                await uow.SaveAsync();
            }

            return res;
        }

        internal async void DeleteAsync(Extra e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.ExtraRepository.Delete(e);
                Extras.Remove(e);
                await uow.SaveAsync();
            }
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.ExtraRepository.FindAllAsync();
                Extras.Clear();

                foreach (var item in list)
                {
                    Extras.Add(item);
                }
            }
        }
    }
}
