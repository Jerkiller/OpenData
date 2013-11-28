using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace csvReading
{
    class Record
    {
        /*
        private int anno;
        private string comune;
        private int codiceIstat;
        private string sesso;
        private int popolazioneInizioAnno;
        private int natiVivi;
        private int morti;
        private int iscritti;
        private int cancellati;
        private int popolazioneFineAnno;
        private int popolazioneMedia;
        */



        public Record(List<string> dati)
        {
            this.Comune = dati[2];
            this.Sesso = dati[3];
            this.Anno = Int32.Parse(dati[0]);
            this.CodiceIstat = Int32.Parse(dati[1]);
            this.Morti = Parse(dati[6]);
            this.PopolazioneInizioAnno = Parse(dati[4]);
            this.NatiVivi = Parse(dati[5]);
            this.Morti = Parse(dati[6]);
            this.Iscritti = Parse(dati[7]);
            this.Cancellati = Parse(dati[8]);
            this.PopolazioneFineAnno = Parse(dati[9]);
            this.PopolazioneMedia = Parse(dati[10]);
        }



        public int Anno{get; set;}
        public string Comune { get; set; }
        public int CodiceIstat { get; set; }
        public string Sesso { get; set; }
        public int PopolazioneInizioAnno { get; set; }
        public int NatiVivi { get; set; }
        public int Morti { get; set; }
        public int Iscritti { get; set; }
        public int Cancellati { get; set; }
        public int PopolazioneFineAnno { get; set; }
        public int PopolazioneMedia { get; set; }


        public int Parse(string numero) {
            numero.Trim('.');
            List<string> a = (numero.Split('.')).ToList();
            string z = "";
            foreach (string h in a) z += h;
            return Int32.Parse(z);
        }



        public void PrintRecord() {
            MessageBox.Show("Anno: " + Anno + "\n" + "Comune: " + Comune + "\n" + "Sesso: " + Sesso + "\n" + "Codice Istat: " + CodiceIstat+"\n\n"
                + "PopInizio: " + PopolazioneInizioAnno + "\n" + "PopFine: " + PopolazioneFineAnno + "\n" + "Popmedia: " + PopolazioneMedia + "\n\n"
                + "Nati: " + NatiVivi + "\n" + "Morti: " + Morti + "\n" + "Iscritti: " + Iscritti + "\n" + "Cancellati: " + Cancellati);
        }
    }
}
