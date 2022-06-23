using Stand.UWP.ViewModels;
using Stand.UWP.Views.Viatura;
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
    public sealed partial class VendaFormPage : Page
    {
        public VendaViewModel VendaViewModel { get; set; }
        ViaturaViewModel ViaturaViewModel { get; set; }
        public FuncionarioViewModel FuncionarioViewModel { get; set; }
        public ClienteViewModel ClienteViewModel { get; set; }

        public static DateTime Today { get; }
        DateTime thisDay = DateTime.Today;

        public VendaFormPage()
        {
            this.InitializeComponent();
            ClienteViewModel = new ClienteViewModel();
            VendaViewModel = new VendaViewModel();
            FuncionarioViewModel = App.FuncionarioViewModel;
            ViaturaViewModel = new ViaturaViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ViaturaViewModel = e.Parameter as ViaturaViewModel;
                VendaViewModel.MatriculaCarro = ViaturaViewModel.Viatura.Matricula;
                //VendaViewModel.Venda.Viatura = ViaturaViewModel.Viatura;
            }

            base.OnNavigatedTo(e);
        }

        public async void BtnSave_ClickAsync(object sender, RoutedEventArgs e)
        {
            
            if (await VendaViewModel.AddVendaAsync() != null)
            {
                Frame.Navigate(typeof(VerFatura), VendaViewModel);
            }
            else
            {
                FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void AsbCliente_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var list = await VendaViewModel.LoadCleintesByNameStartWithAsync(sender.Text);
            sender.ItemsSource = list;
        }

        private void AsbCliente_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                sender.Text = args.ChosenSuggestion.ToString();
                //VendaViewModel.Venda.Cliente.CartaConducao = args.ChosenSuggestion.CartaConducao;
                //VendaViewModel.Venda.Cliente.CartaConducao = args.ChosenSuggestion.CartaConducao;
                //VendaViewModel.Venda.Cliente.CartaConducao = args.ChosenSuggestion.CartaConducao;
                //VendaViewModel.Venda.Cliente.CartaConducao = args.ChosenSuggestion.CartaConducao;
            }
        }

        private void AsbCliente_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            sender.Text = args.SelectedItem.ToString();
            VendaViewModel.Venda.Cliente = args.SelectedItem as Stand.Domain.Models.Cliente;
        }

        public string preco_total()
        {
            //ViaturaViewModel.Viatura.PrecoBasse + ViaturaViewModel.Viatura.TotalExtras()
            return ViaturaViewModel.Viatura.PrecoBasse + "€";
        }
    }
}
