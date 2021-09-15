using ApiConsumerDemo.ViewModels;
using System.Windows;

namespace ApiConsumerDemo.Views
{
    /// <summary>
    /// Interaction logic for OpenWeatherView.xaml
    /// </summary>
    public partial class OpenWeatherView : Window
    {
        public OpenWeatherView()
        {
            InitializeComponent();
            DataContext = new OpenWeatherViewModel();
        }
    }
}
