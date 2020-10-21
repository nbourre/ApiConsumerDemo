using ApiConsumerDemo.ViewModels;
using ApiConsumerDemo.Views;
using DemoLibrary;
using System.Windows;

namespace ApiConsumerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Source : https://www.youtube.com/watch?v=aWePkE2ReGw&t=4s&ab_channel=IAmTimCorey
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel vm;


        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            vm = new MainViewModel();
            DataContext = vm;
            //nextImageButton.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vm.LoadImageCommand.Execute(0);
        }

        /// <summary>
        /// Bypassing the MVVM pattern, because it's just a demo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sunInformationButton_Click(object sender, RoutedEventArgs e)
        {
            SunInfo sunInfo = new SunInfo();
            sunInfo.Show();
        }

        private void MenuItem_OpenWeather_Click(object sender, RoutedEventArgs e)
        {
            OpenWeatherView ow = new OpenWeatherView();
            ow.Show();
        }
    }
}
