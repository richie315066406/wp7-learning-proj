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

namespace WindowsPhoneNavigation.Views.Music
{
    public partial class Default : PhoneApplicationPage
    {
        public Default()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Default_Loaded); 
        }

        void Default_Loaded(object sender, RoutedEventArgs e)
        {
            this.musicToPlay.Text = this.NavigationContext.QueryString.Values.First();
            try
            {
                media.AutoPlay = true;
                media.Source = new Uri(NavigationContext.QueryString.Values.First(), UriKind.RelativeOrAbsolute);
                media.Position = TimeSpan.FromMilliseconds(0);
                media.Play();
            }
            catch
            { 
            }
        }
    }
}