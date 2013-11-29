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

namespace csvReading
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=vr",UriKind.Relative));
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=vi", UriKind.Relative));
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=ro", UriKind.Relative));
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=bl", UriKind.Relative));
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=tv", UriKind.Relative));
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=pd", UriKind.Relative));
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=ve", UriKind.Relative));
        }
    }
}