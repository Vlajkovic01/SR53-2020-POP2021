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
    public partial class AllUsersWindow : Window
    {
        ICollectionView view;
        RegistrovaniKorisnik trenutniKorisnik;
        public AllUsersWindow(RegistrovaniKorisnik korisnik)
        {
            InitializeComponent();
            trenutniKorisnik = korisnik;
            UpdateView();

            CBTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika));
            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;
            if(korisnik.Aktivan)
            {
                if(txtIme.Text != "")
                {
                    return korisnik.Ime.Contains(txtIme.Text);
                } 
                else if(txtPrezime.Text != "")
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
                if(CBTipKorisnika.SelectedItem != null)
                {
                    if(CBTipKorisnika.SelectedItem.Equals(ETipKorisnika.ADMINISTRATOR))
                    {
                        return korisnik.TipKorisnika.Equals(ETipKorisnika.ADMINISTRATOR);
                    }
                    
                    else if (CBTipKorisnika.SelectedItem.Equals(ETipKorisnika.INSTRUKTOR))
                    {
                        return korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR);
                    }
                    else if (CBTipKorisnika.SelectedItem.Equals(ETipKorisnika.POLAZNIK))
                    {
                        return korisnik.TipKorisnika.Equals(ETipKorisnika.POLAZNIK);
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        private void UpdateView()
        {
            DGKorisnici.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);
            DGKorisnici.ItemsSource = view;
            DGKorisnici.IsSynchronizedWithCurrentItem = true;

            DGKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
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
        private void CBTipKorisnika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            view.Refresh();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(trenutniKorisnik);
            homeWindow.Show();
        }
        private void DGKorisnici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik noviKorisnik = new RegistrovaniKorisnik();
            AddEditUserWindow aeu = new AddEditUserWindow(noviKorisnik);
            this.Hide();
            if (!(bool)aeu.ShowDialog())
            {

            }
            this.Show();
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik selektovaniKorisnik = view.CurrentItem as RegistrovaniKorisnik;
            RegistrovaniKorisnik stariKorisnik = selektovaniKorisnik.Clone();

            AddEditUserWindow addEditUser = new AddEditUserWindow(selektovaniKorisnik, EOdabraniStatus.IZMENI);
            this.Hide();
            if (!(bool)addEditUser.ShowDialog())
            {
                int index = Util.Instance.Korisnici.ToList().FindIndex(k => k.JMBG.Equals(stariKorisnik.JMBG));
                Util.Instance.Korisnici[index] = stariKorisnik;
            }
            this.Show();

            view.Refresh();
        }

        private void BtnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            if(DGKorisnici.SelectedIndex != -1)
            {
                RegistrovaniKorisnik selektovani = view.CurrentItem as RegistrovaniKorisnik;

                if (MessageBox.Show($"Da li ste sigurni da zelite da obrisete?{selektovani.Ime + " " + selektovani.Prezime} ", "Potvrda", MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
                {
                    
                    Util.Instance.BrisanjeKorisnika(selektovani.JMBG);
                    UpdateView();
                    view.Refresh();
                }
                    
            } else
            {
                MessageBox.Show("Morate izabrati korisnika.");
            }

        }
    }
}
