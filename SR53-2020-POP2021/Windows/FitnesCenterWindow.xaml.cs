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
    /// Interaction logic for AllFitnesCenterWindow.xaml
    /// </summary>
    public partial class FitnesCenterWindow : Window
    {
        ICollectionView view;
        RegistrovaniKorisnik trenutniKorisnik;
        public FitnesCenterWindow(RegistrovaniKorisnik korisnik)
        {
            InitializeComponent();
            trenutniKorisnik = korisnik;
            UpdateView();

            view.Filter = CustomFilter;

            if(trenutniKorisnik != null)
            {
                BtnPregledajInstruktore.Visibility = Visibility.Collapsed;
                BtnOdjavi.Visibility = Visibility.Collapsed;
            }

        }
        private bool CustomFilter(object obj)
        {
            Centar centar = obj as Centar;
            if (centar.Aktivan)
            {
                return true;
            }
            return false;
        }
        private void UpdateView()
        {
            DGCentar.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Centri);
            DGCentar.ItemsSource = view;
            DGCentar.IsSynchronizedWithCurrentItem = true;

            DGCentar.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGCentar.SelectedItems.Clear(); //da ne bira prvog u tabeli za brisanje
        }
        private void DGCentar_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void BtnPregledajInstruktore_Click(object sender, RoutedEventArgs e)
        {
            ReviewInstructorsWindow review = new ReviewInstructorsWindow(trenutniKorisnik);

            this.Hide();
            review.Show();
        }

        private void BtnOdjavi_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();

            this.Hide();
            lw.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(trenutniKorisnik);
            homeWindow.Show();
        }
    }
}
