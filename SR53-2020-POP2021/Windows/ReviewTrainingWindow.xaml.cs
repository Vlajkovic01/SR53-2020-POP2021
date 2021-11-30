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
    /// Interaction logic for ReviewTrainingWindow.xaml
    /// </summary>
    public partial class ReviewTrainingWindow : Window
    {
        ICollectionView view;
        RegistrovaniKorisnik trenutniKorisnik;
        RegistrovaniKorisnik trenutniInstruktor;
        public ReviewTrainingWindow(RegistrovaniKorisnik korisnik, RegistrovaniKorisnik instruktor)
        {
            InitializeComponent();
            trenutniKorisnik = korisnik;
            trenutniInstruktor = instruktor;
            UpdateView();

            CBStatus.ItemsSource = Enum.GetValues(typeof(EStatusTreninga));

            view.Filter = CustomFilter;
        }
        private bool CustomFilter(object obj)
        {
            Trening trening = obj as Trening;
            if (trening.Aktivan && trening.Instruktor.Korisnik.JMBG.Equals(trenutniInstruktor.JMBG))
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

        private void BtnRezervisi_Click(object sender, RoutedEventArgs e)
        {
            if (DGTreninzi.SelectedIndex != -1)
            {
                Trening selektovanTrening = view.CurrentItem as Trening;
                if(selektovanTrening.StatusTreninga.Equals(EStatusTreninga.SLOBODAN))
                {
                    selektovanTrening.Polaznik = Util.Instance.Polaznici.ToList().Find(k => k.Korisnik.JMBG.Equals(trenutniKorisnik.JMBG)); // kad ne stavim ovako menja svim treninzima polaznika
                    selektovanTrening.StatusTreninga = EStatusTreninga.REZERVISAN;
                    Util.Instance.SacuvajEntitet("treninzi.txt");
                } else
                {
                    MessageBox.Show("Morate izabrati SLOBODAN trening.");
                }
                
                view.Refresh();
                DGTreninzi.SelectedItems.Clear();
            }
            else
            {
                MessageBox.Show("Morate izabrati trening.");
            }
        }

        private void BtnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            if (DGTreninzi.SelectedIndex != -1)
            {
                Trening selektovanTrening = view.CurrentItem as Trening;
                if (selektovanTrening.StatusTreninga.Equals(EStatusTreninga.REZERVISAN) && selektovanTrening.Polaznik.Korisnik.JMBG.Equals(trenutniKorisnik.JMBG))
                {

                    selektovanTrening.Polaznik = Util.Instance.Polaznici.ToList().Find(k => k.Korisnik.JMBG.Equals("0000000000000")); // stavlja nultog polaznika tj kao da nema polaznika
                    selektovanTrening.StatusTreninga = EStatusTreninga.SLOBODAN;
                    Util.Instance.SacuvajEntitet("treninzi.txt");
                }
                else
                {
                    MessageBox.Show("Morate izabrati SVOJ REZERVISAN trening.");
                }

                view.Refresh();
                DGTreninzi.SelectedItems.Clear();
            }
            else
            {
                MessageBox.Show("Morate izabrati trening.");
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

        private void DGTreninzi_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
    }
}
