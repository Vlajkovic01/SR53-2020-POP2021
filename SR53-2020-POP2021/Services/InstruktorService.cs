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
   public class InstruktorService : IEntitet
    {

        public void IzbrisiEntitet(string jmbg)
        {
            Instruktor instruktor = Util.Instance.Instruktori.ToList().Find(i => i.Korisnik.JMBG.Equals(jmbg));
            if (instruktor == null)
            {
                throw new UserNotFoundException($"Ne postoji korisnik sa JMBG: {jmbg}");
            }

            instruktor.Korisnik.Aktivan = false;
            Util.Instance.SacuvajEntitet("instruktori.txt");
        }

        public void UcitajEntitet(string filename)
        {
            Util.Instance.Instruktori = new ObservableCollection<Instruktor>();
            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] instruktorIzFajla = line.Split('|');
                    RegistrovaniKorisnik registrovaniKorisnik = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.JMBG.Equals(instruktorIzFajla[2]));

                    Instruktor instruktor = new Instruktor
                    {

                        Korisnik = registrovaniKorisnik,
                    };

                    Util.Instance.Instruktori.Add(instruktor);
                }
            }
        }

        public void SacuvajEntitet(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Instruktor instruktor in Util.Instance.Instruktori)
                {
                    file.WriteLine(instruktor.InstruktorZaUpisUFajl());
                }
            }
        }
    }
}
