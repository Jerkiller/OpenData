using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace csvReading
{
    public partial class Graph : PhoneApplicationPage
    {
        public double altezzaCanvas;
        public double larghezzaCanvas;
        public double larghezzaGriglia;
        public List<int> valori;
        public List<int> anni;
        string tipoDato;

        #region Metodi statistici
        public int Max()
        {
            int max = 0;
            foreach (int w in valori) if (w > max) max = w;
            return max;
        }

        public int Min()
        {
            int min = int.MaxValue;
            foreach (int w in valori) if (w < min) min = w;
            return min;
        }

        public double Media()
        {
            double media = 0;
            foreach (int w in valori) media += w;
            return (double)(media/(double)valori.Count);
        }

        public double Varianza()
        {
            double media = Media();
            double var = 0;
            foreach (int w in valori) var = (w-media)*(w-media);
            return (double)(var / (double)valori.Count);
        }
        public string Statistica()
        {
            string stat = "";

            stat += "Massimo: " + Max().ToString() + "\n";
            stat += "Minimo: " + Min().ToString() + "\n";
            stat += "Range: " + (Max() - Min()).ToString() + "\n";
            stat += "Media: " + Media() + "\n";
            stat += "Varianza: " + Varianza() + "\n";

            return stat;
        }
        #endregion


        int idComune;
        Record comune = null;
        CsvReader csv = CsvReader.Instance;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string codice;
            // recupero id del comune di cui cercare il tutto
            if (NavigationContext.QueryString.TryGetValue("comune", out codice))
            {
                idComune = Int32.Parse(codice);
                comune = csv.LoadLastData(idComune);
            }
            else { MessageBox.Show("Errore nel caricamento dei dati del grafico selezionato"); }

            //recupera che tipo di dato caricare
            NavigationContext.QueryString.TryGetValue("dato", out tipoDato);
            //carica dati
            LoadData();


            /*
             * 
            string debug="";
            foreach(int w in anni)debug+=w.ToString();
            debug += "\n\n";
            foreach(int w in valori)debug+=w.ToString();
            MessageBox.Show(debug);
             * 
             */

            //imposta punti grafico
            Grafico.Points = calcolaPunti();
            //imposta titolo pagina
            PageTitle.Text = comune.Comune;
            if (comune.Comune.Length > 9) PageTitle.FontSize = 50;
            if (comune.Comune.Length > 18) PageTitle.FontSize = 40;

            
            Statistiche.Text = "Statistiche:\n"+Statistica();

        }


        public void LoadData()
        {
            valori = csv.LoadDati(idComune, tipoDato)[0];
            anni = csv.LoadDati(idComune, tipoDato)[1];
        }

        public PointCollection calcolaPunti() {
            List<int> pop = csv.LoadPopolazione(idComune);

            PointCollection listaPunti = new PointCollection();
            double coordX = 0;
            double coordY = 0;

            double range = Max() - Min();
            
            for (int w = 3; w < valori.Count; w++)
            {
                //Calcolo ascisse
                coordX = larghezzaGriglia * (w-3);

                //Calcolo ordinate
                double invertito = (valori[w]-Min())* altezzaCanvas / range;
                double value = altezzaCanvas - invertito*0.8;
                coordY = 300 - value;
                Point z = new Point(coordX, coordY);
                listaPunti.Add(z);
            }
            return listaPunti;
        }

        public Graph()
        {
            InitializeComponent();
            larghezzaGriglia = 40;
            larghezzaCanvas = Caneva.Width;
            altezzaCanvas = Caneva.Height;
        }

    }
}