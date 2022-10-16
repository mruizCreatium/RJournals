using JournalApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JournalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResearchersPage : ContentPage
    {
        ResearchersViewModel _viewModel;
        public ResearchersPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ResearchersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}