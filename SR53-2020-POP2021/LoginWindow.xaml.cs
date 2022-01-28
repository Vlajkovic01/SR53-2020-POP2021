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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();
            Util.Instance.Initialize();
            Util.Instance.CitanjeEntiteta("adrese.txt");
            Util.Instance.CitanjeEntiteta("korisnici.txt");
            Util.Instance.CitanjeEntiteta("instruktori.txt");
            Util.Instance.CitanjeEntiteta("polaznici.txt");
            Util.Instance.CitanjeEntiteta("korisnici.txt");
            Util.Instance.CitanjeEntiteta("centri.txt");
            Util.Instance.CitanjeEntiteta("treninzi.txt");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string jmbg = TxtJMBG.Text;
            string password = PBPassword.Password;
            
            if(jmbg.Equals("") || password.Equals("")) {
                MessageBox.Show("Niste uneli sve podatke za prijavu!");
            } else
            {
                RegistrovaniKorisnik prijavljenKorisnik = Util.Instance.Login(jmbg, password);
                if (prijavljenKorisnik == null)
                {
                    MessageBox.Show("Pogresni podaci prijave!");
                }
                else if(prijavljenKorisnik.Aktivan == false)
                {
                    MessageBox.Show("Niste vise deo teretane, Vas nalog je izbrisan!");
                }
                else if(prijavljenKorisnik != null && prijavljenKorisnik.Aktivan == true)
                {
                    HomeWindow hw = new HomeWindow(prijavljenKorisnik);
                    this.Close();
                    hw.Show();
                }
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FitnesCenterWindow centar = new FitnesCenterWindow(null);
            this.Close();
            centar.Show();
        }

        private void ButtonRegistracija_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik noviKorisnik = new RegistrovaniKorisnik();
            AddEditUserWindow aeu = new AddEditUserWindow(noviKorisnik);
            aeu.CBTipKorisnika.IsEnabled = false;
            if (!(bool)aeu.ShowDialog())
            {

            }
            this.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
        }
    }
}
