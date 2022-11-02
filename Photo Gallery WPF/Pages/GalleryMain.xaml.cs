using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Photo_Gallery_WPF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
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

namespace Photo_Gallery_WPF.Pages
{
    public partial class GalleryMain : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));




        public List<ImageSource> ImageSources { get; set; }
        private uint _rows = 3;

        public uint RowsCount
        {
            get { return _rows; }
            set
            {
                _rows = value;
                NotifyPropertyChanged();
            }
        }

        private uint _columns = 3;

        public uint ColumnsCount
        {
            get { return _columns; }
            set
            {
                _columns = value;
                NotifyPropertyChanged();
            }
        }


        public GalleryMain()
        {
            InitializeComponent();
            DataContext = this;
            ImageSources = new();

        }



        private async void lbx1_DragOver(object sender, DragEventArgs e)
        {
            bool dropEnabled = true;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames =
                                 e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (string filename in filenames)
                {
                    if (System.IO.Path.GetExtension(filename).ToUpperInvariant() != ".JPG" && System.IO.Path.GetExtension(filename).ToUpperInvariant() != ".JPEG"&& System.IO.Path.GetExtension(filename).ToUpperInvariant() != ".PNG"&& System.IO.Path.GetExtension(filename).ToUpperInvariant() != ".BMP")
                    {
                        dropEnabled = false;
                        break;
                    }
                }
            }
            else
            {
                dropEnabled = false;
            }

            if (!dropEnabled)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }


        private async void lbx_Drop(object sender, DragEventArgs e)
        {
            foreach (var filename in e.Data.GetData(DataFormats.FileDrop) as string[])
            {
                var image = new Image()
                {
                    Source = new BitmapImage(new Uri(filename)),
                    Width = 150,
                    Height = 150,
                    MinHeight = 70,
                    MinWidth = 70,
                    Stretch = Stretch.Uniform
                };

                lbx.Items.Add(image);
                ImageSources.Add(image.Source);
            }

        }



        private void lbx_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbx.SelectedItem is Image image)
            {
                PicturePage photoPage = new(image.Source, ImageSources);

                NavigationService.Navigate(photoPage);

            }
        }



        private void MenuItemViewLarge_Click(object sender, RoutedEventArgs e)
        {
            ColumnsCount = 1;
            RowsCount = ((uint)lbx.Items.Count);
        }

        private void MenuItemViewMedium_Click(object sender, RoutedEventArgs e) => ColumnsCount = 3;
        private void MenuItemViewSmall_Click(object sender, RoutedEventArgs e) => ColumnsCount = 8;
        private void MenuItemFileNew(object sender, RoutedEventArgs e)
        {
            MainView mainView = new MainView();
            mainView.Show();
        }



        private  void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG|*.png|JPG|*.jpg; *.jpeg;|GIF|*.gif;|BMP|*.bmp;";
            openFileDialog.Multiselect = true;  

            if (openFileDialog.ShowDialog() is true)
            {
                var image = new Image()
                {
                    Source = new BitmapImage(new Uri(openFileDialog.FileName)),
                    Width = 150,
                    Height = 150,
                    MinHeight = 70,
                    MinWidth = 70,
                    Stretch = Stretch.Uniform
                };

                lbx.Items.Add(image);
                
                ImageSources.Add(image.Source);
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (lbx.SelectedItem != null)
                lbx.Items.Remove(lbx.SelectedItem);
        }
    }
}
