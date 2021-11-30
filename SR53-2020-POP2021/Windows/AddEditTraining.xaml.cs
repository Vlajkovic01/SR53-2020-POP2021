using SR53_2020_POP2021.model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddEditTraining.xaml
    /// </summary>
    public partial class AddEditTraining : Window
    {
        private EOdabraniStatus izabraniStatus;
        private Trening izabranTrening;
        RegistrovaniKorisnik trenutniKorisnik;
        public AddEditTraining(RegistrovaniKorisnik korisnik,Trening trening, EOdabraniStatus status = EOdabraniStatus.DODAJ)
        {
            InitializeComponent();
            this.DataContext = trening;

            izabraniStatus = status;
            izabranTrening = trening;
            trenutniKorisnik = korisnik;

            TxtID.IsEnabled = false;

            CBStatus.ItemsSource = Enum.GetValues(typeof(EStatusTreninga));

            
            foreach(Instruktor instruktor in Util.Instance.Instruktori)
            {
                if(trenutniKorisnik.TipKorisnika.Equals(ETipKorisnika.ADMINISTRATOR))
                {
                    if (instruktor.Korisnik.Aktivan)
                    {
                        CBInstruktor.Items.Add(instruktor);
                    }
                } else if(trenutniKorisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR))
                {
                    if (instruktor.Korisnik.Aktivan && instruktor.Korisnik.JMBG.Equals(trenutniKorisnik.JMBG))
                    {
                        CBInstruktor.Items.Add(instruktor);
                    }
                }

            }

            if (status.Equals(EOdabraniStatus.IZMENI) && trening != null)
            {
                this.Title = "Izmena treninga";
            }
            else
            {
                this.Title = "Dodavanje treninga";
            }
        }

        private void BtnOtkazi_Click(object sender, RoutedEventArgs e)
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
                    izabranTrening.Aktivan = true;
                    izabranTrening.Polaznik = Util.Instance.Polaznici.ToList().Find(k => k.Korisnik.JMBG.Equals("0000000000000"));
                    Util.Instance.Treninzi.Add(izabranTrening);
                }
                Util.Instance.SacuvajEntitet("treninzi.txt");
                this.DialogResult = true;
                this.Close();
            }
        }

        private bool Validacija()
        {
            string poruka = "Molimo popravite sledece greske u unosu: " + "\n";
            bool ispravno = true;
            if (TxtDatum.Text.Equals("") || !TxtDatum.Text.Contains("."))
            {
                poruka += "- Niste pravilno uneli datum" + "\n";
                ispravno = false;
            }
            if (TxtVreme.Text.Equals("") || !TxtVreme.Text.Contains(":"))
            {
                poruka += "- Niste pravilno uneli vreme" + "\n";
                ispravno = false;
            }
            try
            {
                int.Parse(TxtTrajanje.Text);
            } catch (FormatException)
            {
                poruka += "- Trajanje mora biti broj" + "\n";
                ispravno = false;
            }
            if (CBStatus.SelectedItem == null)
            {
                poruka += "- Niste odabrali status" + "\n";
                ispravno = false;
            }
            if (CBInstruktor.SelectedItem == null)
            {
                poruka += "- Niste odabrali instruktora" + "\n";
                ispravno = false;
            }
            if (CBInstruktor.SelectedItem == null)
            {
                poruka += "- Niste odabrali instruktora" + "\n";
                ispravno = false;
            }
            if (ispravno == false)
            {
                MessageBox.Show(poruka, "Greska");
            }
            return ispravno;
        }
    }
}
