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

namespace RSSReader
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        public PivotPage1()
        {
            InitializeComponent();
        }
        private void NavigateWithParam(String param)
        {
            NavigationService.Navigate(new Uri("/PanoramaPage1.xaml?category=" + param, UriKind.Relative));
        }
        private void Navigate()
        {
            NavigationService.Navigate(new Uri("/PanoramaPage1.xaml", UriKind.Relative));
        }
        private void NewsTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigateWithParam("world");
        }

        private void SportsTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigateWithParam("sport");
        }

        private void TechTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigateWithParam("technology");
        }

        private void OpinionTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigateWithParam("opinion");
        }

        private void SettingsTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Navigate();
        }
    }
}