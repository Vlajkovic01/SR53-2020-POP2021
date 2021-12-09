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
    /// Interaction logic for MyProfileWindow.xaml
    /// </summary>
    public partial class MyProfileWindow : Window
    {
        private EOdabraniStatus izabraniStatus;
        private RegistrovaniKorisnik izabraniKorisnik;
        public MyProfileWindow(RegistrovaniKorisnik korisnik, EOdabraniStatus status = EOdabraniStatus.IZMENI)
        {
            InitializeComponent();
            this.DataContext = korisnik;

            CBPol.ItemsSource = Enum.GetValues(typeof(EPol));
            CBTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika));

            TxtJMBG.IsEnabled = false;
            CBTipKorisnika.IsEnabled = false;

            izabraniStatus = status;
            izabraniKorisnik = korisnik;


            foreach (Adresa adresa in Util.Instance.Adrese)
            {
                if (adresa.Aktivna)
                {
                    CBAdresa.Items.Add(adresa);
                }
            }

            this.Title = "Izmena licnih podataka";
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (Validacija())
            {

                Util.Instance.IzmeniEntitet(izabraniKorisnik);

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
            if (TxtEmail.Text.Equals("") || !TxtEmail.Text.Contains("@gmail.com"))
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
            if (ispravno == false)
            {
                MessageBox.Show(poruka, "Greska");
            }
            return ispravno;
        }
    }
}
