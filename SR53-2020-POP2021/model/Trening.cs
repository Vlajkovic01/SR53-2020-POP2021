using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    [Serializable]
    public class Trening
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string datumTreninga;

        public string DatumTreninga
        {
            get { return datumTreninga; }
            set { datumTreninga = value; }
        }

        private string vremePocetkaTreninga;

        public string VremePocetkaTreninga
        {
            get { return vremePocetkaTreninga; }
            set { vremePocetkaTreninga = value; }
        }

        private int trajanjeTreninga; //u minutama

        public int TrajanjeTreninga
        {
            get { return trajanjeTreninga; }
            set { trajanjeTreninga = value; }
        }

        private EStatusTreninga statusTreninga;

        public EStatusTreninga StatusTreninga
        {
            get { return statusTreninga; }
            set { statusTreninga = value; }
        }

        private Instruktor instruktor;

        public Instruktor Instruktor
        {
            get { return instruktor; }
            set { instruktor = value; }
        }

        private Polaznik polaznik;

        public Polaznik Polaznik
        {
            get { return polaznik; }
            set { polaznik = value; }
        }
        private bool aktivan;

        public bool Aktivan
        {
            get { return aktivan; }
            set { aktivan = value; }
        }


        //public Trening()
        //{

        //}

        //public Trening(int id, string datumTreninga, string pocetakTreninga, int trajanjeTreninga, EStatusTreninga statusTreninga, Instruktor instruktor, Polaznik polaznik, bool aktivan)
        //{
        //    ID = id;
        //    DatumTreninga = datumTreninga;
        //    VremePocetkaTreninga = pocetakTreninga;
        //    TrajanjeTreninga = trajanjeTreninga;
        //    StatusTreninga = statusTreninga;
        //    Instruktor = instruktor;
        //    Polaznik = polaznik;
        //    Aktivan = aktivan;
        //}

        public string TreningZaUpisUFajl()
        {
            return ID + "|" + DatumTreninga + "|" + VremePocetkaTreninga + "|" + TrajanjeTreninga + "|" + StatusTreninga + "|" + Instruktor.Korisnik.JMBG + "|" + Polaznik.Korisnik.JMBG + "|" + Aktivan;
        }

        public override string ToString()
        {
            return "Trening{" + "ID='" + ID + '\'' + ", DatumTreninga='" + DatumTreninga + '\'' + ", PocetakTreninga='" + VremePocetkaTreninga + '\'' + ", TrajanjeTreninga='" + TrajanjeTreninga +"min'" + ", StatusTreninga='" + StatusTreninga + '\'' + ", " + instruktor.ToString() + ", " + Polaznik.ToString() + ", Aktivan='" + Aktivan + '\'';
        }

        public Trening Clone()
        {
            Trening kopija = new Trening();
            kopija.ID = ID;
            kopija.DatumTreninga = DatumTreninga;
            kopija.VremePocetkaTreninga = VremePocetkaTreninga;
            kopija.StatusTreninga = StatusTreninga;
            kopija.Instruktor = Instruktor;
            kopija.Polaznik = Polaznik;
            kopija.Aktivan = Aktivan;

            return kopija;

        }
    }
}
