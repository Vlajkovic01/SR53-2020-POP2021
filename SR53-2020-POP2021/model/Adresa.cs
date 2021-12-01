using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    [Serializable]
    public class Adresa
    {
        public int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string ulica;

        public string Ulica
        {
            get { return ulica; }
            set { ulica = value; }
        }

        public string broj;

        public string Broj
        {
            get { return broj; }
            set { broj = value; }
        }

        public string grad;

        public string Grad
        {
            get { return grad; }
            set { grad = value; }
        }

        public string drzava;

        public string Drzava
        {
            get { return drzava; }
            set { drzava = value; }
        }

        private bool aktivna;

        public bool Aktivna
        {
            get { return aktivna; }
            set { aktivna = value; }
        }

        public string AdresaZaUpisUFajl()
        {
            return ID + "|" + Ulica + "|" + Broj + "|" + Grad + "|" + Drzava + "|" + Aktivna;
        }

        public override string ToString()
        {
            return "Ulica='" + Ulica + '\'' + ", Broj='" + Broj + '\'' + ", Grad='" + Grad + '\'' + ", Drzava='" + Drzava + '\'' + ", ID='" + ID + '\'' + ", Aktivna='" + Aktivna + '\'';
        }

        public Adresa Clone()
        {
            Adresa kopija = new Adresa();
            kopija.ID = ID;
            kopija.Ulica = Ulica;
            kopija.Broj = Broj;
            kopija.Grad = Grad;
            kopija.Drzava = Drzava;
            kopija.Aktivna = Aktivna;

            return kopija;
        }
    }
}
