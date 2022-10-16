using JournalApp.Services;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JournalApp.ViewModels
{
    public class NewJournalViewModel : BaseViewModel
    {
        private string journalTitle;
        private string fileName;
        private Stream pdfFile;
        private JournalsApiClient _apiClient;

        public NewJournalViewModel()
        {
            _apiClient = new JournalsApiClient();
            PickFileCommand = new Command(OnPickFile);
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(journalTitle)
                && pdfFile != null;
        }

        public string JournalTitle
        {
            get => journalTitle;
            set => SetProperty(ref journalTitle, value);
        }

        public Stream PdfFile
        {
            get => pdfFile;
            set => SetProperty(ref pdfFile, value);
        }


        public string FileName
        {
            get => fileName;
            set => SetProperty(ref fileName, value);
        }

        public Command PickFileCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnPickFile()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select your PDF file",
                FileTypes = FilePickerFileType.Pdf
            });

            var file = await result.OpenReadAsync();

            if (file != null)
            {
                //byte[] data = new byte[file.Length];
                //file.Read(data, 0, Convert.ToInt32(file.Length));

                this.FileName = result.FileName;
                this.PdfFile = file;
            }
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            // This will pop the current page off the navigation stack

            await _apiClient.PublishJournal(JournalTitle, PdfFile, FileName);

            await Shell.Current.GoToAsync("..");
        }
    }
}
