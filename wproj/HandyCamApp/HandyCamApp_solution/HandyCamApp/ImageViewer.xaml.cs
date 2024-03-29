﻿using System;
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
using System.Windows.Media.Imaging;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone; 

namespace HandyCamApp
{
    public partial class ImageViewer : PhoneApplicationPage
    {
        public ImageViewer()
        {
            InitializeComponent();
        }

        private WriteableBitmap LoadFile(string fileName)
        {
            IsolatedStorageFile userStorage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream fs = userStorage.OpenFile(fileName, FileMode.Open, FileAccess.Read);
            return PictureDecoder.DecodeJpeg(fs);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string imageFileName = NavigationContext.QueryString.Values.First();
            WriteableBitmap wb = LoadFile(imageFileName);
            this.ImageViewerBox.Source = wb;
        }
    }
}