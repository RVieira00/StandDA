using System;
using Stand.Domain.Models;
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
using Stand.UWP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Stand.UWP.Views.ViaturaExtra
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViaturaExtraFormPage : Page
    {
        public ViaturaExtraViewModel ViaturaExtraViewModel { get; set; }
        public ExtrasViewModel ExtrasViewModel { get; set; }

        public ViaturaExtraFormPage()
        {
            this.InitializeComponent();
            ViaturaExtraViewModel = new ViaturaExtraViewModel();
            ViaturaExtraViewModel.ViaturaViewModel = new ViaturaViewModel();
            ExtrasViewModel = new ExtrasViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ViaturaExtraViewModel.ViaturaViewModel = e.Parameter as ViaturaViewModel;

            }

            base.OnNavigatedTo(e);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            Stand.Domain.Models.ViaturaExtra novo_extra = await ViaturaExtraViewModel.AddViaturaExtraAsync(ViaturaExtraViewModel.ViaturaViewModel.Viatura.Matricula);

            if ( novo_extra != null)
            {
                ViaturaExtraViewModel.ViaturaViewModel.Viatura.ViaturasExtras.Add(novo_extra);
                Frame.Navigate(typeof(VerViaturaPage), ViaturaExtraViewModel.ViaturaViewModel);
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

        private async void AsbExtra_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var list = await ViaturaExtraViewModel.LoadExtrasByNameStartWithAsync(sender.Text);
            sender.ItemsSource = list;
        }

        private void AsbExtra_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                sender.Text = args.ChosenSuggestion.ToString();
            }
        }

        private void AsbExtra_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            sender.Text = args.SelectedItem.ToString();
        }

    }
}
