using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace csvReading
{
    class CsvReader
    {

#region Singleton definition
        /* Singleton*/
    private static CsvReader instance = null;


    

    private CsvReader()
    {
    }

    public static CsvReader Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new CsvReader();
            }
            return instance;
        }
    }

#endregion


        /// <returns>Il numero di abitanti di tale provincia (cod istat) in tale anno</returns>
        public int AbitantiProvincia(int codProv, int anno)
        {
            int contatore = 0;
            try
            {
                var ResourceStream = Application.GetResourceStream(new Uri("file.txt", UriKind.Relative));

                if (ResourceStream != null)
                {
                    using (Stream myFileStream = ResourceStream.Stream)
                    {

                        if (myFileStream.CanRead)
                        {
                            StreamReader myStreamReader = new StreamReader(myFileStream);

                            string line;
                            myStreamReader.ReadLine(); //spreco intestazione

                            //Trova tutti i record con comune e anno ivi specificati, scrivi sempre i dati sullo stesso oggetto
                            line = myStreamReader.ReadLine();
                            while ((line = myStreamReader.ReadLine()) != null)
                            {
                                //substring ha come argomenti il carattere giusto prima e la lungh della sottostringa
                                if ((line.Substring(0, 4) == anno.ToString()) && (line.Substring(5, 2) == codProv.ToString()))
                                {
                                    List<string> dato = new List<string>();
                                    dato = SplitLine(line);
                                    contatore += Parse(dato[10]);
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n\n" + e.Data + "\n\n" + e.StackTrace);
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return contatore;
        }

        /// <returns>L'ultimo record temporale di un comune</returns>
        public Record LoadLastData(int comune)
        {
            Record ultimo = null;
            Record penultimo = null;
            try
            {

                var ResourceStream = Application.GetResourceStream(new Uri("file.txt", UriKind.Relative));

                if (ResourceStream != null)
                {
                    using (Stream myFileStream = ResourceStream.Stream)
                    {

                        if (myFileStream.CanRead)
                        {
                            StreamReader myStreamReader = new StreamReader(myFileStream);

                            string line;
                            myStreamReader.ReadLine(); //spreco intestazione
                            
                            //Trova tutti i record con comune e anno ivi specificati, scrivi sempre i dati sullo stesso oggetto
                            line = myStreamReader.ReadLine();
                            while ((line = myStreamReader.ReadLine()) != null)
                            {
                                //substring ha come argomenti il carattere giusto prima e la lungh della sottostringa
                                if (line.Substring(5, 5) == comune.ToString())
                                {
                                    if(ultimo!=null)penultimo = ultimo;
                                    ultimo = new Record(SplitLine(line));
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n\n" + e.Data + "\n\n" + e.StackTrace);
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            if (ultimo.Sesso == "Non rilevato") return ultimo;
            return penultimo.somma(ultimo);
        }

        /// <returns>Una lista di record di tale comune</returns>
        public List<Record> LoadRecord(int comune) {
            List<Record> dati = new List<Record>();
            try
            {

                var ResourceStream = Application.GetResourceStream(new Uri("file.txt", UriKind.Relative));

                if (ResourceStream != null)
                {
                    using (Stream myFileStream = ResourceStream.Stream)
                    {
                        if (myFileStream.CanRead)
                        {
                            StreamReader myStreamReader = new StreamReader(myFileStream);
                            string line;
                            myStreamReader.ReadLine(); //spreco intestazione

                            line = myStreamReader.ReadLine();
                            while ((line = myStreamReader.ReadLine()) != null)
                            {
                                //substring ha come argomenti il carattere giusto prima e la lungh della sottostringa
                                if (line.Substring(5, 5) == comune.ToString())
                                {
                                    dati.Add(new Record(SplitLine(line)));
                                }
                            }
                            //ho scorso tutti i valori
                        }
                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n\n" + e.Data + "\n\n" + e.StackTrace);
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return dati;

        
        }

        /// <returns>Data una lista di record restituisce una lista
        /// con un record per anno con i dati dei due sessi sommati</returns>
        public List<Record> AggregaSessi(List<Record> lista)
        {
            Record primoVal = null;
            List<Record> listaAggregata = new List<Record>();

            for(int w=0;w<lista.Count;w++)
            {
                if(((lista[w].Sesso=="Femmina") ||(lista[w].Sesso=="Femmina"))&&(primoVal == null))
                {
                    primoVal = lista[w];
                    continue;
                }

                if(((lista[w].Sesso=="Femmina") ||(lista[w].Sesso=="Femmina"))&&(primoVal != null))
                {
                    listaAggregata.Add(primoVal.somma(lista[w]));
                    primoVal = null;
                }

                if (lista[w].Sesso == "Non rilevato")
                {
                    listaAggregata.Add(lista[w]);
                }
            }//end for
            return listaAggregata;
        }

        /// <returns>Una lista di interi a tre a tre costituiti da anno, num abitanti, e un delimitatore=0
        /// es. 1990 23435 0 1991 24123 0 ...</returns>
        public List<int>[] LoadDati(int comune, string dato)
        {
            List<int> listaValori = new List<int>();
            List<int> listaAnni = new List<int>();
            
            List<Record> dati = LoadRecord(comune);
            List<Record> datiAggregati = AggregaSessi(dati);


            //riempi listaAnni
            foreach (Record w in datiAggregati) listaAnni.Add(w.Anno);

            //riempi listaValori
            switch (dato)
            {
                case ("popolazione"): {
                    foreach(Record w in datiAggregati)
                    listaValori.Add(w.PopolazioneMedia); break;
                }
                case ("nati"):
                    {
                        foreach (Record w in datiAggregati)
                            listaValori.Add(w.NatiVivi); break;
                    }
                case ("morti"):
                    {
                        foreach (Record w in datiAggregati)
                            listaValori.Add(w.Morti); break;
                    }
                case ("iscritti"):
                    {
                        foreach (Record w in datiAggregati)
                            listaValori.Add(w.Iscritti); break;
                    }
                case ("cancellati"):
                    {
                        foreach (Record w in datiAggregati)
                            listaValori.Add(w.Cancellati); break;
                    }
                default:
                    {
                        MessageBox.Show("Errore nel caricamento del dato"); break;
                    }
            }

            //compongo l'array di 2 liste valori/anni
            List<int>[] array = new List<int>[2];
            array[0] = listaValori;
            array[1] = listaAnni;
            return array;

        }

        /// <returns>Una lista di interi a tre a tre costituiti da anno, num abitanti, e un delimitatore=0
        /// es. 1990 23435 0 1991 24123 0 ...</returns>
        public List<int> LoadPopolazione(int comune)
        {
            List<int> listaPopolazione = new List<int>();

            try
            {

                var ResourceStream = Application.GetResourceStream(new Uri("file.txt", UriKind.Relative));

                if (ResourceStream != null)
                {
                    using (Stream myFileStream = ResourceStream.Stream)
                    {

                        if (myFileStream.CanRead)
                        {
                            StreamReader myStreamReader = new StreamReader(myFileStream);

                            string line;
                            myStreamReader.ReadLine(); //spreco intestazione

                           

                            //int cont = 0;
                            line = myStreamReader.ReadLine();
                            int primoVal = 0;
                            while ((line = myStreamReader.ReadLine()) != null)
                            {
                                //cont++;

                                //substring ha come argomenti il carattere giusto prima e la lungh della sottostringa
                                if (/*(line.Substring(0, 4) == "2003") &&*/ (line.Substring(5, 5) == comune.ToString()))
                                {
                                    Record singolo = new Record(SplitLine(line));

                                    if (((singolo.Sesso == "Femmina") || (singolo.Sesso == "Maschio")) && (primoVal == 0))
                                    {
                                        primoVal = singolo.PopolazioneMedia;
                                        continue;
                                    }

                                    if (((singolo.Sesso == "Femmina") || (singolo.Sesso == "Maschio")) && (primoVal != 0))
                                    {
                                        listaPopolazione.Add(singolo.Anno);
                                        listaPopolazione.Add(primoVal + singolo.PopolazioneMedia);
                                        listaPopolazione.Add(0);
                                        primoVal = 0;
                                    }

                                    if (singolo.Sesso == "Non rilevato")
                                    {
                                        listaPopolazione.Add(singolo.Anno);
                                        listaPopolazione.Add(singolo.PopolazioneMedia);
                                        listaPopolazione.Add(0);
                                    }
                                }
                            }
                            
                            //ho scorso tutti i valori
                        }
                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n\n" + e.Data + "\n\n" + e.StackTrace);
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return listaPopolazione;

        }


        /// <returns>Una lista di stringhe date dall'esplosione della stringa principale</returns>
        public List<string> SplitLine(string csvLine)
        {
            List<string> line = new List<string>();
            string[] elements = csvLine.Split(';');
            foreach (string s in elements) line.Add(s);
            return line;
        }


        /// <returns>Una lista di interi con tutti i codici di tutti i comuni con lo stesso ordine
        /// della lista restituita dal metodo caricaComuni(). Così se ottengo due liste a e b, a[5]
        /// sarà il codice relativo al comune b[5].</returns>
        public List<int> caricaIstat()
        {
            List<int> comuni = new List<int>();
            try
            {

                var ResourceStream = Application.GetResourceStream(new Uri("file.txt", UriKind.Relative));

                if (ResourceStream != null)
                {
                    using (Stream myFileStream = ResourceStream.Stream)
                    {

                        if (myFileStream.CanRead)
                        {
                            StreamReader myStreamReader = new StreamReader(myFileStream);

                            string line;
                            myStreamReader.ReadLine(); //spreco intestazione

                            //Trova tutti i record con comune e anno ivi specificati, scrivi sempre i dati sullo stesso oggetto
                            line = myStreamReader.ReadLine();
                            while ((line = myStreamReader.ReadLine()) != null)
                            {
                                //substring ha come argomenti il carattere giusto prima e la lungh della sottostringa
                                if(line.Substring(0, 4) == "1990")
                                {
                                    comuni.Add(Int32.Parse(line.Substring(5, 5)));
                                }//end if body
                            }//end while body
                        }//end if leggi stream
                    }//end of using
                }//end if
            }//end try

            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n\n" + e.Data + "\n\n" + e.StackTrace);
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return comuni;
}

        public List<string> caricaComuni()
        {
            List<string> comuni = new List<string>();
            try
            {

                var ResourceStream = Application.GetResourceStream(new Uri("file.txt", UriKind.Relative));

                if (ResourceStream != null)
                {
                    using (Stream myFileStream = ResourceStream.Stream)
                    {

                        if (myFileStream.CanRead)
                        {
                            StreamReader myStreamReader = new StreamReader(myFileStream);

                            string line;
                            myStreamReader.ReadLine(); //spreco intestazione

                            //Trova tutti i record con comune e anno ivi specificati, scrivi sempre i dati sullo stesso oggetto
                            line = myStreamReader.ReadLine();
                            while ((line = myStreamReader.ReadLine()) != null)
                            {
                                //substring ha come argomenti il carattere giusto prima e la lungh della sottostringa
                                if (line.Substring(0, 4) == "1990")
                                {
                                    string[] oggetto = new string[6];
                                    oggetto = line.Split(';');
                                    comuni.Add(oggetto[2]);
                                }//end if body
                            }//end while body
                        }//end if leggi stream
                    }//end of using
                }//end if
            }//end try

            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n\n" + e.Data + "\n\n" + e.StackTrace);
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return comuni;
        }

        public int Parse(string numero)
        {
            numero.Trim('.');
            List<string> a = (numero.Split('.')).ToList();
            string z = "";
            foreach (string h in a) z += h;
            return Int32.Parse(z);
        }

    }
}
