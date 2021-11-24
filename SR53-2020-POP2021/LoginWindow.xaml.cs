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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private Centar centar;
        public LoginWindow()
        {
            InitializeComponent();

            this.centar = new Centar();

            centar.ucitajAdrese();
            centar.ucitajAdministratore();
            centar.ucitajInstruktore();
            centar.ucitajPolaznike();
            centar.ucitajTreninge();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string jmbg = TxtJMBG.Text;
            string password = PBPassword.Password;
            
            if(jmbg.Equals("") || password.Equals("")) {
                MessageBox.Show("Niste uneli sve podatke za prijavu!");
            } else
            {
                Administrator prijavljenAdmin = centar.loginAdmin(jmbg, password);
                Instruktor prijavljenInstruktor = centar.loginInstruktor(jmbg, password);
                Polaznik prijavljenPolaznik = centar.loginPolaznik(jmbg, password);
                if (prijavljenAdmin == null && prijavljenInstruktor == null && prijavljenPolaznik == null)
                {
                    MessageBox.Show("Pogresni podaci prijave!");
                } 
                else if(prijavljenAdmin != null)
                {
                    HomeWindow hw = new HomeWindow(centar, prijavljenAdmin);
                    this.Close();
                    hw.Show();
                }
                else if (prijavljenInstruktor != null)
                {
                    HomeWindow hw = new HomeWindow(centar, prijavljenInstruktor);
                    this.Close();
                    hw.Show();
                } 
                else if (prijavljenPolaznik != null)
                {
                    HomeWindow hw = new HomeWindow(centar, prijavljenPolaznik);
                    this.Close();
                    hw.Show();
                }
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HomeWindow hw = new HomeWindow(centar, null);
            this.Close();
            hw.Show();
        }

        private void ButtonRegistracija_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
