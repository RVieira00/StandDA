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
    public class FuncionarioViewModel : BindableBase
    {

        public ObservableCollection<Funcionario> Funcionarios { get; set; }

        private Funcionario _funcionario;
        public Funcionario Funcionario
        {
            get { return _funcionario; }
            set
            {
                _funcionario = value;
            }
        }

        public FuncionarioViewModel()
        {
            Funcionario = new Funcionario();
            Funcionarios = new ObservableCollection<Funcionario>();

        }


        private Funcionario _loggedFuncionario;
        public Funcionario LoggedFuncionario
        {
            get { return _loggedFuncionario; }
            set
            {
                _loggedFuncionario = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLogged));
                OnPropertyChanged(nameof(IsNotLogged));
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool Valid
        {
            get
            {
                bool res = _loggedFuncionario != null;
                return res;
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        public bool IsLogged
        {
            get => LoggedFuncionario != null;
        }

        public bool IsNotLogged
        {
            get => !IsLogged;
        }
        private bool _showError;

        public bool ShowError
        {
            get { return _showError; }
            set
            {
                _showError = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdmin => LoggedFuncionario != null && LoggedFuncionario.isAdmin;

        internal async Task<bool> DoLoginAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var funcionario = await uow.FuncionarioRepository.FindByEmailAndPassword(Funcionario.Email, Funcionario.Password);

                LoggedFuncionario = funcionario;
                ShowError = funcionario == null;
            }

            return !ShowError;
        }

        public void DoLogout()
        {
            LoggedFuncionario = new Funcionario();
            LoggedFuncionario = null;
        }

        internal async Task<bool> RegisterAsync()
        {
            bool res = true;

            using (var uow = new UnitOfWork())
            {
                var funcionario = await uow.FuncionarioRepository.FindByEmail(Funcionario.Email);

                if (funcionario == null)
                {
                    funcionario = uow.FuncionarioRepository.Create(Funcionario);
                    await uow.SaveAsync();
                    LoggedFuncionario = funcionario;
                    res = false;
                }
            }

            ShowError = res;
            return !ShowError;
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.FuncionarioRepository.FindAllAsync();
                Funcionarios.Clear();

                foreach (var item in list)
                {
                    Funcionarios.Add(item);
                }
            }
        }
        internal async void DeleteAsync(Funcionario e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.FuncionarioRepository.Delete(e);
                Funcionarios.Remove(e);
                await uow.SaveAsync();
            }
        }

        internal async Task<Funcionario> UpsertAsync()
        {
            Funcionario res = null;

            using (var uow = new UnitOfWork())
            {
                
                res = await uow.FuncionarioRepository.UpsertAsync(Funcionario);
                await uow.SaveAsync();
            }

            return res;
        }
    }
}

