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
    public class AdresaService : IEntitet
    {
        public void IzbrisiEntitet(string id)
        {
            int.TryParse(id, out int sifraAdrese);
            Adresa adresa = Util.Instance.Adrese.ToList().Find(t => t.ID == sifraAdrese);
            if (adresa == null)
            {
                throw new UserNotFoundException($"Ne postoji Adresa sa ID: {id}");
            }

            adresa.Aktivna = false;
            Util.Instance.SacuvajEntitet("adrese.txt");
        }
        public void UcitajEntitet(string filename)
        {
            Util.Instance.Adrese = new ObservableCollection<Adresa>();

            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] adresaIzFajla = line.Split('|');
                    int.TryParse(adresaIzFajla[0], out int sifraAdrese);
                    Boolean.TryParse(adresaIzFajla[5], out Boolean status);
                    Adresa adresa = new Adresa
                    {

                        ID = sifraAdrese,
                        Ulica = adresaIzFajla[1],
                        Broj = adresaIzFajla[2],
                        Grad = adresaIzFajla[3],
                        Drzava = adresaIzFajla[4],
                        Aktivna = status
                    };

                    Util.Instance.Adrese.Add(adresa);
                }
            }
        }
        public void SacuvajEntitet(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Adresa adresa in Util.Instance.Adrese)
                {
                    file.WriteLine(adresa.AdresaZaUpisUFajl());
                }

            }
        }
    }
}
