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
            media = (double)(media/(double)valori.Count);
            return Math.Round(media,2);
        }

        public double Varianza()
        {
            double media = Media();
            double var = 0;
            foreach (int w in valori) var = (w-media)*(w-media);
            var = (double)(var / (double)valori.Count);
            return Math.Round(var, 2);
        }


        /// <returns>l'anno in cui è avvenuta tale prima ricorrenza</returns>
        public int Anno(int dato)
        {
            for (int w=0; w < valori.Count; w++)
            {
                if (valori[w] == dato)
                {
                    return anni[w];
                }
            }
            return -1;
        }


        public string Statistica()
        {
            string stat = "";

            stat += "Massimo: " + Max().ToString();
            int anno = Anno(Max());
            if (anno > 0) { stat += " raggiunto nel " + anno; }
            stat += "\nMinimo: " + Min().ToString();
            anno = Anno(Min());
            if (anno > 0) { stat += " raggiunto nel " + anno; }
            stat += "\nRange: " + (Max() - Min()).ToString();
            stat += "\nMedia: " + Media();
            stat += "\nVarianza: " + Varianza();

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
            
            for (int w = 2; w < valori.Count; w++)
            {
                //Calcolo ascisse
                coordX = larghezzaGriglia * (w-2);

                //Calcolo ordinate
                double invertito = (valori[w]-Min())* altezzaCanvas / range;
                coordY = altezzaCanvas - invertito * 0.8;
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