using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Collections.Generic;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/info.xaml", UriKind.Relative));
        }
    }
}