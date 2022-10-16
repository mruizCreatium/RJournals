using JournalApp.Views;
using Xamarin.Forms;

namespace JournalApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(JournalReaderPage), typeof(JournalReaderPage));
            Routing.RegisterRoute(nameof(NewJournalPage), typeof(NewJournalPage));
        }

    }
}
