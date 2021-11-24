using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    public class Centar
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string nazivCentra;

        public string NazivCentra
        {
            get { return nazivCentra; }
            set { nazivCentra = value; }
        }

        private Adresa adresaCentra;

        public Adresa AdresaCentra
        {
            get { return adresaCentra; }
            set { adresaCentra = value; }
        }

        private List<Administrator> administratori;

        public List<Administrator> Administratori
        {
            get { return administratori; }
            set { administratori = value; }
        }

        private List<Instruktor> instruktori;

        public List<Instruktor> Instruktori
        {
            get { return instruktori; }
            set { instruktori = value; }
        }

        private List<Polaznik> polaznici;

        public List<Polaznik> Polaznici
        {
            get { return polaznici; }
            set { polaznici = value; }
        }

        private List<Trening> treninzi;

        public List<Trening> Treninzi
        {
            get { return treninzi; }
            set { treninzi = value; }
        }

        private List<Adresa> listaAdresa;

        public List<Adresa> ListaAdresa
        {
            get { return listaAdresa; }
            set { listaAdresa = value; }
        }


        public Centar()
        {
            this.ID = 0;
            this.NazivCentra = "";
            this.AdresaCentra = new Adresa();
            this.Administratori = new List<Administrator>();
            this.Instruktori = new List<Instruktor>();
            this.Polaznici = new List<Polaznik>();
            this.Treninzi = new List<Trening>();
            this.ListaAdresa = new List<Adresa>();
        }

        public Centar(int id, string naziv, Adresa adresa)
        {
            ID = id;
            NazivCentra = naziv;
            AdresaCentra = adresa;
        }

        public override string ToString()
        {
            return "Centar{" + "ID='" + ID + '\'' + ", Naziv='" + NazivCentra + '\'' + ", Adresa='" + AdresaCentra.ToString() + '\'' + '}';
        }

        //=========================================ADMINISTRATORI===========================================
        
        public void sacuvajAdministratore()
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/administratori.txt"))
            {
                foreach (Administrator admin in administratori)
                {
                    file.WriteLine(admin.AdminZaUpisUFajl());
                }
                
            }
        }

        public void ucitajAdministratore()
        {
            using (StreamReader file = new StreamReader(@"../../Resources/administratori.txt"))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] split = line.Split('|');
                    String ime = split[0];
                    String prezime = split[1];
                    String jmbg = split[2];
                    Enum.TryParse(split[3], out EPol pol);
                    int.TryParse(split[4], out int sifraAdrese); 
                    Adresa adresa = PronadjiAdresu(sifraAdrese);
                    String email = split[5];
                    String lozinka = split[6];
                    Enum.TryParse(split[7], out ETipKorisnika tipKorisnika);
                    Administrator admin = new Administrator(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika);
                    administratori.Add(admin);

                }
            }
        }

        public void dodajAdministratora(Administrator administrator)
        {
            bool postojiAdmin = false;

            foreach(Administrator admin in administratori)
            {
                if(admin.JMBG.Contains(administrator.JMBG))
                {
                    postojiAdmin = true;
                }
            }
            if(postojiAdmin)
            {
                Console.WriteLine("Postoji administrator sa tim JMBG.");
            } else
            {
                this.administratori.Add(administrator);
                Console.WriteLine("Administrator uspesno dodat.");
            }
        }

        //==================================ADRESA==========================================

        public void ucitajAdrese()
        {
            using (StreamReader file = new StreamReader(@"../../Resources/adrese.txt"))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] split = line.Split('|');
                    int.TryParse(split[0], out int sifraAdrese);
                    String ulica = split[1];
                    String broj = split[2];
                    String grad = split[3];
                    String drzava = split[4];
                    Adresa adresa = new Adresa(sifraAdrese, ulica, broj, grad, drzava);
                    ListaAdresa.Add(adresa);

                }
            }
        }

        public void sacuvajAdrese()
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/adrese.txt"))
            {
                foreach (Adresa adresa in ListaAdresa)
                {
                    file.WriteLine(adresa.AdresaZaUpisUFajl());
                }

            }
        }

        public Adresa PronadjiAdresu(int sifra)
        {
            foreach(Adresa adresa in ListaAdresa)
            {
                if(adresa.ID == sifra)
                {
                    return adresa;
                }
            }
            return null;
        }

        public void dodajAdresu(Adresa a)
        {
            bool postojiAdresa = false;

            foreach (Adresa adresa in ListaAdresa)
            {
                if (adresa.ID == a.ID)
                {
                    postojiAdresa = true;
                }
            }
            if (postojiAdresa)
            {
                Console.WriteLine("Postoji adresa sa tim ID-jem.");
            }
            else
            {
                this.ListaAdresa.Add(a);
                Console.WriteLine("Adresa uspesno dodata.");
            }
        }

        //==================================INSTRUKTORI================================

        public void sacuvajInstruktore()
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/instruktori.txt"))
            {
                foreach (Instruktor instruktor in instruktori)
                {
                    file.WriteLine(instruktor.InstruktorZaUpisUFajl());
                }

            }
        }

        public void ucitajInstruktore()
        {
            using (StreamReader file = new StreamReader(@"../../Resources/instruktori.txt"))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] split = line.Split('|');
                    String ime = split[0];
                    String prezime = split[1];
                    String jmbg = split[2];
                    Enum.TryParse(split[3], out EPol pol);
                    int.TryParse(split[4], out int sifraAdrese);
                    Adresa adresa = PronadjiAdresu(sifraAdrese);
                    String email = split[5];
                    String lozinka = split[6];
                    Enum.TryParse(split[7], out ETipKorisnika tipKorisnika);
                    List<Trening> treninzi = new List<Trening>();
                    Instruktor instruktor = new Instruktor(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika, treninzi);
                    instruktori.Add(instruktor);

                }
            }
        }

        public void dodajInstruktora(Instruktor i)
        {
            bool postojiInstruktor = false;

            foreach (Instruktor instruktor in instruktori)
            {
                if (instruktor.JMBG.Contains(i.JMBG))
                {
                    postojiInstruktor = true;
                }
            }
            if (postojiInstruktor)
            {
                Console.WriteLine("Postoji instruktor sa tim JMBG.");
            }
            else
            {
                this.instruktori.Add(i);
                Console.WriteLine("Instruktor uspesno dodat.");
            }
        }

        public Instruktor PronadjiInstruktora(string jmbg)
        {
            foreach (Instruktor instruktor in instruktori)
            {
                if (instruktor.JMBG.Contains(jmbg))
                {
                    return instruktor;
                }
            }
            return null;
        }

        //===============================POLAZNIK===================================

        public void sacuvajPolaznike()
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/polaznici.txt"))
            {
                foreach (Polaznik polaznik in polaznici)
                {
                    file.WriteLine(polaznik.PolaznikZaUpisUFajl());
                }

            }
        }

        public void ucitajPolaznike()
        {
            using (StreamReader file = new StreamReader(@"../../Resources/polaznici.txt"))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] split = line.Split('|');
                    String ime = split[0];
                    String prezime = split[1];
                    String jmbg = split[2];
                    Enum.TryParse(split[3], out EPol pol);
                    int.TryParse(split[4], out int sifraAdrese);
                    Adresa adresa = PronadjiAdresu(sifraAdrese);
                    String email = split[5];
                    String lozinka = split[6];
                    Enum.TryParse(split[7], out ETipKorisnika tipKorisnika);
                    List<Trening> treninzi = new List<Trening>();
                    Polaznik polaznik = new Polaznik(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika, treninzi);
                    polaznici.Add(polaznik);

                }
            }
        }

        public void dodajPolaznika(Polaznik p)
        {
            bool postojiPolaznik = false;

            foreach (Polaznik polaznik in polaznici)
            {
                if (polaznik.JMBG.Contains(p.JMBG))
                {
                    postojiPolaznik = true;
                }
            }
            if (postojiPolaznik)
            {
                Console.WriteLine("Postoji polaznik sa tim JMBG.");
            }
            else
            {
                this.polaznici.Add(p);
                Console.WriteLine("Polaznik uspesno dodat.");
            }
        }

        public Polaznik PronadjiPolaznika(string jmbg)
        {
            foreach (Polaznik polaznik in polaznici)
            {
                if (polaznik.JMBG.Contains(jmbg))
                {
                    return polaznik;
                }
            }
            return null;
        }

        //====================================TRENING========================================

        public void ucitajTreninge()
        {
            using (StreamReader file = new StreamReader(@"../../Resources/treninzi.txt"))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] split = line.Split('|');
                    int.TryParse(split[0], out int sifraTreninga);
                    String datumTreninga = split[1];
                    String pocetakTreninga = split[2];
                    int.TryParse(split[3], out int trajanjeTreninga);
                    Enum.TryParse(split[4], out EStatusTreninga statusTreninga);
                    String jmbgInstruktora = split[5];
                    Instruktor instruktor = PronadjiInstruktora(jmbgInstruktora);
                    String jmbgPolaznika = split[6];
                    Polaznik polaznik = PronadjiPolaznika(jmbgPolaznika);
                    Trening trening = new Trening(sifraTreninga,datumTreninga, pocetakTreninga, trajanjeTreninga, statusTreninga, instruktor, polaznik);
                    if(instruktor != null)
                    {
                        instruktor.ListaTreninga.Add(trening);   //obratiti paznju kasnije na ovo
                    }
                    if(polaznik != null)
                    {
                        polaznik.ListaRezervisanihTreninga.Add(trening);
                    }
                    treninzi.Add(trening);
                }
            }
        }

        public void sacuvajTreninge()
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/treninzi.txt"))
            {
                foreach (Trening trening in Treninzi)
                {
                    file.WriteLine(trening.TreningZaUpisUFajl());
                }

            }
        }

        public void dodajTrening(Trening t)
        {
            bool postojiTrening = false;

            foreach (Trening trening in treninzi)
            {
                if (trening.ID == t.ID)
                {
                    postojiTrening = true;
                }
            }
            if (postojiTrening)
            {
                Console.WriteLine("Postoji trening sa tim ID-jem.");
            }
            else
            {
                this.treninzi.Add(t);
                Console.WriteLine("Trening uspesno dodat.");
            }
        }

        //===============================LOGIN================================

        public Administrator loginAdmin(string jmbg, string pass)
        {
            foreach(Administrator admin in administratori)
            {
                if(admin.JMBG.Equals(jmbg) && admin.Lozinka.Equals(pass))
                {
                    return admin;
                }
            }
            return null;
        }

        public Instruktor loginInstruktor(string jmbg, string pass)
        {
            foreach (Instruktor instruktor in instruktori)
            {
                if (instruktor.JMBG.Equals(jmbg) && instruktor.Lozinka.Equals(pass))
                {
                    return instruktor;
                }
            }
            return null;
        }

        public Polaznik loginPolaznik(string jmbg, string pass)
        {
            foreach (Polaznik polaznik in polaznici)
            {
                if (polaznik.JMBG.Equals(jmbg) && polaznik.Lozinka.Equals(pass))
                {
                    return polaznik;
                }
            }
            return null;
        }
    }
}
