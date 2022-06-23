using Stand.Domain.Models;
using Stand.Domain.Services;
using Stand.Infrastructure;
using Stand.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stand.UWP.ViewModels
{
    public class ViaturaExtraViewModel : BindableBase
    {
        public ObservableCollection<ViaturaExtra> ViaturaExtras { get; set; }
        public ViaturaExtraService ViaturaExtraService { get; set; }

        public ViaturaViewModel ViaturaViewModel { get; set; }

        public ViaturaExtraViewModel()
        {
            ViaturaExtra = new ViaturaExtra();
            ViaturaExtras = new ObservableCollection<ViaturaExtra>();
            ViaturaExtraService = new ViaturaExtraService(new UnitOfWork());
        }


        private string _extraNome;
        public string ExtraNome
        {
            get { return _extraNome; }
            set
            {
                Set(ref _extraNome, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }


        private string _matricula;
        public string Matricula
        {
            get { return _matricula; }
            set
            {
                Set(ref _matricula, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }


        private ViaturaExtra _viaturaextra;
        public ViaturaExtra ViaturaExtra
        {
            get { return _viaturaextra; }
            set
            {
                _viaturaextra = value;
                ExtraNome = _viaturaextra.Extra?.Nome;
                Matricula = _viaturaextra.Viatura?.Matricula;
            }
        }

        public bool Valid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ExtraNome) && !string.IsNullOrWhiteSpace(Matricula);
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }


        internal async Task<ViaturaExtra> AddViaturaExtraAsync(string mat)
        {
            
            return await ViaturaExtraService.AddViaturaExtraAsync(ExtraNome, mat, ViaturaExtra);
        }

        internal async Task<ViaturaExtra> UpsertAsync()
        {
            ViaturaExtra res = null;

            using (var uow = new UnitOfWork())
            {
                res = await uow.ViaturaExtraRepository.UpsertAsync(_viaturaextra);
                await uow.SaveAsync();
            }

            return res;
        }

        internal async void DeleteAsync(ViaturaExtra e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.ViaturaExtraRepository.Delete(e);
                await uow.SaveAsync();
            }
        }

        public async void LoadAsync(int id)
        {
            using (var uow = new UnitOfWork())
            {
                ViaturaExtra = await uow.ViaturaExtraRepository.FindByIdAsync(id);
            }
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.ViaturaExtraRepository.FindAllAsync();
                ViaturaExtras.Clear();

                foreach (var item in list)
                {
                    ViaturaExtras.Add(item);
                }
            }
        }

        internal async Task<ObservableCollection<Extra>> LoadExtrasByNameStartWithAsync(string text)
        {
            ObservableCollection<Extra> res;

            using (var uow = new UnitOfWork())
            {
                var list = await uow.ExtraRepository.FindAllByNameStartWithAsync(text);
                res = new ObservableCollection<Extra>(list);
            }

            return res;
        }

        internal async Task<ObservableCollection<Extra>> LoadExtrasByViaturaIdAsync(int viatura_id)
        {
            ObservableCollection<Extra> res;

            using (var uow = new UnitOfWork())
            {
                var list = await uow.ViaturaExtraRepository.FindByViaturaIdAsync(viatura_id);
                res = new ObservableCollection<Extra>(list);
            }

            return res;
        }
    }
}