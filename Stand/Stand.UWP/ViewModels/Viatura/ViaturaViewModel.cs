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
    public class ViaturaViewModel : BindableBase
    {
       

        private Viatura _viatura;
        public Viatura Viatura
        {
            get { return _viatura; }
            set
            {
                _viatura = value;

            }
        }

        public bool Valid
        {
            get
            {
                return _viatura != null;
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        public ViaturaViewModel()
        {
            Viatura = new Viatura();
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
                await uow.SaveAsync();
            }
        }

        public async void LoadViaturaAsync(int id)
        {
            using (var uow = new UnitOfWork())
            {
                Viatura = await uow.ViaturaRepository.FindByIdAsync(id);
            }
        }
    }
}
