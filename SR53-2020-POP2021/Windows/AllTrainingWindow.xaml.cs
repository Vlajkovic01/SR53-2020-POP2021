using SR53_2020_POP2021.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AllTrainingWindow.xaml
    /// </summary>
    public partial class AllTrainingWindow : Window
    {
        ICollectionView view;
        RegistrovaniKorisnik trenutniKorisnik;
        public AllTrainingWindow(RegistrovaniKorisnik korisnik)
        {
            InitializeComponent();
            trenutniKorisnik = korisnik;
            UpdateView();

            CBStatus.ItemsSource = Enum.GetValues(typeof(EStatusTreninga));

            view.Filter = CustomFilter;
            if(trenutniKorisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR))
            {
                BtnIzmeni.Visibility = Visibility.Collapsed;
            }
        }

        private bool CustomFilter(object obj)
        {
            Trening trening = obj as Trening;
            if (trening.Aktivan)
            {
                if (txtDatum.Text != "")
                {
                    return trening.DatumTreninga.Contains(txtDatum.Text);
                }
                else if (txtVreme.Text != "")
                {
                    return trening.VremePocetkaTreninga.Contains(txtVreme.Text);
                }
                else if (txtTrajanje.Text != "")
                {
                    int.TryParse(txtTrajanje.Text, out int trajanje);
                    return trening.TrajanjeTreninga.Equals(trajanje);
                }
                if (CBStatus.SelectedItem != null)
                {
                    if (CBStatus.SelectedItem.Equals(EStatusTreninga.SLOBODAN))
                    {
                        return trening.StatusTreninga.Equals(EStatusTreninga.SLOBODAN);
                    }
                    else if (CBStatus.SelectedItem.Equals(EStatusTreninga.REZERVISAN))
                    {
                        return trening.StatusTreninga.Equals(EStatusTreninga.REZERVISAN);
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
            DGTreninzi.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Treninzi);
            DGTreninzi.ItemsSource = view;
            DGTreninzi.IsSynchronizedWithCurrentItem = true;

            DGTreninzi.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGTreninzi.SelectedItems.Clear(); //da ne bira prvog u tabeli za brisanje
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Trening noviTrening = new Trening();
            noviTrening.ID = Util.Instance.GenerisanjeIDTreninga();
            AddEditTraining aet = new AddEditTraining(trenutniKorisnik, noviTrening);
            aet.CBStatus.IsEnabled = false;
            this.Hide();
            if (!(bool)aet.ShowDialog())
            {

            }
            this.Show();
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (DGTreninzi.SelectedIndex != -1)
            {
                Trening selektovanTrening = view.CurrentItem as Trening;
                Trening stariTrening = selektovanTrening.Clone();

                AddEditTraining addEditTraining = new AddEditTraining(trenutniKorisnik ,selektovanTrening, EOdabraniStatus.IZMENI);
                this.Hide();
                if (!(bool)addEditTraining.ShowDialog())
                {
                    int index = Util.Instance.Treninzi.ToList().FindIndex(k => k.ID.Equals(stariTrening.ID));
                    Util.Instance.Treninzi[index] = stariTrening;
                }
                this.Show();

                view.Refresh();
                DGTreninzi.SelectedItems.Clear();
            }
            else
            {
                MessageBox.Show("Morate izabrati trening.");
            }
        }

        private void BtnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            Trening selektovan = view.CurrentItem as Trening;
            if (trenutniKorisnik.TipKorisnika.Equals(ETipKorisnika.ADMINISTRATOR))
            {
                if (DGTreninzi.SelectedIndex != -1)
                {

                    if (MessageBox.Show($"Da li ste sigurni da zelite da obrisete?{selektovan.DatumTreninga + " " + selektovan.VremePocetkaTreninga} ", "Potvrda", MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
                    {

                        Util.Instance.BrisanjeTreninga(selektovan.ID);
                        UpdateView();
                        view.Refresh();
                    }

                }
                else
                {
                    MessageBox.Show("Morate izabrati trening.");
                }
            } else if (trenutniKorisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR))
            {
                if(DGTreninzi.SelectedIndex != -1)
                {
                    if (selektovan.StatusTreninga.Equals(EStatusTreninga.SLOBODAN) && selektovan.Instruktor.Korisnik.JMBG.Equals(trenutniKorisnik.JMBG))
                    {
                        
                        if (MessageBox.Show($"Da li ste sigurni da zelite da obrisete?{selektovan.DatumTreninga + " " + selektovan.VremePocetkaTreninga} ", "Potvrda", MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
                        {

                            Util.Instance.BrisanjeTreninga(selektovan.ID);
                            UpdateView();
                            view.Refresh();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Mozete izbrisati samo SVOJ i SLOBODAN trening.");
                    }
                }
                else
                {
                    MessageBox.Show("Morate izabrati trening");
                }
            }

        }

        private void txtDatum_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtVreme_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtTrajanje_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void CBStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            view.Refresh();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(trenutniKorisnik);
            homeWindow.Show();
        }

        private void DGTreninzi_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
    }
}
