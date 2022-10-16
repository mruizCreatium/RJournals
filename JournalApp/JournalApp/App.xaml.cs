using JournalModels;
using Xamarin.Forms;

namespace JournalApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //Fixing current researcher for demo porpouses, a login would be required to implement dynamic profile loading
            DependencyService.RegisterSingleton<Researcher>(
                new Researcher
                {
                    Id = 1,
                    Institution = "CREATIUM Research Intitute",
                    Name = "Manuel Ruiz",
                    ResearchArea = "Computer Science",
                    PictureUrl = "researchers/picture_1.jpg"
                });
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
