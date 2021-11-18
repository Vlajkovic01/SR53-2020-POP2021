using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    public abstract class RegistrovaniKorisnik
    {
        private string ime;

        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        private string prezime;

        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }

        private string jmbg;

        public string JMBG
        {
            get { return jmbg; }
            set { jmbg = value; }
        }

        private EPol pol;

        public EPol Pol
        {
            get { return pol; }
            set { pol = value; }
        }

        private Adresa adresa;

        public Adresa Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string lozinka;

        public string Lozinka
        {
            get { return lozinka; }
            set { lozinka = value; }
        }

        private ETipKorisnika tipKorisnika;

        public ETipKorisnika TipKorisnika
        {
            get { return tipKorisnika; }
            set { tipKorisnika = value; }
        }

        public RegistrovaniKorisnik()
        {

        }

        public RegistrovaniKorisnik(string ime, string prezime, string jmbg, EPol pol, Adresa adresa, string email, string lozinka, ETipKorisnika tipKorisnika)
        {
            Ime = ime;
            Prezime = prezime;
            JMBG = jmbg;
            Pol = pol;
            Adresa = adresa;
            Email = email;
            Lozinka = lozinka;
            TipKorisnika = tipKorisnika;
        }

        public override string ToString()
        {
            return "Korisnik{" + "Ime='" + Ime + '\'' + ", Prezime='" + Prezime + '\'' + ", JMBG='" + JMBG + '\'' + ", Pol='" + Pol + '\'' + ", Adresa= " + Adresa.ToString() + ", Email='" + Email + '\'' + ", Lozinka='" + Lozinka + '\'' + ", TipKorisnika='" + TipKorisnika + '\'' + '}';
        }

    }
}
