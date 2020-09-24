using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class DogProcessor
    {
        public static async Task<List<string>> LoadDogImagesAsync(int numberOfImages = 0)
        {
            string url =
                "https://dog.ceo/api/breeds/image/random";

            if (numberOfImages > 1)
                url += $"/{numberOfImages}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    if (numberOfImages > 1)
                    {
                        DogImagesModel result = await response.Content.ReadAsAsync<DogImagesModel>();
                        return result.ImagePaths;
                    } else
                    {
                        DogImageModel result = await response.Content.ReadAsAsync<DogImageModel>();

                        return new List<string> { result.ImagePath };
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<string>> LoadDogBreedsAsync(int numberOfImages = 0)
        {
            string url =
                "https://dog.ceo/api/breeds/image/random";

            if (numberOfImages > 1)
                url += $"/{numberOfImages}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    if (numberOfImages > 1)
                    {
                        DogImagesModel result = await response.Content.ReadAsAsync<DogImagesModel>();
                        return result.ImagePaths;
                    }
                    else
                    {
                        DogImageModel result = await response.Content.ReadAsAsync<DogImageModel>();

                        return new List<string> { result.ImagePath };
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


    }
}
