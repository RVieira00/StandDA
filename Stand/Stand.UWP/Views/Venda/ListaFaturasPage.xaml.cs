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

namespace Stand.UWP.Views.Venda
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListaFaturasPage : Page
    {

        public VendaViewModel VendaViewModel { get; set; }

        public ListaFaturasPage()
        {
            this.InitializeComponent();
            VendaViewModel = new VendaViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            VendaViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private void GrdMain_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        private void BtnVew_Click(object sender, RoutedEventArgs e)
        {

            if (sender is FrameworkElement fe && fe.DataContext is Stand.Domain.Models.Venda c)
            {
                VendaViewModel.Venda = c;
                Frame.Navigate(typeof(VerFatura), VendaViewModel.Venda);
            }
            
        }
    }
}
