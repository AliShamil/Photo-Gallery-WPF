using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Photo_Gallery_WPF.Pages
{
    public partial class PicturePage : Page, INotifyPropertyChanged
    {
        private ImageSource _currentImageSource;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ImageSource CurrentImageSource
        {
            get { return _currentImageSource; }
            set
            {
                _currentImageSource = value;
                OnPropertyChanged();
            }
        }

       
        public List<ImageSource> ImageSources { get; set; }

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public PicturePage(ImageSource currentImageSource, List<ImageSource> imageSources)
        {
            InitializeComponent();
            DataContext = this;
            _currentImageSource = currentImageSource;
            ImageSources = imageSources;

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
        }

        private void BtnForward_Click(object sender, RoutedEventArgs e)
        {
            BtnPlay.IsChecked = false;
            dispatcherTimer_Tick(sender, e);
        }

        private void BtnBackward_Click(object sender, RoutedEventArgs e)
        {
            BtnPlay.IsChecked = false;
            var index = ImageSources.IndexOf(CurrentImageSource);

            if (index -1 >= 0)
            {
                CurrentImageSource = ImageSources[index - 1];
                return;
            }

            CurrentImageSource = ImageSources[ImageSources.Count-1];
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var index = ImageSources.IndexOf(CurrentImageSource);

            if (index + 1 < ImageSources.Count)
            {
                CurrentImageSource = ImageSources[index + 1];
                return;
            }

            CurrentImageSource = ImageSources[0];
        }

        private void BtnPlayChecked(object sender, RoutedEventArgs e) => dispatcherTimer.Start();

        private void BtnPlayUnchecked(object sender, RoutedEventArgs e) => dispatcherTimer.Stop();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BtnPlay.IsChecked = false;
        }
    }
}
