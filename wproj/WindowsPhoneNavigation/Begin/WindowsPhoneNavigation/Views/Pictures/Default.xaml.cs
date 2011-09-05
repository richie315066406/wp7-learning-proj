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
using System.Collections.ObjectModel;
using WindowsPhoneNavigation.Misc;
using Microsoft.Phone.Shell;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.IsolatedStorage;

namespace WindowsPhoneNavigation.Views.Pictures
{
    public partial class Default : PhoneApplicationPage
    {
        ObservableCollection<Photo> photos = new ObservableCollection<Photo>();
        //private List<Uri> imgUriList = WindowsPhoneNavigation.Misc.Utils.imgUriList; 
        private List<Uri> imgUriList = new List<Uri>();
        private List<Uri> savedImgList = WindowsPhoneNavigation.Misc.Utils.imgUriList; 

        private string currentUrl = "http://bbs.6park.com/bbs2/messages/39862.html";
        private int topicPageCount = 0; 
        private int downloadingImgListIndex = 0;

        public Default()
        {
            InitializeComponent();
            //for  test: temp stop getting images before storage full!
            //BeginParseTopicPageURL("http://web.6park.com/bbs/first1.shtml");

            initializeFileList();

            InitializePhotos();
            this.lstPictures.ItemsSource = photos;
 //           (ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).Click += new EventHandler(ApplicationBar_OnClick);
        }

        private void initializeFileList()
        {
            string[] strArr;
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                strArr = store.GetFileNames("\\*_jpg"); 
            }
            foreach (string s in strArr)
            {
                System.Diagnostics.Debug.WriteLine(s);
                string fakeUrl = s.Replace("http___", "http://").Replace("_jpg","/jpg");
                this.savedImgList.Add(new Uri(fakeUrl));
            }
        }

