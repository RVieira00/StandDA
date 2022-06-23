using Stand.Domain.Models;
using Stand.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Stand.UWP.Views.Login
{
    public sealed partial class LoginDialog : ContentDialog
    {
        public FuncionarioViewModel FuncionarioViewModel { get; set; }
        public LoginDialog()
        {
            this.InitializeComponent();
            FuncionarioViewModel = App.FuncionarioViewModel;
            FuncionarioViewModel.Funcionario = new Stand.Domain.Models.Funcionario();
            FuncionarioViewModel.ShowError = false;

        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var deferral = args.GetDeferral();
            args.Cancel = !await FuncionarioViewModel.DoLoginAsync();
            deferral.Complete();

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
