using JournalApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JournalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyJournals : ContentPage
    {
        MyJournalsViewModel _viewModel;
        public MyJournals()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MyJournalsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}