using ApiConsumerDemo.Commands;
using DemoLibrary.Weather;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiConsumerDemo.ViewModels
{
    public class OpenWeatherViewModel: BaseViewModel
    {
        private OpenWeatherOneCallModel owResult;

        public OpenWeatherOneCallModel OWResult
        {
            get { return owResult; }
            set { 
                owResult = value;
                OnPropertyChanged();
            }
        }

        private string rawText;

        public string RawText
        {
            get { return rawText; }
            set {
                rawText = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand<string> GetCurrentWeatherCommand { get; set; }
        public DelegateCommand<string> GetOneCallCommand { get; set; }

        public OpenWeatherViewModel()
        {
            GetCurrentWeatherCommand = new DelegateCommand<string>(GetCurrentWeather);
            GetOneCallCommand = new DelegateCommand<string>(GetOneCall);
        }

        private async void GetOneCall(string obj)
        {
            OWResult = await OpenWeatherProcessor.Instance.GetOneCallAsync();

            if (OWResult != null)
            {
                RawText = $"Heure : {LongToDateTime( OWResult.Current.DateTime).ToLocalTime().ToShortTimeString()}" + Environment.NewLine;
                RawText += $"Température : {string.Format("{0:N2}", OWResult.Current.Temperature)}" + Environment.NewLine;
            }
            else
                RawText = "Error!";
        }

        
        private async void GetCurrentWeather(string obj)
        {
            var currentWeather = await OpenWeatherProcessor.Instance.GetCurrentWeather();

            if (currentWeather != null)
            {
                RawText = $"Ville : {currentWeather.Name} ({currentWeather.Coord.Latitude}, {currentWeather.Coord.Longitude})" + Environment.NewLine;
                RawText += $"Temperature : {currentWeather.Main.Temperature}" + Environment.NewLine;
                RawText += $"Heure : {LongToDateTime(currentWeather.DateTime).ToLocalTime().ToShortTimeString()}" + Environment.NewLine;

            }
            else
            {
                RawText = "Error!";
            }
        }

        private DateTime LongToDateTime(long value)
        {
            return DateTimeOffset.FromUnixTimeSeconds(value).UtcDateTime;
        }
    }
}
