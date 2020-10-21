using ApiConsumerDemo.Commands;
using DemoLibrary.Weather;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiConsumerDemo.ViewModels
{
    public class OpenWeatherViewModel: BaseViewModel
    {
        private OpenWeatherResultModel owResult;

        public OpenWeatherResultModel OWResult
        {
            get { return owResult; }
            set { 
                owResult = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand<string> OneCallCommand { get; set; }

        public OpenWeatherViewModel()
        {
            OneCallCommand = new DelegateCommand<string>(GetOneCall);
        }

        private async void GetOneCall(string obj)
        {
            OWResult = await OpenWeatherProcessor.Instance.GetOneCallAsync();
        }
    }
}
