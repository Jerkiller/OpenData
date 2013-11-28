using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Net;
using System.Net;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace csvReading
{
    public partial class Graph : PhoneApplicationPage
    {
        public Graph()
        {
            InitializeComponent();
            CsvReader csv = CsvReader.Instance;
            csv.Load(27024);
            csv.datasheet[1].PrintRecord();
            csv.datasheet[7].PrintRecord();

            string popolazioniMirano = "";
            List<int> pop = csv.LoadPopolazione(24012);

            PointCollection listaPunti = new PointCollection();
            int coordX=0;
            int coordY=0;
            int ordine=1;
            for (int y = 0; y < pop.Count ;y++)
                {
                
                    if(y%3==0)
                    {
                        int anno=pop[y];
                        int z = y / 3;
                        coordX=40*z;
                    }

                    if (y == 1) { ordine = pop[y] / 1000; }

                    if(y%3==1)
                    {
                        int value = (pop[y] - 1000 * ordine)/8;
                        coordY = 360-value;
                        Point z = new Point(coordX,coordY);
                        //MessageBox.Show(coordY.ToString());
                        listaPunti.Add(z);

                    }
                    if(y%3==2)
                    {
                       //do nothing
                    }

                    popolazioniMirano += (pop[y].ToString() + "\t");
                }

            Grafico.Points = listaPunti;
            Popo.Text = popolazioniMirano;

            //foreach (Record h in csv.datasheet) h.PrintRecord();

            //csv.Load(26021);
            //foreach (Record h in csv.datasheet) h.PrintRecord();
        }

    }
}