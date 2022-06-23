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
    public sealed partial class VerFatura : Page
    {
        public VendaViewModel VendaViewModel { get; set; }


        public VerFatura()
        {
            this.InitializeComponent();
            VendaViewModel = new VendaViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                VendaViewModel.Venda = e.Parameter as Stand.Domain.Models.Venda;
            }

            base.OnNavigatedTo(e);
        }

        public string fatura_id()
        {
            return "Fatura: " + VendaViewModel.Venda.Id;
        }
    }
}
