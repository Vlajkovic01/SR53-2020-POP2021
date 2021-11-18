using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    public class Polaznik : RegistrovaniKorisnik
    {

        private List<Trening> listaRezervisanihTreninga;

        public List<Trening> ListaRezervisanihTreninga
        {
            get { return listaRezervisanihTreninga; }
            set { listaRezervisanihTreninga = value; }
        }

        public Polaznik() : base()
        {

        }

        public Polaznik(string ime, string prezime, string jmbg, EPol pol, Adresa adresa, string email, string lozinka, ETipKorisnika tipKorisnika, List<Trening> rezervisaniTreninzi) : base(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika)
        {
            ListaRezervisanihTreninga = rezervisaniTreninzi;
        }

        public string PolaznikZaUpisUFajl()
        {
            return Ime + "|" + Prezime + "|" + JMBG + "|" + Pol + "|" + Adresa.ID + "|" + Email + "|" + Lozinka + "|" + TipKorisnika;
        }

        public override string ToString()
        {
            String s = "Polaznik{" + "Ime='" + Ime + '\'' + ", Prezime='" + Prezime + '\'' + ", JMBG='" + JMBG + '\'' + ", Pol='" + Pol + '\'' + ", Adresa= " + Adresa.ToString() + ", Email='" + Email + '\'' + ", Lozinka='" + Lozinka + '\'' + ", TipKorisnika='" + TipKorisnika + '\'' + ", IDTreninga= ";
            foreach (Trening trening in ListaRezervisanihTreninga)
            {
                s += trening.ID + ", ";
            }
            return s + '}';
        }

    }
}
