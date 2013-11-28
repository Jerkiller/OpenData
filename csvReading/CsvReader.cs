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








        public Record LoadLastData(int comune)
        {
            Record dati = null;
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
                                if (/*(line.Substring(0, 4) == "2003") &&*/ (line.Substring(5, 5) == comune.ToString()))
                                {
                                    dati=new Record(SplitLine(line));
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

            return dati;
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


        public List<string> SplitLine(string csvLine)
        {
            List<string> line = new List<string>();
            string[] elements = csvLine.Split(';');
            foreach (string s in elements) line.Add(s);
            return line;
        }





    }
}
