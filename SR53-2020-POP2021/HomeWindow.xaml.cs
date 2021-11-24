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

namespace SR53_2020_POP2021
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private Centar centar;
        private RegistrovaniKorisnik korisnik;
        private ETipKorisnika tipKorisnika;
        public HomeWindow(Centar centar ,RegistrovaniKorisnik korisnik)
        {
            InitializeComponent();

            this.centar = centar;
            this.korisnik = korisnik;

            if(korisnik is Administrator)
            {
                tipKorisnika = ETipKorisnika.ADMINISTRATOR;
            } else if(korisnik is Instruktor)
            {
                tipKorisnika = ETipKorisnika.INSTRUKTOR;
            } else
            {
                tipKorisnika = ETipKorisnika.POLAZNIK;
            }
            if(korisnik != null)
            {
                this.Title = "Prijavljeni ste kao " + tipKorisnika + " - " + korisnik.Ime;
            } else
            {
                this.Title = "Prijavljeni ste kao gost";
            }

            initGUI();

        }

        public void initGUI()
        {
            if(korisnik == null)
            {
                MIAdministratori.Visibility = Visibility.Collapsed;
                MIInstruktori.Visibility = Visibility.Collapsed;
                MIPolaznici.Visibility = Visibility.Collapsed;
                MIAdrese.Visibility = Visibility.Collapsed;
                MITreninzi.Visibility = Visibility.Collapsed;
                
            } 
            else if(korisnik.TipKorisnika.Equals(ETipKorisnika.POLAZNIK))
            {
                MIAdministratori.Visibility = Visibility.Collapsed;
                MIPolaznici.Visibility = Visibility.Collapsed;
                MIAdrese.Visibility = Visibility.Collapsed;
                MITreninzi.Visibility = Visibility.Collapsed;

            }
            else if(korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR))
            {
                MIAdministratori.Visibility = Visibility.Collapsed;
                MIInstruktori.Visibility = Visibility.Collapsed;
                MIPolaznici.Visibility = Visibility.Collapsed;
                MIAdrese.Visibility = Visibility.Collapsed;
            }
        }

        private void MIAdministratori_Click(object sender, RoutedEventArgs e)
        {
            //AllDoctors doctorWindow = new AllDoctors(CurrentUser);

            //this.Hide();
            //doctorWindow.Show();
        }
        private void MIInstruktori_Click(object sender, RoutedEventArgs e)
        {
            //AllPatients patinetWindow = new AllPatients(CurrentUser);

            //this.Hide();
            //patinetWindow.Show();


        }
        private void MIPolaznici_Click(object sender, RoutedEventArgs e)
        {
            //AllAdress adressWindow = new AllAdress(CurrentUser);

            //this.Hide();
            //adressWindow.Show();
        }

        private void MIAdrese_Click(object sender, RoutedEventArgs e)
        {
            //AllHospitals hospitalWindow = new AllHospitals(CurrentUser);

            //this.Hide();
            //hospitalWindow.Show();
        }

        private void MIFitnesCentri_Click(object sender, RoutedEventArgs e)
        {
            //AllAppointments appointmentWindow = new AllAppointments(CurrentUser);

            //this.Hide();
            //appointmentWindow.Show();
        }

        private void MITreninzi_Click(object sender, RoutedEventArgs e)
        {
            //AllTherapy therapyWindow = new AllTherapy(CurrentUser);

            //this.Hide();
            //therapyWindow.Show();
        }

        private void BtnOdjava_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();

            this.Hide();
            lw.Show();
        }
    }
}
