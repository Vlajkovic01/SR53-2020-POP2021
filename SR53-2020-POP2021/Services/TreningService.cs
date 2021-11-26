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
    public class TreningService : IEntitet
    {
        public void IzbrisiEntitet(string id)
        {
            int.TryParse(id, out int sifraTreninga);
            Trening trening = Util.Instance.Treninzi.ToList().Find(t => t.ID == sifraTreninga);
            if (trening == null)
            {
                throw new UserNotFoundException($"Ne postoji trening sa ID: {id}");
            }

            trening.Aktivan = false;
            Util.Instance.SacuvajEntitet("treninzi.txt");
        }
        public void UcitajEntitet(string filename)
        {
            Util.Instance.Treninzi = new ObservableCollection<Trening>();

            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] treningIzFajla = line.Split('|');
                    int.TryParse(treningIzFajla[0], out int sifraTreninga);
                    int.TryParse(treningIzFajla[3], out int trajanjeTreninga);
                    Enum.TryParse(treningIzFajla[4], out EStatusTreninga statusTreninga);
                    Boolean.TryParse(treningIzFajla[7], out Boolean status);
                    Trening trening = new Trening
                    {

                        ID = sifraTreninga,
                        DatumTreninga = treningIzFajla[1],
                        VremePocetkaTreninga = treningIzFajla[2],
                        TrajanjeTreninga = trajanjeTreninga,
                        StatusTreninga = statusTreninga,
                        Instruktor = Util.Instance.PronadjiInstruktora(treningIzFajla[5]),
                        Polaznik = Util.Instance.PronadjiPolaznika(treningIzFajla[6]),
                        Aktivan = status
                    };

                    Util.Instance.Treninzi.Add(trening);
                }
            }
        }
        public void SacuvajEntitet(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Trening trening in Util.Instance.Treninzi)
                {
                    file.WriteLine(trening.TreningZaUpisUFajl());
                }

            }
        }
    }
}
