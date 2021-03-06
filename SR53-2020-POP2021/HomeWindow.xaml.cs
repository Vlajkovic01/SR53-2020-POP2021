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
                MIKorisnici.Visibility = Visibility.Collapsed;
                MIAdrese.Visibility = Visibility.Collapsed;
                MITreninzi.Visibility = Visibility.Collapsed;
                MIProfil.Visibility = Visibility.Collapsed;
                MIMojiTreninziInstruktor.Visibility = Visibility.Collapsed;
                MIPregledInstruktora.Visibility = Visibility.Collapsed;
                SeparatorProfil.Visibility = Visibility.Collapsed;

            }
            else if (korisnik.TipKorisnika.Equals(ETipKorisnika.POLAZNIK))
            {
                MIKorisnici.Visibility = Visibility.Collapsed;
                MIAdrese.Visibility = Visibility.Collapsed;
                MITreninzi.Visibility = Visibility.Collapsed;
                MIMojiTreninziInstruktor.Visibility = Visibility.Collapsed;

            }
            else if (korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR))
            {
                MIKorisnici.Visibility = Visibility.Collapsed;
                MIAdrese.Visibility = Visibility.Collapsed;
                MITreninzi.Visibility = Visibility.Collapsed;
                MIPregledInstruktora.Visibility = Visibility.Collapsed;
            }
            else if (korisnik.TipKorisnika.Equals(ETipKorisnika.ADMINISTRATOR))
            {
                MIMojiTreninziInstruktor.Visibility = Visibility.Collapsed;
                MIPregledInstruktora.Visibility = Visibility.Collapsed;
            }

        }
        private void MIKorisnici_Click(object sender, RoutedEventArgs e)
        {
            AllUsersWindow iw = new AllUsersWindow(trenutniKorisnik);

            this.Hide();
            iw.Show();
        }

        private void MIAdrese_Click(object sender, RoutedEventArgs e)
        {
            AllAddressWindow aw = new AllAddressWindow(trenutniKorisnik);

            this.Hide();
            aw.Show();
        }

        private void MIFitnesCentri_Click(object sender, RoutedEventArgs e)
        {
            FitnesCenterWindow fcw = new FitnesCenterWindow(trenutniKorisnik);

            this.Hide();
            fcw.Show();
        }
        private void MIProfil_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik stariKorisnik = trenutniKorisnik.Clone();

            MyProfileWindow addEditUser = new MyProfileWindow(trenutniKorisnik, EOdabraniStatus.IZMENI);
            this.Hide();
            if (!(bool)addEditUser.ShowDialog())
            {
                int index = Util.Instance.Korisnici.ToList().FindIndex(k => k.JMBG.Equals(stariKorisnik.JMBG));
                Util.Instance.Korisnici[index] = stariKorisnik;
            }
            this.Show();

        }

        private void MITreninzi_Click(object sender, RoutedEventArgs e)
        {
            AllTrainingWindow atw = new AllTrainingWindow(trenutniKorisnik);

            this.Hide();
            atw.Show();
        }
        private void MIPregledInstruktora_Click(object sender, RoutedEventArgs e)
        {
            ReviewInstructorsWindow riw = new ReviewInstructorsWindow(trenutniKorisnik);

            this.Hide();
            riw.Show();
        }
        private void MIMojiTreninziInstruktor_Click(object sender, RoutedEventArgs e)
        {
            InstructorsTrainingWindow itw = new InstructorsTrainingWindow(trenutniKorisnik);

            this.Hide();
            itw.Show();
        }

        private void BtnOdjava_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();

            this.Hide();
            lw.Show();
        }
    }
}
