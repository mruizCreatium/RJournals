using JournalApp.ViewModels;
using JournalModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JournalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewJournalPage : ContentPage
    {
        public Journal Journal { get; set; }

        public NewJournalPage()
        {
            InitializeComponent();
            BindingContext = new NewJournalViewModel();
        }
    }
}