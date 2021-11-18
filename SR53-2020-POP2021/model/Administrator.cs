using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    public class Administrator : RegistrovaniKorisnik
    {
        public Administrator() : base()
        {

        }

        public Administrator(string ime, string prezime, string jmbg, EPol pol, Adresa adresa, string email, string lozinka, ETipKorisnika tipKorisnika) : base(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika)
        {

        }

        public string AdminZaUpisUFajl()
        {
            return Ime + "|" + Prezime + "|" + JMBG + "|" + Pol + "|" + Adresa.ID + "|" + Email + "|" + Lozinka + "|" + TipKorisnika;
        }

        public override string ToString()
        {
            return "Administrator{" + "Ime='" + Ime + '\'' + ", Prezime='" + Prezime + '\'' + ", JMBG='" + JMBG + '\'' + ", Pol='" + Pol + '\'' + ", Adresa= " + Adresa.ToString() + ", Email='" + Email + '\'' + ", Lozinka='" + Lozinka + '\'' + ", TipKorisnika='" + TipKorisnika + '\'' + '}';
        }
    }
}
