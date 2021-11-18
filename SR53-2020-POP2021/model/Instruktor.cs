using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    public class Instruktor : RegistrovaniKorisnik
    {
        private List<Trening> listaTreninga;

        public List<Trening> ListaTreninga
        {
            get { return listaTreninga; }
            set { listaTreninga = value; }
        }
        public Instruktor() : base()
        {

        }

        public Instruktor(string ime, string prezime, string jmbg, EPol pol, Adresa adresa, string email, string lozinka, ETipKorisnika tipKorisnika, List<Trening> listaTreninga) : base(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika)
        {
            ListaTreninga = listaTreninga;
        }

        public string InstruktorZaUpisUFajl()
        {
            return Ime + "|" + Prezime + "|" + JMBG + "|" + Pol + "|" + Adresa.ID + "|" + Email + "|" + Lozinka + "|" + TipKorisnika;
        }

        public override string ToString()
        {
            String s = "Instruktor{" + "Ime='" + Ime + '\'' + ", Prezime='" + Prezime + '\'' + ", JMBG='" + JMBG + '\'' + ", Pol='" + Pol + '\'' + ", Adresa= " + Adresa.ToString() + ", Email='" + Email + '\'' + ", Lozinka='" + Lozinka + '\'' + ", TipKorisnika='" + TipKorisnika + '\'' + ", IDTreninga= ";
            foreach(Trening trening in ListaTreninga) {
                s += trening.ID + ", ";
            }
            return s + '}';
        }
    }
}
