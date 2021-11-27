using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    public class Centar
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string nazivCentra;

        public string NazivCentra
        {
            get { return nazivCentra; }
            set { nazivCentra = value; }
        }

        private Adresa adresaCentra;

        public Adresa AdresaCentra
        {
            get { return adresaCentra; }
            set { adresaCentra = value; }
        }
        private bool aktivan;

        public bool Aktivan
        {
            get { return aktivan; }
            set { aktivan = value; }
        }

        public Centar()
        {
            
        }
        public string CentarZaUpisUFajl()
        {
            return ID + "|" + NazivCentra + "|" + AdresaCentra.ID + "|" + Aktivan;
        }
        public override string ToString()
        {
            return "Centar{" + "ID='" + ID + '\'' + ", Naziv='" + NazivCentra + '\'' + ", Adresa='" + AdresaCentra.ToString() + '\'' + ", Aktivan='" + Aktivan + "\'" +'}';
        }

        public Centar Clone()
        {
            Centar kopija = new Centar();

            kopija.ID = ID;
            kopija.nazivCentra = NazivCentra;
            kopija.AdresaCentra = AdresaCentra;
            kopija.Aktivan = Aktivan;

            return kopija;
        }
    }
}
