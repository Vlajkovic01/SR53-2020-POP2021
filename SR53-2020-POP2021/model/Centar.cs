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

        private List<Instruktor> instruktori;

        public List<Instruktor> Instruktori
        {
            get { return instruktori; }
            set { instruktori = value; }
        }

        private List<Polaznik> polaznici;

        public List<Polaznik> Polaznici
        {
            get { return polaznici; }
            set { polaznici = value; }
        }

        private List<Trening> treninzi;

        public List<Trening> Treninzi
        {
            get { return treninzi; }
            set { treninzi = value; }
        }

        private List<Adresa> listaAdresa;

        public List<Adresa> ListaAdresa
        {
            get { return listaAdresa; }
            set { listaAdresa = value; }
        }


        public Centar()
        {
            this.ID = 0;
            this.NazivCentra = "";
            this.AdresaCentra = new Adresa();
            this.Instruktori = new List<Instruktor>();
            this.Polaznici = new List<Polaznik>();
            this.Treninzi = new List<Trening>();
            this.ListaAdresa = new List<Adresa>();
        }

        public Centar(int id, string naziv, Adresa adresa)
        {
            ID = id;
            NazivCentra = naziv;
            AdresaCentra = adresa;
        }

        public override string ToString()
        {
            return "Centar{" + "ID='" + ID + '\'' + ", Naziv='" + NazivCentra + '\'' + ", Adresa='" + AdresaCentra.ToString() + '\'' + '}';
        }
    }
}
