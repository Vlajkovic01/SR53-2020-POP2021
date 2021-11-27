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
    public class CentarService : IEntitet
    {
        public void IzbrisiEntitet(string id)
        {
            int.TryParse(id, out int sifraCentra);
            Centar centar = Util.Instance.Centri.ToList().Find(c => c.ID.Equals(sifraCentra));
            if (centar == null)
            {
                throw new UserNotFoundException($"Ne postoji centar sa ID: {id}");
            }

            centar.Aktivan = false;
            Util.Instance.SacuvajEntitet("centri.txt");
        }
        public void UcitajEntitet(string filename)
        {
            Util.Instance.Centri = new ObservableCollection<Centar>();

            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] centarIzFajla = line.Split('|');
                    int.TryParse(centarIzFajla[0], out int sifraCentra);
                    int.TryParse(centarIzFajla[2], out int sifraAdrese);
                    Boolean.TryParse(centarIzFajla[3], out Boolean status);
                    Centar centar = new Centar
                    {

                        ID = sifraCentra,
                        NazivCentra = centarIzFajla[1],
                        AdresaCentra = Util.Instance.PronadjiAdresu(sifraAdrese),
                        Aktivan = status
                    };

                    Util.Instance.Centri.Add(centar);
                }
            }
        }
        public void SacuvajEntitet(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Centar centar in Util.Instance.Centri)
                {
                    file.WriteLine(centar.CentarZaUpisUFajl());
                }

            }
        }
    }
}
