using JournalApp.Services;
using JournalModels;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JournalApp.ViewModels
{
    internal class ResearchersViewModel : BaseViewModel
    {
        string _mediaServer = DeviceInfo.Platform == DevicePlatform.Android ? "https://rjournalsapi.creatium.mx" : "http://localhost:5045";
        private Subscription _selectedSubscription;
        private ResearchersApiClient _apiClient;

        public ObservableCollection<Researcher> Researchers { get; }

        public Command LoadSubscriptionsCommand { get; }

        public Command<Researcher> AddSubscriptionCommand { get; }

        public Command<Researcher> RemoveSubscriptionCommand { get; }

        public ResearchersViewModel()
        {
            _apiClient = new ResearchersApiClient();
            var currentResearcher = DependencyService.Get<Researcher>();
            Title = $"{currentResearcher.Name} - Researchers";
            Researchers = new ObservableCollection<Researcher>();
            LoadSubscriptionsCommand = new Command(async () => await ExecuteLoadSubscriptionsCommand());

            RemoveSubscriptionCommand = new Command<Researcher>(OnDeleteSubscription);

            AddSubscriptionCommand = new Command<Researcher>(OnAddSubscription);
        }

        async Task ExecuteLoadSubscriptionsCommand()
        {
            IsBusy = true;

            try
            {
                var currentResearcher = DependencyService.Get<Researcher>();
                var loadedResearchers = await _apiClient.GetResearchers(currentResearcher.Id);
                Researchers.Clear();
                foreach (var researcher in loadedResearchers)
                {
                    Researchers.Add(researcher);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }


        private async void OnAddSubscription(Researcher researcher)
        {
            if (researcher == null)
                return;
            var currentResearcher = DependencyService.Get<Researcher>();
            await _apiClient.Subscribe(currentResearcher.Id, researcher.Id);
            researcher.Subscribed = true;
            var ix = Researchers.IndexOf(researcher);
            Researchers.Remove(researcher);
            Researchers.Insert(ix, researcher);
            OnPropertyChanged("Researchers");
        }

        async void OnDeleteSubscription(Researcher researcher)
        {
            if (researcher == null)
                return;
            var currentResearcher = DependencyService.Get<Researcher>();
            await _apiClient.Unsubscribe(currentResearcher.Id, researcher.Id);
            researcher.Subscribed = false;
            var ix = Researchers.IndexOf(researcher);
            Researchers.Remove(researcher);
            Researchers.Insert(ix, researcher);
            OnPropertyChanged("Researchers");
        }
    }
}