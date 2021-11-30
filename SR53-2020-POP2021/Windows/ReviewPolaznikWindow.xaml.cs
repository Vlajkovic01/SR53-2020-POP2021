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
    /// Interaction logic for ReviewPolaznikWindow.xaml
    /// </summary>
    public partial class ReviewPolaznikWindow : Window
    {
        RegistrovaniKorisnik trenutniKorisnik;
        public ReviewPolaznikWindow(RegistrovaniKorisnik polaznik, RegistrovaniKorisnik korisnik)
        {
            InitializeComponent();
            this.DataContext = polaznik;
            trenutniKorisnik = korisnik;

            TxtIme.IsEnabled = false;
            TxtPrezime.IsEnabled = false;
            TxtPol.IsEnabled = false;

            TxtAdresa.IsEnabled = false;
            TxtAdresa.Text = polaznik.Adresa.ToString();

            TxtEmail.IsEnabled = false;
            TxtTipKorisnika.IsEnabled = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InstructorsTrainingWindow review = new InstructorsTrainingWindow(trenutniKorisnik);
            this.Hide();
            review.Show();
        }
    }
}
