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

namespace WindowsPhoneNavigation.Views.Video
{
    public partial class Default : PhoneApplicationPage
    {
        public Default()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Default_Loaded);
            this.OrientationChanged += delegate(object s, OrientationChangedEventArgs e) //Lambda way: (s,e) =>
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

            this.BackKeyPress += (s, e) =>
            {
                if (media.CurrentState == MediaElementState.Playing)
                    media.Stop();
            };
        }

        private void Default_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavigationContext.QueryString.Count > 0)
            {
                try
                {
                    media.Source = new Uri(NavigationContext.QueryString.Values.First(), UriKind.RelativeOrAbsolute);
                    media.Position = TimeSpan.FromMilliseconds(0);
                    media.Play();
                }
                catch (Exception ex)
                { 
                    //handle        
                }
            }
        }
    }
}