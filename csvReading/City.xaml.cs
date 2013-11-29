using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace csvReading
{
    public partial class City : PhoneApplicationPage
    {
        int idComune;


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string codice;
            // recupero il comune di cui cercare il tutto
            if (NavigationContext.QueryString.TryGetValue("comune", out codice))
            {
                idComune = Int32.Parse(codice);
            }


            //carico dati da csv
            Record dati = CsvReader.Instance.LoadLastData(idComune);

            /*Scrivo dati nella pagina*/
            if (dati.Comune.Length > 9) Comune.FontSize = 50;
            Istat.Text = dati.CodiceIstat.ToString();
            NumAbitanti.Text = dati.PopolazioneMedia.ToString();
            Comune.Text = dati.Comune;
            Morti.Text = dati.Morti.ToString();
            Nati.Text = dati.NatiVivi.ToString();
            Anno.Text = "Dati aggiornati al " + dati.Anno.ToString();
            Immigrati.Text = dati.Iscritti.ToString();
            Emigrati.Text = dati.Cancellati.ToString();
        }


        public City()
        {
            InitializeComponent();
        }

        private void Abitanti_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che dato e di che comune visualizzare
            NavigationService.Navigate(new Uri("/Graph.xaml?dato=popolazione&comune="+idComune.ToString(), UriKind.Relative));
        }
        private void Nati_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che dato e di che comune visualizzare
            NavigationService.Navigate(new Uri("/Graph.xaml?dato=nati&comune=" + idComune.ToString(), UriKind.Relative));
        }

    }
}