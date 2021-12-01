using SR53_2020_POP2021.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SR53_2020_POP2021.Windows
{
    /// <summary>
    /// Interaction logic for AddEditUserWindow.xaml
    /// </summary>
    public partial class AddEditUserWindow : Window
    {
        private EOdabraniStatus izabraniStatus;
        private RegistrovaniKorisnik izabraniKorisnik;
        public AddEditUserWindow(RegistrovaniKorisnik korisnik, EOdabraniStatus status = EOdabraniStatus.DODAJ)
        {
            InitializeComponent();

            this.DataContext = korisnik;

            CBPol.ItemsSource = Enum.GetValues(typeof(EPol));
            CBTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika));

            if(status.Equals(EOdabraniStatus.IZMENI))
            {
                TxtJMBG.IsEnabled = false;
            }

            izabraniStatus = status;
            izabraniKorisnik = korisnik;

            foreach (Adresa adresa in Util.Instance.Adrese)
            {
                if (adresa.Aktivna)
                {
                    CBAdresa.Items.Add(adresa);
                }   
            }
           
            if(status.Equals(EOdabraniStatus.IZMENI) && korisnik != null)
            {
                this.Title = "Izmena korisnika";
            } else
            {
                this.Title = "Dodavanje korisnika";
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            
            if (Validacija())
            {
                if (izabraniStatus.Equals(EOdabraniStatus.DODAJ))
                {
                    
                    if (izabraniKorisnik.TipKorisnika.Equals(ETipKorisnika.POLAZNIK))
                    {
                        izabraniKorisnik.Aktivan = true;
                        Polaznik noviPolaznik = new Polaznik
                        {   
                            Korisnik = izabraniKorisnik,
                            ListaRezervisanihTreninga = new ObservableCollection<Trening>()
                        };
                        Util.Instance.Korisnici.Add(izabraniKorisnik);
                        Util.Instance.Polaznici.Add(noviPolaznik);
                    }
                    else if (izabraniKorisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR))
                    {
                        izabraniKorisnik.Aktivan = true;
                        Instruktor noviInstruktor = new Instruktor
                        {   
                            Korisnik = izabraniKorisnik,
                            ListaTreninga = new ObservableCollection<Trening>()
                        };
                        Util.Instance.Korisnici.Add(izabraniKorisnik);
                        Util.Instance.Instruktori.Add(noviInstruktor);
                    } else
                    {
                        izabraniKorisnik.Aktivan = true;
                        Util.Instance.Korisnici.Add(izabraniKorisnik);
                    }
                }
                Util.Instance.SacuvajEntitet("instruktori.txt");
                Util.Instance.SacuvajEntitet("polaznici.txt");
                Util.Instance.SacuvajEntitet("korisnici.txt");

                this.DialogResult = true;
                this.Close();
            }
        }

        private bool Validacija()
        {
            string poruka = "Molimo popravite sledece greske u unosu: " + "\n";
            bool ispravno = true;
            if (TxtIme.Text.Equals(""))
            {
                poruka += "- Niste uneli Ime" + "\n";
                ispravno = false;
            }
            if (TxtPrezime.Text.Equals(""))
            {
                poruka += "- Niste uneli Prezime" + "\n";
                ispravno = false;
            }
            if (TxtJMBG.Text.Equals("") || TxtJMBG.Text.Length != 13)
            {
                poruka += "- Niste pravilno uneli JMBG" + "\n";
                ispravno = false;
            }
            if(izabraniStatus.Equals(EOdabraniStatus.DODAJ))
            {
                string maticniBroj = TxtJMBG.Text;
                RegistrovaniKorisnik pronadjen = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.JMBG.Equals(maticniBroj));
                if (pronadjen != null)
                {
                    poruka += "- Postoji korisnik sa tim JMBG" + "\n";
                    ispravno = false;
                }
            }
            if (CBPol.SelectedItem == null)
            {
                poruka += "- Niste odabrali Pol korisnika" + "\n";
                ispravno = false;
            }
            if (CBAdresa.SelectedItem == null)
            {
                poruka += "- Niste uneli Adresu" + "\n";
                ispravno = false;
            }
            if (TxtEmail.Text.Equals("") || !TxtEmail.Text.Contains("@") || !TxtEmail.Text.EndsWith(".com"))
            {
                poruka += "- Niste pravilno uneli Email" + "\n";
                ispravno = false;
            }
            if (TxtLozinka.Text.Equals(""))
            {
                poruka += "- Niste uneli Sifru" + "\n";
                ispravno = false;
            }
            if (CBTipKorisnika.SelectedItem == null)
            {
                poruka += "- Niste odabrali Tip korisnika" + "\n";
                ispravno = false;
            }
            if(ispravno == false)
            {
                MessageBox.Show(poruka, "Greska");
            }
            return ispravno;
        }

        private void BtnNovaAdresa_Click(object sender, RoutedEventArgs e)
        {
            Adresa novaAdresa = new Adresa();
            novaAdresa.ID = Util.Instance.GenerisanjeIDAdrese();
            AddEditUsersAddressWindow aeua = new AddEditUsersAddressWindow(novaAdresa);
            if (!(bool)aeua.ShowDialog())
            {

            }
            if (novaAdresa.Aktivna != false)
            {
                CBAdresa.Items.Add(novaAdresa);
            }
        }
    }
}

