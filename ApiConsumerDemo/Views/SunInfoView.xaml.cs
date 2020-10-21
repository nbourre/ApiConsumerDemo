using DemoLibrary;
using System.Windows;

namespace ApiConsumerDemo
{
    /// <summary>
    /// Interaction logic for SunInfo.xaml
    /// </summary>
    public partial class SunInfo : Window
    {
        public SunInfo()
        {
            InitializeComponent();
        }

        private async void loadSunInfo_Click(object sender, RoutedEventArgs e)
        {
            var sunInfo = await SunProcessor.LoadSunInformation();
            sunriseText.Text = $"{Properties.Resources.msg_sunrise} {sunInfo.Sunrise.ToLocalTime().ToShortTimeString()}";
            sunsetText.Text = $"{Properties.Resources.msg_sunset} {sunInfo.Sunset.ToLocalTime().ToShortTimeString()}";
        }
    }
}
