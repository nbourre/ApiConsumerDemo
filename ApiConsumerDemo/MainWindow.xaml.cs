using ApiConsumerDemo.ViewModels;
using DemoLibrary;
using System;
using System.Net.Cache;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

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

        //private async Task LoadImage(int imageNumber = 0)
        //{
        //    var comic = await ComicProcessor.LoadComic(imageNumber);

        //    if (imageNumber == 0)
        //    {
        //        maxNumber = comic.Num;
        //    }

        //    currentNumber = comic.Num;

        //    var uriSource = new Uri(comic.Img, UriKind.Absolute);

        //    comicImage.Source = new BitmapImage(uriSource, 
        //        new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable));

        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vm.LoadImageCommand.Execute(0);
        }

        //private async void previousImageButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (currentNumber > 1)
        //    {
        //        currentNumber -= 1;
        //        nextImageButton.IsEnabled = true;
        //        await LoadImage(currentNumber);

        //        if (currentNumber == 1)
        //        {
        //            previousImageButton.IsEnabled = false;
        //        }
        //    }
        //}

        //private void sunInformationButton_Click(object sender, RoutedEventArgs e)
        //{
        //    SunInfo sunInfo = new SunInfo();
        //    sunInfo.Show();
        //}

        //private async void nextImageButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (currentNumber < maxNumber)
        //    {
        //        currentNumber += 1;
        //        previousImageButton.IsEnabled = true;
        //        await LoadImage(currentNumber);

        //        if (currentNumber == maxNumber)
        //        {
        //            nextImageButton.IsEnabled = false;
        //        }
        //    }
        //}
    }
}
