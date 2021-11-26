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
    public class PolaznikService : IEntitet
    {
        public void IzbrisiEntitet(string jmbg)
        {
            Polaznik polaznik = Util.Instance.Polaznici.ToList().Find(p => p.Korisnik.JMBG.Equals(jmbg));
            if (polaznik == null)
            {
                throw new UserNotFoundException($"Ne postoji korisnik sa JMBG: {jmbg}");
            }

            polaznik.Korisnik.Aktivan = false;
            Util.Instance.SacuvajEntitet("polaznici.txt");
        }

        public void UcitajEntitet(string filename)
        {
            Util.Instance.Polaznici = new ObservableCollection<Polaznik>();
            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] polaznikIzFajla = line.Split('|');
                    RegistrovaniKorisnik registrovaniKorisnik = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.JMBG.Equals(polaznikIzFajla[2]));

                    Polaznik polaznik = new Polaznik
                    {

                        Korisnik = registrovaniKorisnik,
                    };

                    Util.Instance.Polaznici.Add(polaznik);
                }
            }
        }
        public void SacuvajEntitet(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Polaznik polaznik in Util.Instance.Polaznici)
                {
                    file.WriteLine(polaznik.PolaznikZaUpisUFajl());
                }
            }
        }
    }
}
