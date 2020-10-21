using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DemoLibrary.Weather
{
    public class OpenWeatherProcessor
    {
        private static readonly Lazy<OpenWeatherProcessor> lazy = new Lazy<OpenWeatherProcessor>(() => new OpenWeatherProcessor());

        public static OpenWeatherProcessor Instance { get { return lazy.Value; } }

        public string BaseURL { get; set; }
        public string EndPoint { get; set; }
        
        private UriBuilder uriBuilder;
        private string longUrl;

        private bool lastCallOk = false;
        OpenWeatherResultModel cacheResult;

        private OpenWeatherProcessor()
        {
            BaseURL = $"https://api.openweathermap.org/data/2.5";
            EndPoint = $"/onecall?";

            /// Src : https://stackoverflow.com/a/14517976/503842
            uriBuilder = new UriBuilder($"{BaseURL}{EndPoint}");

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["lat"] = "46.5619"; // Shawinigan
            query["long"] = "-72.7435";
            query["units"] = "metric";
            query["appid"] = "52d0d5f54ec34076e9dfd40dc192a12a";

            longUrl = uriBuilder.ToString();
        }

        public async Task<OpenWeatherResultModel> GetOneCallAsync()
        {
            if (lastCallOk && cacheResult != null) return cacheResult;


            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(longUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    lastCallOk = true;
                    OpenWeatherResultModel result = await response.Content.ReadAsAsync<OpenWeatherResultModel>();
                    cacheResult = result;
                    return result;
                    
                } else
                {
                    lastCallOk = false;
                    cacheResult = null;
                    return null;
                }
            }
        }
    }
}
