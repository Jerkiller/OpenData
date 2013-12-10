using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

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
            string debug="";
            foreach(int w in anni)debug+=w.ToString();
            debug += "\n\n";
            foreach(int w in valori)debug+=w.ToString();
            MessageBox.Show(debug);
            */

            //imposta punti grafico
            Grafico.Points = calcolaPunti();
            //imposta etichette
            AggiornaEtichette();

            //Imposta un colore a caso al grafico
            SetRandomColor();

            //imposta titolo pagina
            PageTitle.Text = comune.Comune;
            if (comune.Comune.Length > 9) PageTitle.FontSize = 50;
            if (comune.Comune.Length > 18) PageTitle.FontSize = 40;

            ApplicationTitle.Text = tipoDato.ToUpper();
            
            Statistiche.Text = "Statistiche:\n"+Statistica();

        }


        public void LoadData()
        {
            valori = csv.LoadDati(idComune, tipoDato)[0];
            anni = csv.LoadDati(idComune, tipoDato)[1];
        }

        public PointCollection calcolaPunti() {
            //List<int> pop = csv.LoadPopolazione(idComune);

            PointCollection listaPunti = new PointCollection();
            double coordX = 0;
            double coordY = 0;

            double range = Max() - Min();

            //MessageBox.Show("Primovalore "+primoValore+"\nche è uguale a: "+anni[primoValore]);
            for (int w = 0; w < valori.Count; w++)
            {
                //Calcolo ascisse
                coordX = larghezzaGriglia * w;

                //Calcolo ordinate
                double invertito = (valori[w]-Min())* altezzaCanvas / range;
                coordY = altezzaCanvas - invertito * 0.9;
                //MessageBox.Show("valore " + w + "\n(" + valori[w] + " , " + anni[w] + ")\ncioè (" + coordX + " , " + coordY+")");
                Point z = new Point(coordX, coordY);
                listaPunti.Add(z);
            }
            return listaPunti;
        }

        public void AggiornaEtichette() {
             int val=valori.Count;
             int min = Min();
             int max = Max();
             double step = (double)((max - min))/8.000;
             Val0.Text = Math.Round((double)min).ToString();
             Val1.Text=Math.Round(min+step).ToString();
             Val2.Text=Math.Round(min+step*2).ToString();
             Val3.Text = Math.Round(min + step * 3).ToString();
             Val4.Text = Math.Round(min + step * 4).ToString();
             Val5.Text = Math.Round(min + step * 5).ToString();
             Val6.Text = Math.Round(min + step * 6).ToString();
             Val7.Text = Math.Round(min + step * 7).ToString();
             Val8.Text = Math.Round((double)max).ToString();
             Val9.Text = Math.Round(max + step).ToString();

             Anno0.Text = anni[val - 23].ToString();
             Anno1.Text = anni[val - 22].ToString();
             Anno2.Text = anni[val - 21].ToString();
             Anno3.Text = anni[val - 20].ToString();
             Anno4.Text = anni[val - 19].ToString();
             Anno5.Text = anni[val - 18].ToString();
             Anno6.Text = anni[val - 17].ToString();
             Anno7.Text = anni[val - 16].ToString();
             Anno8.Text = anni[val - 15].ToString();
             Anno9.Text = anni[val - 14].ToString();
             Anno10.Text = anni[val - 13].ToString();
             Anno11.Text = anni[val - 12].ToString();
             Anno12.Text = anni[val - 11].ToString();
             Anno13.Text = anni[val - 10].ToString();
             Anno14.Text = anni[val - 9].ToString();
             Anno15.Text = anni[val - 8].ToString();
             Anno16.Text = anni[val - 7].ToString();
             Anno17.Text = anni[val - 6].ToString();
             Anno18.Text = anni[val - 5].ToString();
             Anno19.Text = anni[val - 4].ToString();
             Anno20.Text = anni[val - 3].ToString();
             Anno21.Text = anni[val - 2].ToString();
             Anno22.Text = anni[val - 1].ToString();
        }


        public Graph()
        {
            InitializeComponent();
            larghezzaGriglia = 40;
            larghezzaCanvas = Caneva.Width;
            altezzaCanvas = Caneva.Height;
        }


        public void SetRandomColor()
        {
            Random generator = new Random();
            int num=generator.Next(0,9);
            switch(num)
            {
                case (0): { Grafico.Stroke = new SolidColorBrush(Colors.Blue); break; }
                case (1): { Grafico.Stroke = new SolidColorBrush(Colors.Red); break; }
                case (2): { Grafico.Stroke = new SolidColorBrush(Colors.Green); break; }
                case (3): { Grafico.Stroke = new SolidColorBrush(Colors.White); break; }
                case (4): { Grafico.Stroke = new SolidColorBrush(Colors.Yellow); break; }
                case (5): { Grafico.Stroke = new SolidColorBrush(Colors.Purple); break; }
                case (6): { Grafico.Stroke = new SolidColorBrush(Colors.Orange); break; }
                case (7): { Grafico.Stroke = new SolidColorBrush(Colors.Magenta); break; }
                case (8): { Grafico.Stroke = new SolidColorBrush(Colors.Cyan); break; }
                case (9): { Grafico.Stroke = new SolidColorBrush(Colors.Gray); break; }
                default: { Grafico.Stroke = new SolidColorBrush(Colors.Blue); break; }
            
            }
        }

    }
}