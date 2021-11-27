﻿using SR53_2020_POP2021.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.model
{
    public sealed class Util
    {
        private static readonly Util instance = new Util();
        private IEntitet regKorisnikService;
        private IEntitet instruktorService;
        private IEntitet polaznikService;
        private IEntitet treningService;
        private IEntitet adresaService;
        private IEntitet centarService;

        public ObservableCollection<RegistrovaniKorisnik> Korisnici { get; set; }
        public ObservableCollection<Instruktor> Instruktori { get; set; }
        public ObservableCollection<Polaznik> Polaznici { get; set; }
        public ObservableCollection<Trening> Treninzi { get; set; }
        public ObservableCollection<Adresa> Adrese { get; set; }
        public ObservableCollection<Centar> Centri { get; set; }

        private Util()
        {
            regKorisnikService = new RegistrovaniKorisnikService();
            instruktorService = new InstruktorService();
            polaznikService = new PolaznikService();
            treningService = new TreningService();
            adresaService = new AdresaService();
            centarService = new CentarService();
        }
        static Util()
        {

        }
        public static Util Instance
        {
            get
            {
                return instance;
            }
        }

        public void Initialize()
        {
            Adrese = new ObservableCollection<Adresa>();
            Korisnici = new ObservableCollection<RegistrovaniKorisnik>();
            Instruktori = new ObservableCollection<Instruktor>();
            Polaznici = new ObservableCollection<Polaznik>();
            Treninzi = new ObservableCollection<Trening>();
            Centri = new ObservableCollection<Centar>();
        }

        public void SacuvajEntitet(string filename)
        {
            if (filename.Contains("korisnici"))
            {
                regKorisnikService.SacuvajEntitet(filename);
            }
            else if (filename.Contains("instruktori"))
            {
                instruktorService.SacuvajEntitet(filename);
            }
            else if (filename.Contains("polaznici"))
            {
                polaznikService.SacuvajEntitet(filename);
            }
            else if (filename.Contains("adrese"))
            {
                adresaService.SacuvajEntitet(filename);
            }
            else if (filename.Contains("treninzi"))
            {
                treningService.SacuvajEntitet(filename);
            }
            else if (filename.Contains("centri"))
            {
                centarService.SacuvajEntitet(filename);
            }
        }

        public void CitanjeEntiteta(string filename)
        {
            if (filename.Contains("korisnici"))
            {
                regKorisnikService.UcitajEntitet(filename);
            }
            else if (filename.Contains("instruktori"))
            {
                instruktorService.UcitajEntitet(filename);
            }
            else if (filename.Contains("polaznici"))
            {
                polaznikService.UcitajEntitet(filename);
            }
            else if (filename.Contains("adrese"))
            {
                adresaService.UcitajEntitet(filename);
            }
            else if (filename.Contains("treninzi"))
            {
                treningService.UcitajEntitet(filename);
            }
            else if (filename.Contains("centri"))
            {
                centarService.SacuvajEntitet(filename);
            }
        }

        public void BrisanjeEntiteta(string filename)
        {
            if (filename.Contains("korisnici"))
            {
                regKorisnikService.IzbrisiEntitet(filename);
            }
            else if (filename.Contains("instruktori"))
            {
                instruktorService.IzbrisiEntitet(filename);
            }
            else if (filename.Contains("polaznici"))
            {
                polaznikService.IzbrisiEntitet(filename);
            }
            else if (filename.Contains("adrese"))
            {
                adresaService.IzbrisiEntitet(filename);
            }
            else if (filename.Contains("treninzi"))
            {
                treningService.IzbrisiEntitet(filename);
            }
            else if (filename.Contains("centri"))
            {
                centarService.SacuvajEntitet(filename);
            }
        }
        public RegistrovaniKorisnik Login(string jmbg, string pass)
        {
            foreach (RegistrovaniKorisnik korisnik in Korisnici)
            {
                if (korisnik.JMBG.Equals(jmbg) && korisnik.Lozinka.Equals(pass))
                {
                    return korisnik;
                }
            }
            return null;
        }

        public Adresa PronadjiAdresu(int sifra)
        {
            foreach (Adresa adresa in Adrese)
            {
                if (adresa.ID == sifra)
                {
                    return adresa;
                }
            }
            return null;
        }

        public Instruktor PronadjiInstruktora(string jmbg)
        {
            foreach (Instruktor instruktor in Instruktori)
            {
                if (instruktor.Korisnik.JMBG.Contains(jmbg))
                {
                    return instruktor;
                }
            }
            return null;
        }
        public Polaznik PronadjiPolaznika(string jmbg)
        {
            foreach (Polaznik polaznik in Polaznici)
            {
                if (polaznik.Korisnik.JMBG.Contains(jmbg))
                {
                    return polaznik;
                }
            }
            return null;
        }
        public void dodajKorisnika(RegistrovaniKorisnik k)
        {
            bool postojiKorisnik = false;

            foreach (RegistrovaniKorisnik korisnik in Korisnici)
            {
                if (korisnik.JMBG.Contains(k.JMBG))
                {
                    postojiKorisnik = true;
                }
            }
            if (postojiKorisnik)
            {
                Console.WriteLine("Postoji korisnik sa tim JMBG.");
            }
            else
            {
                Util.Instance.Korisnici.Add(k);
                Console.WriteLine("Korisnik uspesno dodat.");
            }
        }
        public void dodajTrening(Trening t)
        {
            bool postojiTrening = false;

            foreach (Trening trening in Treninzi)
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
                Util.Instance.Treninzi.Add(t);
                Console.WriteLine("Trening uspesno dodat.");
            }
        }
        public void dodajAdresu(Adresa a)
        {
            bool postojiAdresa = false;

            foreach (Adresa adresa in Adrese)
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
                Util.Instance.Adrese.Add(a);
                Console.WriteLine("Adresa uspesno dodata.");
            }
        }
    }
}