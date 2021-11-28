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
    /// Interaction logic for AllAddressWindow.xaml
    /// </summary>
    public partial class AllAddressWindow : Window
    {
        ICollectionView view;
        RegistrovaniKorisnik trenutniKorisnik;
        public AllAddressWindow(RegistrovaniKorisnik korisnik)
        {
            InitializeComponent();
            trenutniKorisnik = korisnik;
            UpdateView();

            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            Adresa adresa = obj as Adresa;
            if (adresa.Aktivna)
            {
                if (txtUlicaAdrese.Text != "")
                {
                    return adresa.Ulica.Contains(txtUlicaAdrese.Text);
                }
                else if (txtBroj.Text != "")
                {
                    return adresa.Broj.Contains(txtBroj.Text);
                }
                else if (txtGrad.Text != "")
                {
                    return adresa.Grad.Contains(txtGrad.Text);
                }
                else if (txtDrzava.Text != "")
                {
                    return adresa.Drzava.Contains(txtDrzava.Text);
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
            DGAdrese.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Adrese);
            DGAdrese.ItemsSource = view;
            DGAdrese.IsSynchronizedWithCurrentItem = true;

            DGAdrese.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGAdrese.SelectedItems.Clear();
        }

        private void txtUlicaAdrese_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtBroj_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtDrzava_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtGrad_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Adresa novaAdresa = new Adresa();
            novaAdresa.ID = Util.Instance.GenerisanjeIDAdrese();
            AddEditUsersAddressWindow aeua = new AddEditUsersAddressWindow(novaAdresa);
            this.Hide();
            if (!(bool)aeua.ShowDialog())
            {

            }
            this.Show();
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if(DGAdrese.SelectedIndex != -1)
            {
                Adresa selektovanaAdresa = view.CurrentItem as Adresa;
                Adresa staraAdresa = selektovanaAdresa.Clone();

                AddEditUsersAddressWindow addEditAddress = new AddEditUsersAddressWindow(selektovanaAdresa, EOdabraniStatus.IZMENI);
                this.Hide();
                if (!(bool)addEditAddress.ShowDialog())
                {
                    int index = Util.Instance.Adrese.ToList().FindIndex(k => k.ID.Equals(staraAdresa.ID));
                    Util.Instance.Adrese[index] = staraAdresa;
                }
                this.Show();

                view.Refresh();
                DGAdrese.SelectedItems.Clear();
            }
            else
            {
                MessageBox.Show("Morate izabrati adresu.");
            }

        }

        private void BtnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (DGAdrese.SelectedIndex != -1)
            {
                Adresa selektovana = view.CurrentItem as Adresa;

                if (MessageBox.Show($"Da li ste sigurni da zelite da obrisete?{selektovana.Ulica + " " + selektovana.Broj} ", "Potvrda", MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
                {

                    Util.Instance.BrisanjeAdrese(selektovana.ID);
                    UpdateView();
                    view.Refresh();
                }

            }
            else
            {
                MessageBox.Show("Morate izabrati adresu.");
            }
        }

        private void DGAdrese_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(trenutniKorisnik);
            homeWindow.Show();
        }
    }
}
