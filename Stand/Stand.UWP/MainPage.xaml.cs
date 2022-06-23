using Stand.UWP.ViewModels;
using Stand.UWP.Views.Login;
using Stand.UWP.Views.Cliente;
//using Stand.UWP.Views.Funcionario;
using Stand.UWP.Views.Marca;
using Stand.UWP.Views.Tipo;
using Stand.UWP.Views.Viatura;
using Stand.UWP.Views.ViaturaExtra;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Stand.UWP.Views.Funcionario;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Stand.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public FuncionarioViewModel FuncionarioViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            FuncionarioViewModel = App.FuncionarioViewModel;

        }

        public Frame AppFrame => frame;


        private void NvMain_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectItem = args.InvokedItemContainer as NavigationViewItem;

            if (selectItem != null)
            {
                switch (selectItem.Tag)
                {
                    case "funcionarios":
                        AppFrame.Navigate(typeof(ManageFuncionarioPage));
                        break;
                    case "clientes":
                        AppFrame.Navigate(typeof(ManageClientePage));
                        break;
                    case "marcas":
                        AppFrame.Navigate(typeof(ManageMarcaPage));
                        break;
                    case "extras":
                        AppFrame.Navigate(typeof(Stand.UWP.Views.Extra.ExtrasPage));
                        break;
                    case "tipos":
                        AppFrame.Navigate(typeof(ManageTipoPage));
                        break;
                    case "viaturas":
                        AppFrame.Navigate(typeof(ManageViaturaPage));
                        break;
                    case "faturas":
                        AppFrame.Navigate(typeof(Stand.UWP.Views.Venda.ListaFaturasPage));
                        break;
                }
            }
        }

        private async void AbbLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginDialog dlg = new LoginDialog();
            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                if (App.FuncionarioViewModel.IsLogged)
                {
                    AppFrame.Navigate(typeof(ManageViaturaPage));
                }
            }

        }

        private void mySearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            //this.Frame.Navigate(typeof(SearchResultsPage1), args.QueryText);
        }

        //Fechar a aplicação se for preciso no comand-bar
        //private void AppBarButton_Click(object sender, RoutedEventArgs e)
        //{
        //    CoreApplication.Exit();
        //}

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // only to save time doing login in tests
            FuncionarioViewModel.Funcionario.Email = "admin@admin.com";
            FuncionarioViewModel.Funcionario.Password = "admin";
            await FuncionarioViewModel.DoLoginAsync();

            if (App.FuncionarioViewModel.IsLogged)
            {
                AppFrame.Navigate(typeof(ManageViaturaPage));
            }
        }

        private void AbbLogout_Click(object sender, RoutedEventArgs e)
        {
            FuncionarioViewModel.DoLogout();
            AppFrame.BackStack.Clear();
            AppFrame.Content = null;
        }

        private async void AbbRegistar_Click(object sender, RoutedEventArgs e)
        {
            RegisterDialog dlg = new RegisterDialog();
            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                if (App.FuncionarioViewModel.IsLogged)
                {
                    AppFrame.Navigate(typeof(ManageTipoPage));
                }
            }
        }

        private void frame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {

        }
    }
}
