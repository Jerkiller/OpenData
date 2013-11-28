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
    public partial class ListaComuni : PhoneApplicationPage
    {
        List<string> comuni;
        List<int> codici;


        public ListaComuni()
        {
            InitializeComponent();
            caricaComuni();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //passiamo via get il codice del comune istat voluto
            string idComune = "27024";
            NavigationService.Navigate(new Uri("/City.xaml?comune="+idComune.ToString(), UriKind.Relative));
        }



        public void caricaVenezia()
        {
            //carica una lista di bottoni in ListaComuni
        }

        public void caricaComuni()
        {
            codici = CsvReader.Instance.caricaIstat();
            comuni = CsvReader.Instance.caricaComuni();
            
            //debug sesion
            /*
            string s = "";
            foreach (string y in comuni){s = s+y+" ";}
            MessageBox.Show(comuni.Count.ToString()+" ecco i comuni:\n\n"+s);
            */
        }

    }
}