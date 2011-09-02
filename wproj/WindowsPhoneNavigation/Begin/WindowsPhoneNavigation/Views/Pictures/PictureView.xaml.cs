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
using WindowsPhoneNavigation.Misc;

namespace WindowsPhoneNavigation.Views.Pictures
{
    public partial class PictureView : PhoneApplicationPage
    {
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
                    TitlePanel.Visibility = System.Windows.Visibility.Visible;
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
                thePhoto.Image = Utils.GetImage(NavigationContext.QueryString.Values.First());
                this.DataContext = thePhoto;
            }
        }
    }
}