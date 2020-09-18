using DemoLibrary;
using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ApiConsumerDemo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private int maxNumber;
        private int currentNumber;
        private BitmapImage comicImage;

        public int MaxNumber
        {
            get => maxNumber;
            set
            {
                maxNumber = value;
                OnPropertyChanged();
            }
        }

        public int CurrentNumber
        {
            get => currentNumber;
            set
            {
                currentNumber = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage ComicImage
        {
            get => comicImage;
            set { 
                comicImage = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadImage(int imageNumber = 0)
        {
            var comic = await ComicProcessor.LoadComic(imageNumber);

            if (imageNumber == 0)
            {
                maxNumber = comic.Num;
            }

            currentNumber = comic.Num;

            var uriSource = new Uri(comic.Img, UriKind.Absolute);

            ComicImage = new BitmapImage(uriSource,
                new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable));

        }
    }
}
