using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class DogProcessor
    {
        public static async Task<List<string>> LoadRandomDogImagesAsync(int numberOfImages = 0)
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

        public static async Task<List<string>> LoadDogBreedsAsync()
        {
            string url =
                "https://dog.ceo/api/breeds/list/all";



            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DogBreedsModel result = await response.Content.ReadAsAsync<DogBreedsModel>();

                    
                    return result.Breeds.Keys.ToList();                    
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<DogImageModel>> LoadDogImagesAsync(string breed, string subBreed = "", bool random = false, int numberOfImages = 0)
        {
            string url = $"https://dog.ceo/api/breed/{breed}";

            if (subBreed.Trim() != "")
            {
                url = $"https://dog.ceo/api/breed/{breed}/{subBreed}";
            }

            url += "/images";

            if (random)
            {
                url += "/random";

                if (numberOfImages > 1)
                {
                    url += $"/{numberOfImages}";
                }
            }


            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DogImagesModel result = await response.Content.ReadAsAsync<DogImagesModel>();

                    var images = new List<DogImageModel>();

                    foreach (string urls in result.ImagePaths)
                    {
                        images.Add(new DogImageModel { ImagePath = urls });
                    }
                    return images;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
