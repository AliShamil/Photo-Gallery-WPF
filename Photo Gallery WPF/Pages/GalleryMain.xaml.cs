using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// GalleryMain.xaml etkileşim mantığı
    /// </summary>
    public partial class GalleryMain : Page
    {
        public GalleryMain()
        {
            InitializeComponent();
        }
        public uint Rows { get; set; } = 3;
        public uint Columns { get; set; } = 3;
        

        private void lbx1_DragOver(object sender, DragEventArgs e)
        {
            bool dropEnabled = true;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames =
                                 e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (string filename in filenames)
                {
                    if (System.IO.Path.GetExtension(filename).ToUpperInvariant() != ".JPG" && System.IO.Path.GetExtension(filename).ToUpperInvariant() != ".JPEG"&& System.IO.Path.GetExtension(filename).ToUpperInvariant() != ".PNG")
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


        private void lbx1_Drop(object sender, DragEventArgs e)
        {
            foreach (var filename in e.Data.GetData(DataFormats.FileDrop) as string[])
            {
                lbx.Items.Add(new Image()
                {
                    Source =new BitmapImage(new Uri(filename)),
                    Width=100,
                    Height=100,
                    Stretch = Stretch.Uniform
                });
                
                MessageBox.Show(filename);
            }

        }

        private void lbx_DragEnter(object sender, DragEventArgs e)
        {
            if (lbx.Items.Count > (Rows * 3))
            {
                ++Rows;
            }
            else
                return;
        }

        private void lbx_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

                var listBox = sender as ListBox;
                var selectedImage = listBox?.SelectedItem as Image;

                if (selectedImage is null)
                    return;

           
            //ProductInfo window = new(selectedProductItem);
            // window.ShowDialog();


        }
    }
}
