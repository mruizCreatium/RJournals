using JournalApp.Utils;
using JournalModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JournalApp.Services
{
    public class JournalsApiClient
    {
        JsonSerializerOptions _serializerOptions;
        string _apiServer = DeviceInfo.Platform == DevicePlatform.Android ? "https://rjournalsapi.creatium.mx" : "http://localhost:5045";
        public JournalsApiClient()
        {
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }
        public async Task<List<Journal>> GetRecentForResearcher(long subscriberId)
        {
            List<Journal> recent = new List<Journal>();

            var apiUrl = $"{_apiServer}/api/journals/RecentsForResearcher/{subscriberId}/";


            using (HttpClient client = new HttpClient(HttpHelper.GetInsecureHandler()))
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                using (HttpContent content = response.Content)
                {
                    string webRequestResult = await content.ReadAsStringAsync();
                    recent = JsonSerializer.Deserialize<List<Journal>>(webRequestResult, _serializerOptions);

                }
            }

            return recent;
        }

        public async Task<List<Journal>> GetMyJournals(long researcherId)
        {
            List<Journal> myJournals = new List<Journal>();

            var apiUrl = $"{_apiServer}/api/journals/ByResearcher/{researcherId}/";


            using (HttpClient client = new HttpClient(HttpHelper.GetInsecureHandler()))
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                using (HttpContent content = response.Content)
                {
                    string webRequestResult = await content.ReadAsStringAsync();
                    myJournals = JsonSerializer.Deserialize<List<Journal>>(webRequestResult, _serializerOptions);

                }
            }

            return myJournals;
        }

        public async Task PublishJournal(string title, Stream fileStream, string fileName)
        {
            var apiUrl = $"{_apiServer}/api/journals";


            using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(3600) })
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                var pdfStream = fileStream;
                pdfStream.Position = 0;
                StreamContent pdfPart = new StreamContent(pdfStream);
                pdfPart.Headers.Add("Content-Type", "application/pdf");
                content.Add(pdfPart, String.Format("file"), fileName);

                content.Add(new StringContent(title), "title");

                var currentResearcher = DependencyService.Get<Researcher>();
                content.Add(new StringContent(currentResearcher.Id.ToString()), "publisherId");

                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent resContent = response.Content)
                    {
                        string webRequestResult = await resContent.ReadAsStringAsync();



                    }
                }
                else
                {

                }
            }

            return;
        }
    }
}
