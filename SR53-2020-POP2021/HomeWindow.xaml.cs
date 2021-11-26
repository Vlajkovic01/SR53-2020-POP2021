using SR53_2020_POP2021.model;
using SR53_2020_POP2021.Windows;
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
        RegistrovaniKorisnik trenutniKorisnik;
        public HomeWindow(RegistrovaniKorisnik korisnik = null)
        {
            InitializeComponent();

            trenutniKorisnik = korisnik;

            if(korisnik != null)
            {
                this.Title = "Prijavljeni ste kao " + korisnik.TipKorisnika + " - " + korisnik.Ime;
            } else
            {
                this.Title = "Prijavljeni ste kao gost";
            }

            if (korisnik == null)
            {
                MIAdministratori.Visibility = Visibility.Collapsed;
                MIInstruktori.Visibility = Visibility.Collapsed;
                MIPolaznici.Visibility = Visibility.Collapsed;
                MIAdrese.Visibility = Visibility.Collapsed;
                MITreninzi.Visibility = Visibility.Collapsed;

            }
            else if (korisnik.TipKorisnika.Equals(ETipKorisnika.POLAZNIK))
            {
                MIAdministratori.Visibility = Visibility.Collapsed;
                MIPolaznici.Visibility = Visibility.Collapsed;
                MIAdrese.Visibility = Visibility.Collapsed;
                MITreninzi.Visibility = Visibility.Collapsed;

            }
            else if (korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR))
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
            AllInstructorsWindow iw = new AllInstructorsWindow(trenutniKorisnik);

            this.Hide();
            iw.Show();
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
