using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoLibrary
{
    public class DogImagesModel
    {
        [JsonProperty("message")]
        public List<String> ImagePaths { get; set; }
    }

    public class DogImageModel
    {
        private string imagePath;

        [JsonProperty("message")]
        public string ImagePath { 
            get => imagePath;
            set
            {
                imagePath = value;

                var temp = new Uri(imagePath);

                Breed = temp.Segments[2].Replace("/", null);

                if (Breed.Contains("-"))
                {
                    var split = Breed.Split("-");
                    Breed = split[0];
                    SubBreed = split[1];
                }
            }
        }

        public string Breed { get; set; }
        public string SubBreed { get; set; }
    }

    /// <summary>
    /// Sources
    /// https://stackoverflow.com/questions/15789539/deserialize-array-of-key-value-pairs-using-json-net
    /// https://www.newtonsoft.com/json/help/html/SerializingCollections.htm#DeserializingDictionaries
    /// </summary>
    public class DogBreedsModel
    {
        [JsonProperty("message")]
        public Dictionary<string, List<string>> Breeds { get; set; }
    }
}
