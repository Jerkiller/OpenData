using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Navigation;

namespace csvReading
{
    public partial class MainPage : PhoneApplicationPage
    {
        SolidColorBrush selezionato = new SolidColorBrush(Color.FromArgb(0x77, 0x00, 0x33, 0x00));
        SolidColorBrush the_void = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));
            
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Verona.Fill = the_void;
            Vicenza.Fill = the_void;
            Venezia.Fill = the_void;
            Treviso.Fill = the_void;
            Belluno.Fill = the_void;
            Padova.Fill = the_void;
            Rovigo.Fill = the_void;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Verona.Fill = selezionato;
                //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=vr",UriKind.Relative));
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Vicenza.Fill = selezionato;
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=vi", UriKind.Relative));
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Rovigo.Fill = selezionato;
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=ro", UriKind.Relative));
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Belluno.Fill = selezionato;
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=bl", UriKind.Relative));
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            Treviso.Fill = selezionato;
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=tv", UriKind.Relative));
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            Padova.Fill = selezionato;
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=pd", UriKind.Relative));
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            Venezia.Fill = selezionato;
            //gestisco via get che comune visualizzare
            NavigationService.Navigate(new Uri("/ListaComuni.xaml?prov=ve", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/info.xaml", UriKind.Relative));
        }
    }
}