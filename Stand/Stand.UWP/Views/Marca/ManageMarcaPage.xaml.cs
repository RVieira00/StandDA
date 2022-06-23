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

namespace Stand.UWP.Views.Marca
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageMarcaPage : Page
    {
        public MarcaViewModel MarcaViewModel { get; set; }

        public ManageMarcaPage()
        {

            this.InitializeComponent();
            MarcaViewModel = new MarcaViewModel();
        }

        private void GrdMain_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        private void BrnNew_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MarcaFormPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MarcaViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteDialog = new ContentDialog
            {
                Title = "Tem a certeza que quer eliminar a marca ?",
                Content = "Se eliminar não será possível recuperá-la",
                PrimaryButtonText = "Eliminar",
                CloseButtonText = "Cancelar"
            };

            var result = await deleteDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (sender is FrameworkElement fe && fe.DataContext is Stand.Domain.Models.Marca c)
                {
                    if (c.Viaturas.Any())
                    {
                        FlyoutBase.ShowAttachedFlyout(fe);
                    }
                    else
                    {
                        MarcaViewModel.DeleteAsync(c);
                    }
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Stand.Domain.Models.Marca c)
            {
                MarcaViewModel.Marca = c;
                Frame.Navigate(typeof(MarcaFormPage), MarcaViewModel);
            }
        }
    }
}
