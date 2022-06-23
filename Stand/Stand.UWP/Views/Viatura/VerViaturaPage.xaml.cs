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
using Stand.UWP.Views.Venda;
using Stand.UWP.Views.ViaturaExtra;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Stand.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VerViaturaPage : Page
    {
        public ViaturaViewModel ViaturaViewModel { get; set; }
        public ViaturaExtraViewModel ViaturaExtraViewModel { get; set; }


        public VerViaturaPage()
        {
            this.InitializeComponent();
            ViaturaViewModel = new ViaturaViewModel();
            ViaturaExtraViewModel = new ViaturaExtraViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ViaturaViewModel = e.Parameter as ViaturaViewModel;

                ViaturaViewModel.LoadAllAsync();
            }

            base.OnNavigatedTo(e);
        }

        private void venderBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViaturaViewModel.VenderAsync();
        }

        private void BtnSell_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(VendaFormPage), ViaturaViewModel);
        }

        private void BtnAddExtra_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViaturaExtraFormPage), ViaturaViewModel);
        }
    }
}
