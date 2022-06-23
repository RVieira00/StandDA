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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Stand.UWP.Views.Tipo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageTipoPage : Page
    {
        public TipoViewModel TipoViewModel { get; set; }
        public ManageTipoPage()
        {
            this.InitializeComponent();
            TipoViewModel = new TipoViewModel();
        }

        private void GrdMain_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        private void BrnNew_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TipoFormPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TipoViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteDialog = new ContentDialog
            {
                Title = "Pretende eliminar este registo?",
                Content = "Se eliminar este 'tipo' não tem como recuperá-lo",
                PrimaryButtonText = "Eliminar",
                CloseButtonText = "Cancelar"
            };

            var result = await deleteDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (sender is FrameworkElement fe && fe.DataContext is Stand.Domain.Models.Tipo t)
                {
                    if (t.Viaturas.Any())
                    {
                        FlyoutBase.ShowAttachedFlyout(fe);
                    }
                    else
                    {
                        TipoViewModel.DeleteAsync(t);
                    }
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Stand.Domain.Models.Tipo t)
            {
                TipoViewModel.Tipo = t;
                Frame.Navigate(typeof(TipoFormPage), TipoViewModel);
            }
        }
    }
}
