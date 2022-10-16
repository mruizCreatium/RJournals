using JournalApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JournalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalsPage : ContentPage
    {
        JournalsViewModel _viewModel;

        public JournalsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new JournalsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}