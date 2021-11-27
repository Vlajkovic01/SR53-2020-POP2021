using SR53_2020_POP2021.Izuzeci;
using SR53_2020_POP2021.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.Services
{
    public class RegistrovaniKorisnikService : IEntitet
    {
        public void IzbrisiEntitet(string jmbg)
        {
            RegistrovaniKorisnik registrovaniKorisnik = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.JMBG.Equals(jmbg));
            if(registrovaniKorisnik == null) {
                throw new UserNotFoundException($"Ne postoji korisnik sa JMBG: {jmbg}");
            }

            registrovaniKorisnik.Aktivan = false;
            Util.Instance.SacuvajEntitet("korisnici.txt");
 
        }

        public void UcitajEntitet(string filename)
        {
            Util.Instance.Korisnici = new ObservableCollection<RegistrovaniKorisnik>();

            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] korisnikIzFajla = line.Split('|');

                    Enum.TryParse(korisnikIzFajla[3], out EPol pol);
                    int.TryParse(korisnikIzFajla[4], out int sifraAdrese);
                    Enum.TryParse(korisnikIzFajla[7], out ETipKorisnika tip);
                    Boolean.TryParse(korisnikIzFajla[8], out Boolean status);
                    RegistrovaniKorisnik registrovaniKorisnik = new RegistrovaniKorisnik
                    {

                        Ime = korisnikIzFajla[0],
                        Prezime = korisnikIzFajla[1],
                        JMBG = korisnikIzFajla[2],
                        Adresa = Util.Instance.PronadjiAdresu(sifraAdrese),
                        Email = korisnikIzFajla[5],
                        Lozinka = korisnikIzFajla[6],
                        TipKorisnika = tip,
                        Aktivan = status
                    };

                    Util.Instance.Korisnici.Add(registrovaniKorisnik);
                }
            }
        }

        public void SacuvajEntitet(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (RegistrovaniKorisnik registrovaniKorisnik in Util.Instance.Korisnici)
                {
                    file.WriteLine(registrovaniKorisnik.KorisnikZaUpisUFajl());
                }
            }
        }
    }
}
