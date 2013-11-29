using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace csvReading.ViewModel
{
    public class ComuniVM : INotifyPropertyChanged
    {
        public ComuniVM()
        {
            this.Items = new ObservableCollection<ComuneVM>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ComuneVM> Items { get; private set; }
        private List<string> comuni;
        private List<int> codici;



        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Carica in una collection tutti i comuni della provincia selezionata!
        /// </summary>
        public void LoadData(string prov)
        {
            //carica la lista dei comuni in line 1 e in line 2 la lista dei codici istat

            codici = CsvReader.Instance.caricaIstat();
            comuni = CsvReader.Instance.caricaComuni();

            switch (prov)
            {
                case "ve":
                    {
                        for (int z = 0; z < codici.Count; z++)
                        {
                            if (codici[z] / 1000 == 27)
                            {
                                this.Items.Add(new ComuneVM() { LineOne = comuni[z], LineTwo = codici[z].ToString() });
                            }
                        }//end of for
                        break;
                    }//end of case ve

                case "pd":
                    {
                        for (int z = 0; z < codici.Count; z++)
                        {
                            if (codici[z] / 1000 == 28)
                            {
                                this.Items.Add(new ComuneVM() { LineOne = comuni[z], LineTwo = codici[z].ToString() });
                            }
                        }//end of for
                        break;
                    }//end of case pd

                case "tv":
                    {
                        for (int z = 0; z < codici.Count; z++)
                        {
                            if (codici[z] / 1000 == 26)
                            {
                                this.Items.Add(new ComuneVM() { LineOne = comuni[z], LineTwo = codici[z].ToString() });
                            }
                        }//end of for
                        break;
                    }//end of case tv

                case "ro":
                    {
                        for (int z = 0; z < codici.Count; z++)
                        {
                            if (codici[z] / 1000 == 29)
                            {
                                this.Items.Add(new ComuneVM() { LineOne = comuni[z], LineTwo = codici[z].ToString() });
                            }
                        }//end of for
                        break;
                    }//end of case ro

                case "vi":
                    {
                        for (int z = 0; z < codici.Count; z++)
                        {
                            if (codici[z] / 1000 == 24)
                            {
                                this.Items.Add(new ComuneVM() { LineOne = comuni[z], LineTwo = codici[z].ToString() });
                            }
                        }//end of for
                        break;
                    }//end of case vi

                case "bl":
                    {
                        for (int z = 0; z < codici.Count; z++)
                        {
                            if (codici[z] / 1000 == 25)
                            {
                                this.Items.Add(new ComuneVM() { LineOne = comuni[z], LineTwo = codici[z].ToString() });
                            }
                        }//end of for
                        break;
                    }//end of case bl

                case "vr":
                    {
                        for (int z = 0; z < codici.Count; z++)
                        {
                            if (codici[z] / 1000 == 23)
                            {
                                this.Items.Add(new ComuneVM() { LineOne = comuni[z], LineTwo = codici[z].ToString() });
                            }
                        }//end of for
                        break;
                    }//end of case vr

                default: {
                    this.Items.Add(new ComuneVM() { LineOne = "Nessun comune", LineTwo = "" });
                    break;
                }
            }
            // Sample data; replace with real data
           
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        internal void Cancella()
        {
            Items.Clear();
            IsDataLoaded = false;
        }
    }
}
