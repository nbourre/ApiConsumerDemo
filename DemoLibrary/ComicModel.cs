using Newtonsoft.Json;

namespace DemoLibrary
{
    public class ComicModel
    {
        [JsonProperty("num")]
        public int Number { get; set; }

        [JsonProperty("img")]
        public string ImagePath { get; set; }

        public string Title { get; set; }
    }
}
