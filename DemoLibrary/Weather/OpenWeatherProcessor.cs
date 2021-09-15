using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DemoLibrary.Weather
{
    public class OpenWeatherProcessor
    {
        private static readonly Lazy<OpenWeatherProcessor> lazy = new Lazy<OpenWeatherProcessor>(() => new OpenWeatherProcessor());

        public static OpenWeatherProcessor Instance { get { return lazy.Value; } }

        public IConfiguration Configuration { get; }

        public string BaseURL { get; set; }
        public string EndPoint { get; set; }

        private string longUrl;

        private string apiKey;

        private OpenWeatherProcessor()
        {
            BaseURL = $"https://api.openweathermap.org/data/2.5";
            EndPoint = $"/weather?";

            apiKey = AppSecretConfigurations.Instance.GetSecret("OWApiKey");            
        }

        public async Task<OpenWeatherOneCallModel> GetOneCallAsync()
        {
            EndPoint = $"/onecall?";

            /// Src : https://stackoverflow.com/a/14517976/503842
            var uriBuilder = new UriBuilder($"{BaseURL}{EndPoint}");

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["lat"] = "46.5668"; // Shawinigan
            query["lon"] = "-72.7491";
            query["units"] = "metric";
            query["appid"] = apiKey;
            

            uriBuilder.Query = query.ToString();
            longUrl = uriBuilder.ToString();

            return await doOneCall();
        }

        public async Task<OWCurrentWeaterModel> GetCurrentWeather()
        {
            EndPoint = $"/weather?";

            /// Src : https://stackoverflow.com/a/14517976/503842
            var uriBuilder = new UriBuilder($"{BaseURL}{EndPoint}");

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["q"] = "Shawinigan"; // Shawinigan
            query["units"] = "metric";
            query["appid"] = apiKey;

            uriBuilder.Query = query.ToString();
            longUrl = uriBuilder.ToString();

            return await doCurrentWeatherCall();
        }

        private async Task<OpenWeatherOneCallModel> doOneCall()
        {

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(longUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    OpenWeatherOneCallModel result = await response.Content.ReadAsAsync<OpenWeatherOneCallModel>();
                    return result;
                }

                return null;
            }
        }

        private async Task<OWCurrentWeaterModel> doCurrentWeatherCall()
        {

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(longUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    OWCurrentWeaterModel result = await response.Content.ReadAsAsync<OWCurrentWeaterModel>();
                    return result;
                }

                return null;
                
            }
        }
    }
}
