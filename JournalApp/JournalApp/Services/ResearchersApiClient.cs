using JournalApp.Utils;
using JournalModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JournalApp.Services
{
    public class ResearchersApiClient
    {
        JsonSerializerOptions _serializerOptions;
        string _apiServer = DeviceInfo.Platform == DevicePlatform.Android ? "https://rjournalsapi.creatium.mx" : "http://localhost:5045";
        public ResearchersApiClient()
        {
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }
        public async Task<List<Researcher>> GetResearchers(long subscriberId)
        {
            List<Researcher> researchers = new List<Researcher>();

            var apiUrl = $"{_apiServer}/api/Researchers/ForSubscriber/{subscriberId}/";


            using (HttpClient client = new HttpClient(HttpHelper.GetInsecureHandler()))
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                using (HttpContent content = response.Content)
                {
                    string webRequestResult = await content.ReadAsStringAsync();
                    researchers = JsonSerializer.Deserialize<List<Researcher>>(webRequestResult, _serializerOptions);

                }
            }

            return researchers;
        }

        public async Task<List<Researcher>> Subscribe(long subscriberId, long researcherId)
        {
            List<Researcher> researchers = new List<Researcher>();
            var apiUrl = $"{_apiServer}/api/Researchers";


            using (HttpClient client = new HttpClient(HttpHelper.GetInsecureHandler()))
            {
                var subscription = new Subscription { PublisherId = researcherId, SubscriberId = subscriberId };
                var jSubscription = JsonSerializer.Serialize(subscription);

                HttpContent contentPost = new StringContent(jSubscription, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, contentPost);

                using (HttpContent content = response.Content)
                {
                    string webRequestResult = await content.ReadAsStringAsync();

                    researchers = JsonSerializer.Deserialize<List<Researcher>>(webRequestResult, _serializerOptions);

                }
            }

            return researchers;
        }

        public async Task<List<Researcher>> Unsubscribe(long subscriberId, long researcherId)
        {
            List<Researcher> researchers = new List<Researcher>();
            var apiUrl = $"{_apiServer}/api/Researchers?researcherId={researcherId}&subscriberId={subscriberId}";


            using (HttpClient client = new HttpClient(HttpHelper.GetInsecureHandler()))
            {

                HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                using (HttpContent content = response.Content)
                {
                    string webRequestResult = await content.ReadAsStringAsync();

                    researchers = JsonSerializer.Deserialize<List<Researcher>>(webRequestResult, _serializerOptions);

                }
            }

            return researchers;
        }
    }
}
