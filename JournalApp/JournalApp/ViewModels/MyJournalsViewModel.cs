using JournalApp.Services;
using JournalApp.Views;
using JournalModels;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JournalApp.ViewModels
{
    internal class MyJournalsViewModel : BaseViewModel
    {
        private JournalsApiClient _apiClient;

        public ObservableCollection<Journal> Journals { get; }
        public Command LoadJournalsCommand { get; }
        public Command AddJournalCommand { get; }
        public Command<Journal> JournalTapped { get; }

        public MyJournalsViewModel()
        {
            _apiClient = new JournalsApiClient();
            var currentResearcher = DependencyService.Get<Researcher>();
            Title = $"{currentResearcher.Name} - My journals";
            Journals = new ObservableCollection<Journal>();
            LoadJournalsCommand = new Command(async () => await ExecuteLoadJournalsCommand());

            JournalTapped = new Command<Journal>(OnJournalSelected);

            AddJournalCommand = new Command(OnAddJournal);
        }

        async Task ExecuteLoadJournalsCommand()
        {
            IsBusy = true;

            try
            {
                var currentResearcher = DependencyService.Get<Researcher>();
                var loadedJournals = await _apiClient.GetMyJournals(currentResearcher.Id);

                foreach (var journal in loadedJournals)
                {
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
            IsBusy = true;
            Journals.Clear();
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
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={journal.Id}");
        }
    }
}