        private void InitializePhotos()
        {

            photos.Add(new Photo()
            {
                Filename = "http://ressim.net/8/upload/3f5e3ee5.jpg", //
                
                Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("http://ressim.net/8/upload/3f5e3ee5.jpg"))
                //Image = Utils.GetImage("Butterfly.jpg")
            });

/*
            foreach(Uri uri in this.imgUriList)
            {
                photos.Add(new Photo()
                {
                    Filename = uri.ToString(),
                    Image = new System.Windows.Media.Imaging.BitmapImage(uri)
                });
            }
*/
            /*
            photos.Add(new Photo()
            {
                Filename = "Desert.jpg",
                Image = Utils.GetImage("Desert.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Field.jpg",
                Image = Utils.GetImage("Field.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Flower.jpg",
                Image = Utils.GetImage("Flower.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Hydrangeas.jpg",
                Image = Utils.GetImage("Hydrangeas.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Jellyfish.jpg",
                Image = Utils.GetImage("Jellyfish.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Koala.jpg",
                Image = Utils.GetImage("Koala.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Leaves.jpg",
                Image = Utils.GetImage("Leaves.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Lighthouse.jpg",
                Image = Utils.GetImage("Lighthouse.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Penguins.jpg",
                Image = Utils.GetImage("Penguins.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Rocks.jpg",
                Image = Utils.GetImage("Rocks.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Tulip.jpg",
                Image = Utils.GetImage("Tulip.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Tulips.jpg",
                Image = Utils.GetImage("Tulips.jpg")
            });
            photos.Add(new Photo()
            {
                Filename = "Window.jpg",
                Image = Utils.GetImage("Window.jpg")
            });
            */
        }

        private void btnRemoveSelection_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (null != lstPictures.SelectedItem)
                photos.Remove(lstPictures.SelectedItem as Photo);
            */
            this.imgUriList.Clear();
            this.BeginParseImageURL(this.TextBoxUrl.Text);
        }

        private void lstPictures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (null != lstPictures.SelectedItem)
            {
                btnRemoveSelection.IsEnabled = true;
                ShowCurrentImage();
            }
            if (photos.Count == 0)
            {
                btnRemoveSelection.IsEnabled = false;
            }
            */
        }

        private void ApplicationBar_OnClick(object sender, EventArgs e)
        {
            ShowCurrentImage();
        }

        private void ShowCurrentImage()
        {
            if (null != lstPictures.SelectedItem)
            {
                PhoneApplicationFrame root = Application.Current.RootVisual as PhoneApplicationFrame;
                root.Navigate(new Uri("/PictureView/" + (lstPictures.SelectedItem as Photo).Filename, UriKind.Relative));
            }
        }

        private void lstPicture_Tap(object sender, GestureEventArgs e)
        {
            ShowCurrentImage();
        }

        //async topPage url parsing:
        private void BeginParseTopicPageURL(string url)
        {
            WebClient wc = new WebClient();       
            wc.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(wc_topic_DownloadStringCompleted);
            wc.DownloadStringAsync(new Uri(url));
        }

        void wc_topic_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        { 
            string htmlPage = e.Result;
            System.Diagnostics.Debug.WriteLine("---------topic html---------"+sender.ToString());                                    
            System.Diagnostics.Debug.WriteLine(htmlPage);                                    

            //string imgUrlPattern = "<img\\ssrc[^>]*jpg\">";
            string topicUrlPattern = "<br><!--top:\\s\\d+--><li><a href=\"[^>]*html";
            MatchCollection matches = Regex.Matches(htmlPage, topicUrlPattern,
                                                RegexOptions.IgnoreCase);

            topicPageCount = matches.Count;
            foreach (Match m in matches)
            {
                System.Diagnostics.Debug.WriteLine(m.ToString());
                BeginParseImageURL(m.ToString().Remove(0,34));

                //for test, to be save time by only get one topic.
                //break;
            }
        }

        // asych image parsing functions:
        private void BeginParseImageURL(string url)
        {
            currentUrl = url;
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            wc.DownloadStringAsync(new Uri(url));
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string htmlPage = e.Result;
            System.Diagnostics.Debug.WriteLine("---------html---------"+sender.ToString());                                    
            System.Diagnostics.Debug.WriteLine(htmlPage);                                    

            //string imgUrlPattern = "<img\\ssrc[^>]*jpg\">";
            string imgUrlPattern = "http:[^>]*jpg";
            MatchCollection matches = Regex.Matches(htmlPage, imgUrlPattern,
                                                RegexOptions.IgnoreCase);


            foreach (Match m in matches)
            {
                System.Diagnostics.Debug.WriteLine(m.ToString());
                try
                {
                    Uri imgUri = new Uri(m.ToString());
                    imgUriList.Add(imgUri);
               }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    continue;
                }
           }

            System.Diagnostics.Debug.WriteLine("\n---------hiee---------"+matches.Count);                                    
            //throw new NotImplementedException();

            topicPageCount -= 1;
            if (topicPageCount == 0)
                BeginDownloadImage(imgUriList.ElementAt(0));
        }

        void BeginDownloadImage(Uri imgUri)
        { 
            //get image file
            WebClient wc = new WebClient();
            wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
            wc.OpenReadAsync(imgUri);
        }

        void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    IsolatedStorageFile userStore = IsolatedStorageFile.GetUserStoreForApplication();
                    string fileName = imgUriList.ElementAt(downloadingImgListIndex).ToString();
                    string url = fileName;
                    fileName = fileName.Replace('/','_').Replace(':','_').Replace('.','_');
                    IsolatedStorageFileStream stream = userStore.CreateFile(fileName);
                    byte[] byteImage = new byte[e.Result.Length];   
                    e.Result.Read(byteImage, 0, byteImage.Length);
                    stream.Write(byteImage, 0, byteImage.Length);
                    this.savedImgList.Add(imgUriList.ElementAt(downloadingImgListIndex));
                }
            }
            catch (Exception ex)
            { 
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            this.downloadingImgListIndex+=1;
            if(downloadingImgListIndex < imgUriList.Count())
                BeginDownloadImage(imgUriList.ElementAt(downloadingImgListIndex));
        }
            
    }
}