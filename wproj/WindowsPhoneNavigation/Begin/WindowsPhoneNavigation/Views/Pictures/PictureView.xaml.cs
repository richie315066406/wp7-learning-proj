using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WindowsPhoneNavigation.Misc;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Phone;

namespace WindowsPhoneNavigation.Views.Pictures
{
    public partial class PictureView : PhoneApplicationPage
    {
        private Uri currentImgUri;
        private Point mouseEnterPosition;
        private Point mouseLeavePosition;

        public PictureView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Default_Loaded);
            this.OrientationChanged += (s, e) =>
            {
                if (e.Orientation == PageOrientation.Landscape ||
                    e.Orientation == PageOrientation.LandscapeLeft ||
                    e.Orientation == PageOrientation.LandscapeRight)
                {
                    TitlePanel.Visibility = System.Windows.Visibility.Collapsed;
                    ContentPanel.SetValue(Grid.RowSpanProperty, 2);
                    ContentPanel.SetValue(Grid.RowProperty, 0);
                }
                else
                {
                    //TitlePanel.Visibility = System.Windows.Visibility.Visible;
                    TitlePanel.Visibility = System.Windows.Visibility.Collapsed;
                    ContentPanel.SetValue(Grid.RowSpanProperty, 1);
                    ContentPanel.SetValue(Grid.RowProperty, 1);
                }
            };
        }

        private void Default_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavigationContext.QueryString.Count > 0)
            {
                Photo thePhoto = new Photo();
                thePhoto.Filename = NavigationContext.QueryString.Values.First();

                Uri imgURL = new Uri(NavigationContext.QueryString.Values.First());
                thePhoto.Image = new System.Windows.Media.Imaging.BitmapImage(imgURL);
            
                //thePhoto.Image = Utils.GetImage(NavigationContext.QueryString.Values.First());
                this.DataContext = thePhoto;

                //currentImgUri = imgURL;
                currentImgUri = Utils.imgUriList.ElementAt(0);
            }
        }

        private void Image_DoubleTap(object sender, GestureEventArgs e)
        {
            int index = Utils.imgUriList.IndexOf(currentImgUri);
            if (index < Utils.imgUriList.Count())
                currentImgUri = Utils.imgUriList.ElementAt(index + 1); 

            BitmapImage img = new System.Windows.Media.Imaging.BitmapImage(currentImgUri);
            this.DataContext = new Photo() { Image = img,
                                             Filename = currentImgUri.ToString()};
        }

        private WriteableBitmap LoadFile(string fileName)
        {
            IsolatedStorageFile userStorage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream fs = userStorage.OpenFile(fileName, FileMode.Open, FileAccess.Read);
            return PictureDecoder.DecodeJpeg(fs);
        }

        private void showNextImage()
        { 
            int index = Utils.imgUriList.IndexOf(currentImgUri);
            if (index < Utils.imgUriList.Count()-1)
                currentImgUri = Utils.imgUriList.ElementAt(index + 1); 

            //BitmapImage img = new System.Windows.Media.Imaging.BitmapImage(currentImgUri);
            string fileName = currentImgUri.ToString().Replace('/', '_').Replace(':', '_').Replace('.', '_');
            //WriteableBitmap wb = LoadFile(currentImgUri.ToString().Replace('/','_').Replace(':','_').Replace('.','_'));
            WriteableBitmap wb = LoadFile(fileName);
            /*
            BitmapImage img = new BitmapImage();
            MemoryStream ms = new MemoryStream();
            wb.SaveJpeg(ms, wb.PixelWidth, wb.Pixels.Count()/wb.PixelWidth,0,100);
            img.SetSource(ms);
            this.DataContext = new Photo() { Image = img,
                                             Filename = ""};//currentImgUri.ToString()};
            */
            this.imageBox.Source = wb;
        }

        private void showPreviousImage()
        { 
            int index = Utils.imgUriList.IndexOf(currentImgUri);
            if (index > 0)
                currentImgUri = Utils.imgUriList.ElementAt(index - 1); 

            //BitmapImage img = new System.Windows.Media.Imaging.BitmapImage(currentImgUri);
            WriteableBitmap wb = LoadFile(currentImgUri.ToString().Replace('/','_').Replace(':','_').Replace('.','_'));
            /*
            BitmapImage img = new BitmapImage();
            MemoryStream ms = new MemoryStream();
            wb.SaveJpeg(ms, wb.PixelWidth, wb.Pixels.Count()/wb.PixelWidth,0,100);
            img.SetSource(ms);
 
            this.DataContext = new Photo() { Image = img,
                                             Filename = currentImgUri.ToString()};
            */
            imageBox.Source = wb;
                    
        }

        private void imageBox_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.GetPosition(this.imageBox).ToString());
            mouseEnterPosition = e.GetPosition(this.imageBox);
        }

        private void imageBox_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.GetPosition(this.imageBox).ToString());
            if (mouseEnterPosition.X < mouseLeavePosition.X)
                showPreviousImage();
            else
                showNextImage();
        }

        private void imageBox_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLeavePosition = e.GetPosition(this.imageBox);
        }
    }
}