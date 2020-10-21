using Newtonsoft.Json;
using System;

namespace DemoLibrary.Weather
{
    public class OpenWeatherResultModel
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        
        public string Timezone { get; set; }

        [JsonProperty("timezone_offset")]
        public int TimezoneOffset { get; set; }

        public OpenWeatherCurrentModel Current { get; set; }
    }

    public class OpenWeatherCurrentModel
    {
        public DateTime DateTime { get; set; }

        [JsonProperty("temp")]
        public double Temperature { get; set; }

    }

}
