using JournalApp.Services;
using JournalApp.Views;
using JournalModels;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JournalApp.ViewModels
{
    internal class JournalsViewModel : BaseViewModel
    {
        string _mediaServer = DeviceInfo.Platform == DevicePlatform.Android ? "https://rjournalsapi.creatium.mx" : "http://localhost:5045";
        private Journal _selectedJournal;
        private JournalsApiClient _apiClient;

        public ObservableCollection<Journal> Journals { get; }
        public Command LoadJournalsCommand { get; }
        public Command AddJournalCommand { get; }
        public Command<Journal> JournalTapped { get; }

        public JournalsViewModel()
        {
            _apiClient = new JournalsApiClient();
            var currentResearcher = DependencyService.Get<Researcher>();
            Title = $"{currentResearcher.Name} - Last journal entries";
            Journals = new ObservableCollection<Journal>();
            LoadJournalsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            JournalTapped = new Command<Journal>(OnJournalSelected);

            AddJournalCommand = new Command(OnAddJournal);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var currentResearcher = DependencyService.Get<Researcher>();
                var loadedJournals = await _apiClient.GetRecentForResearcher(currentResearcher.Id);

                foreach (var journal in loadedJournals)
                {
                    //journal.Researcher.MediaServer = _mediaServer;
                    Journals.Add(journal);
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
            Journals.Clear();
            IsBusy = true;
            SelectedJournal = null;
        }

        public Journal SelectedJournal
        {
            get => _selectedJournal;
            set
            {
                SetProperty(ref _selectedJournal, value);
                OnJournalSelected(value);
            }
        }

        private async void OnAddJournal(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewJournalPage));
        }

        async void OnJournalSelected(Journal journal)
        {
            if (journal == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(JournalReaderPage)}");
        }
    }
}