using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    [Serializable]
    public class RegistrovaniKorisnik
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

        private bool aktivan;

        public bool Aktivan
        {
            get { return aktivan; }
            set { aktivan = value; }
        }


        public RegistrovaniKorisnik()
        {

        }

        public string KorisnikZaUpisUFajl()
        {
            return Ime + "|" + Prezime + "|" + JMBG + "|" + Pol + "|" + Adresa.ID + "|" + Email + "|" + Lozinka + "|" + TipKorisnika + "|" + Aktivan;
        }

        public override string ToString()
        {
            return "Korisnik{" + "Ime='" + Ime + '\'' + ", Prezime='" + Prezime + '\'' + ", JMBG='" + JMBG + '\'' + ", Pol='" + Pol + '\'' + ", Adresa= " + Adresa.ToString() + ", Email='" + Email + '\'' + ", Lozinka='" + Lozinka + '\'' + ", TipKorisnika='" + TipKorisnika + '\'' + ", Aktivan='" + Aktivan + '\'' + '}';
        }

        public RegistrovaniKorisnik Clone()
        {
            RegistrovaniKorisnik kopija = new RegistrovaniKorisnik();
            kopija.Ime = Ime;
            kopija.Prezime = Prezime;
            kopija.JMBG = JMBG;
            kopija.Pol = Pol;
            kopija.Adresa = Adresa;
            kopija.Email = Email;
            kopija.Lozinka = Lozinka;
            kopija.TipKorisnika = TipKorisnika;
            kopija.Aktivan = Aktivan;

            return kopija;
        }

    }
}
