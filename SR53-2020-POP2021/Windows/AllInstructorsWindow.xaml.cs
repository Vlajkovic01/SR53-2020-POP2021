using SR53_2020_POP2021.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AllInstructorsWindow.xaml
    /// </summary>
    public partial class AllInstructorsWindow : Window
    {
        //ICollectionView view;
        RegistrovaniKorisnik trenutniKorisnik;
        public AllInstructorsWindow(RegistrovaniKorisnik korisnik)
        {
            InitializeComponent();
            trenutniKorisnik = korisnik;
        }

        private bool CustomFilter(object obj)
        {
            return false;
        }

        private void txtIme_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtPrezime_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtUlica_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnPretrazi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DGInstruktori_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(trenutniKorisnik);
            homeWindow.Show();
        }

        private void Window_Closing_1(object sender, CancelEventArgs e)
        {

        }
    }
}
