using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class ComicProcessor : IRestProcessor
    {
        public static string BaseUrl { get; private set; }

        public static async Task<ComicModel> LoadComic(int comicNumber = 0)
        {
            string url;
            if (comicNumber > 0)
                url = $"{BaseUrl}/{comicNumber}/info.0.json";
            else
                url = $"{BaseUrl}/info.0.json";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ComicModel comic = await response.Content.ReadAsAsync<ComicModel>();
                    return comic;
                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public void SetBaseUrl(string baseUrl)
        {
            BaseUrl = baseUrl;
        }
    }
}
