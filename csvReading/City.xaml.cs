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
            Anno.Text = "Dati aggiornati al " + dati.Anno;
            Immigrati.Text = dati.Iscritti.ToString();
            Emigrati.Text = dati.Cancellati.ToString();

            int istat = dati.CodiceIstat/1000;
            int anno = dati.Anno;
            int abitanti = CsvReader.Instance.AbitantiProvincia(istat, anno);
            //MessageBox.Show("Abitati in provincia: "+abitanti.ToString());
            double popol = (double)(dati.PopolazioneMedia);
            double percent = 100*popol/(double)abitanti;
            double percentuale = Math.Round(percent,2);
            //imposto articolo per la percentuale
            string articolo = "il ";
            if(percentuale<1)articolo = "lo ";
            if(percentuale<2&&percentuale>=1)articolo="l'";
            if(percentuale<9&&percentuale>=8)articolo="l'";
            if(percentuale<12&&percentuale>=11)articolo="l'";

            int codProv = (dati.CodiceIstat)/1000;
            string provincia = "";
#region switch
            switch (codProv)
        {
            case (23):
                {
                    provincia = "Verona";
                    break;
                }
            case (24):
                {
                    provincia = "Vicenza";
                    break;
                }
            case (25):
                {
                    provincia = "Belluno";
                    break;
                }
            case (26):
                {
                    provincia = "Treviso";
                    break;
                }
            case (27):
                {
                    provincia = "Venezia";
                    break;
                }
            case (28):
                {
                    provincia = "Padova";
                    break;
                }
            case (29):
                {
                    provincia = "Rovigo";
                    break;
                }
        }
#endregion

            Percentuale.Text="Cioè "+articolo+percentuale+"% della provincia di "+provincia+".";
        }


        public City()
        {
            InitializeComponent();
        }

        #region gestione click pulsanti del grafico
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
        private void Morti_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che dato e di che comune visualizzare
            NavigationService.Navigate(new Uri("/Graph.xaml?dato=morti&comune=" + idComune.ToString(), UriKind.Relative));
        }
        private void Immigrati_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che dato e di che comune visualizzare
            NavigationService.Navigate(new Uri("/Graph.xaml?dato=iscritti&comune=" + idComune.ToString(), UriKind.Relative));
        }
        private void Emigrati_Click(object sender, RoutedEventArgs e)
        {
            //gestisco via get che dato e di che comune visualizzare
            NavigationService.Navigate(new Uri("/Graph.xaml?dato=cancellati&comune=" + idComune.ToString(), UriKind.Relative));
        }
        #endregion

    }
}