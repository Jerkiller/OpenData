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
        int max = 0;
        int min = int.MaxValue;
        int idComune;
        Record comune = null;
        CsvReader csv = CsvReader.Instance;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string codice;
            // recupero il comune di cui cercare il tutto
            if (NavigationContext.QueryString.TryGetValue("comune", out codice))
            {
                idComune = Int32.Parse(codice);

                comune = csv.LoadLastData(idComune);
            }
            else { idComune = 25011; }

            //imposta punti grafico
            Grafico.Points = calcolaPunti();
            //imposta titolo pagina
            PageTitle.Text = comune.Comune;
            if (comune.Comune.Length > 9) PageTitle.FontSize = 50;
        }

        public PointCollection calcolaPunti() {
            List<int> pop = csv.LoadPopolazione(idComune);

            PointCollection listaPunti = new PointCollection();
            double coordX = 0;
            double coordY = 0;


            for (int w = 1; w < pop.Count; w += 3)
            {
                if (pop[w] < min) min = pop[w];
                if (pop[w] > max) max = pop[w];
            }
            double range = max - min;
            double fattoreScala = double.Parse("440") / range;
            // MessageBox.Show("Il massimo valore e' " + max + ", il minimo è " + min + "\nMentre il range usato è " + range+". Fattore di scala è "+fattoreScala.ToString());


            for (int y = 0; y < pop.Count; y++)
            {

                if (y % 3 == 0)
                {

                    int anno = pop[y];
                    int z = y / 3;
                    coordX = 40 * z;
                }

                if (y % 3 == 1)
                {

                    double value = (pop[y] - min) * fattoreScala;
                    coordY = 300 - value;
                    Point z = new Point(coordX, coordY);
                    listaPunti.Add(z);

                }
                if (y % 3 == 2)
                {
                    //do nothing
                }
            }
            /* Codice che serve a selezionare solo gli ultimi 12 anni..
            PointCollection s = new PointCollection();
            for (int w=0; w < listaPunti.Count; w++)
            {
                if (w >= (listaPunti.Count - 12)) s.Add(listaPunti[w]);
            }
            return s;*/
            return listaPunti;
        }

        public Graph()
        {
            InitializeComponent();
        }

    }
}