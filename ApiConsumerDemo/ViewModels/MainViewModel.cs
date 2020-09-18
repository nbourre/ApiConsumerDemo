using ApiConsumerDemo.Commands;
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
        #region Properties
        private int maxNumber;
        private int currentNumber;
        private BitmapImage comicImage;
        private bool nextEnabled;
        private bool previousEnabled = true;

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
            set
            {
                comicImage = value;
                OnPropertyChanged();
            }
        }

        public bool NextEnabled
        {
            get => nextEnabled;
            set
            {
                nextEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool PreviousEnabled
        {
            get { return previousEnabled; }
            set { 
                previousEnabled = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Commands

        public AsyncCommand<int> LoadImageCommand { get; private set; }
        public AsyncCommand<object> WindowLoadedCommand { get; private set; }
        public AsyncCommand<object> LoadPreviousCommand { get; private set; }
        public AsyncCommand<object> LoadNextCommand { get; private set; }

        #endregion

        public MainViewModel()
        {
            LoadImageCommand = new AsyncCommand<int>(LoadImageAsync);
            WindowLoadedCommand = new AsyncCommand<object>(WindowLoaded);

            LoadPreviousCommand = new AsyncCommand<object>(LoadPreviousAsync, CanLoadPrevious);
            LoadNextCommand = new AsyncCommand<object>(LoadNextAsync, CanLoadNext);
        }

        private async Task WindowLoaded(object arg)
        {
            await LoadImageAsync();
        }

        private async Task LoadImageAsync(int imageNumber = 0)
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

        private async Task LoadPreviousAsync(object arg)
        {
            if (currentNumber > 1)
            {
                currentNumber -= 1;
                NextEnabled = true;
                await LoadImageAsync(currentNumber);

                if (currentNumber == 1)
                {
                    PreviousEnabled = false;
                }
            }
        }

        private async Task LoadNextAsync(object arg)
        {
            if (currentNumber < maxNumber)
            {
                currentNumber += 1;
                PreviousEnabled = true;
                await LoadImageAsync(currentNumber);

                if (currentNumber == maxNumber)
                {
                    NextEnabled = false;
                }
            }
        }

        private bool CanLoadPrevious(object arg) => PreviousEnabled;
        private bool CanLoadNext(object arg) => NextEnabled;

    }
}
