using Stand.UWP.ViewModels;
using Stand.UWP.Views.Login;
using Stand.UWP.Views.Tipo;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Stand.UWP.Views.Funcionario
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageFuncionarioPage : Page
    {
        public FuncionarioViewModel FuncionarioViewModel { get; set; }

        public ManageFuncionarioPage()
        {
            this.InitializeComponent();
            FuncionarioViewModel = new FuncionarioViewModel();

        }


        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteDialog = new ContentDialog
            {
                Title = "Tem a certeza que quer eliminar o funcionario ?",
                Content = "Se eliminar não será possível recuperá-lo",
                PrimaryButtonText = "Eliminar",
                CloseButtonText = "Cancelar"
            };

            var result = await deleteDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (sender is FrameworkElement fe && fe.DataContext is Stand.Domain.Models.Funcionario c)
                {
                    if (c == null)
                    {
                        FlyoutBase.ShowAttachedFlyout(fe);
                    }
                    else
                    {
                        FuncionarioViewModel.DeleteAsync(c);
                    }
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Stand.Domain.Models.Funcionario c)
            {
                FuncionarioViewModel.Funcionario = c;
                Frame.Navigate(typeof(FuncionarioFormPage), FuncionarioViewModel);
            }
        }


        private void GrdMain_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FuncionarioViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }


        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (await FuncionarioViewModel.UpsertAsync() != null)
            {
                Frame.GoBack();
            }
            else
            {
                FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
            }
        }

        private async void BrnNew_Click(object sender, RoutedEventArgs e)
        {
            RegisterDialog dlg = new RegisterDialog();
            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                if (App.FuncionarioViewModel.IsLogged)
                {
                    //AppFrame.Navigate(typeof(ManageTipoPage));
                    Frame.Navigate(typeof(ManageTipoPage));
                }
            }
        }
    }
}
