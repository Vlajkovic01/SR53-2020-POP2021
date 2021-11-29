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
    /// Interaction logic for ReviewInstructorsWindow.xaml
    /// </summary>
    public partial class ReviewInstructorsWindow : Window
    {
        ICollectionView view;
        RegistrovaniKorisnik trenutniKorisnik;
        public ReviewInstructorsWindow(RegistrovaniKorisnik korisnik)
        {
            InitializeComponent();
            trenutniKorisnik = korisnik;
            UpdateView();

            if(trenutniKorisnik == null)
            {
                BtnTreninzi.Visibility = Visibility.Collapsed;
            }

            
            view.Filter = CustomFilter;
        }
        private void UpdateView()
        {
            DGInstruktori.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);
            DGInstruktori.ItemsSource = view;
            DGInstruktori.IsSynchronizedWithCurrentItem = true;

            DGInstruktori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGInstruktori.SelectedItems.Clear(); //da ne bira prvog u tabeli za brisanje
        }
        private bool CustomFilter(object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;
            if (korisnik.Aktivan && korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR))
            {
                if (txtIme.Text != "")
                {
                    return korisnik.Ime.Contains(txtIme.Text);
                }
                else if (txtPrezime.Text != "")
                {
                    return korisnik.Prezime.Contains(txtPrezime.Text);
                }
                else if (txtUlica.Text != "")
                {
                    return korisnik.Adresa.Ulica.Contains(txtUlica.Text);
                }
                else if (txtEmail.Text != "")
                {
                    return korisnik.Email.Contains(txtEmail.Text);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        private void txtIme_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtPrezime_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtUlica_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }


        private void BtnTreninzi_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DGInstruktori_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if(trenutniKorisnik == null)
            {
                FitnesCenterWindow fcw = new FitnesCenterWindow(trenutniKorisnik);

                this.Hide();
                fcw.Show();
            } else
            {
                HomeWindow homeWindow = new HomeWindow(trenutniKorisnik);
                homeWindow.Show();
            }
        }
    }
}
