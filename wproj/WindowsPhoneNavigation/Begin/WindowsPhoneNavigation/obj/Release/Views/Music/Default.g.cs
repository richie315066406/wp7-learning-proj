﻿#pragma checksum "D:\git_proj\wp7\wproj\WindowsPhoneNavigation\Begin\WindowsPhoneNavigation\Views\Music\Default.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7351F6EFBB95D99EF5F1DA427D350FFF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WindowsPhoneNavigation.Views.Music {
    
    
    public partial class Default : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationName;
        
        internal System.Windows.Controls.TextBlock ListName;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.MediaElement media;
        
        internal System.Windows.Controls.TextBlock musicToPlay;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WindowsPhoneNavigation;component/Views/Music/Default.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationName = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationName")));
            this.ListName = ((System.Windows.Controls.TextBlock)(this.FindName("ListName")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.media = ((System.Windows.Controls.MediaElement)(this.FindName("media")));
            this.musicToPlay = ((System.Windows.Controls.TextBlock)(this.FindName("musicToPlay")));
        }
    }
}

