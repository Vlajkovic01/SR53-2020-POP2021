using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    [Serializable]
    public class Instruktor
    {
        private RegistrovaniKorisnik korisnik;

        public RegistrovaniKorisnik Korisnik
        {
            get { return korisnik; }
            set { korisnik = value; }
        }


        private ObservableCollection<Trening> listaTreninga;

        public ObservableCollection<Trening> ListaTreninga
        {
            get { return listaTreninga; }
            set { listaTreninga = value; }
        }



        public string InstruktorZaUpisUFajl()
        {
            return korisnik.Ime + "|" + korisnik.Prezime + "|" + korisnik.JMBG + "|" + korisnik.Pol + "|" + korisnik.Adresa.ID + "|" + korisnik.Email + "|" + korisnik.Lozinka + "|" + korisnik.TipKorisnika + "|" + korisnik.Aktivan;
        }

        public override string ToString()
        {
            String s = "Instruktor{" + "Ime='" + korisnik.Ime + '\'' + ", Prezime='" + korisnik.Prezime + '\'' + ", JMBG='" + korisnik.JMBG + '\'' + ", Pol='" + korisnik.Pol + '\'' + ", Adresa= " + korisnik.Adresa.ToString() + ", Email='" + korisnik.Email + '\'' + ", Lozinka='" + korisnik.Lozinka + '\'' + ", TipKorisnika='" + korisnik.TipKorisnika + '\'' + ", Aktivan='" + korisnik.Aktivan + '\'' + ", IDTreninga= ";
            //foreach(Trening trening in ListaTreninga) {
            //    s += trening.ID + ", ";
            //}
            return s + '}';
        }
    }
}
