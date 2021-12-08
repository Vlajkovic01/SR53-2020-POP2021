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
    /// Interaction logic for AddEditUsersAddressWindow.xaml
    /// </summary>
    public partial class AddEditUsersAddressWindow : Window
    {
        private EOdabraniStatus izabraniStatus;
        private Adresa izabranaAdresa;
        public AddEditUsersAddressWindow(Adresa adresa, EOdabraniStatus status = EOdabraniStatus.DODAJ)
        {
            InitializeComponent();
            this.DataContext = adresa;

            izabraniStatus = status;
            izabranaAdresa = adresa;

            if (status.Equals(EOdabraniStatus.IZMENI) && adresa != null)
            {
                this.Title = "Izmena adrese";
            }
            else
            {
                this.Title = "Dodavanje adrese";
            }

        }

        private void BtnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (Validacija())
            {
                if (izabraniStatus.Equals(EOdabraniStatus.DODAJ))
                {
                    izabranaAdresa.Aktivna = true;
                    Util.Instance.Adrese.Add(izabranaAdresa);


                }
                Util.Instance.SacuvajEntitet("adrese.txt");
                this.DialogResult = true;
                this.Close();
            }
        }

        private bool Validacija()
        {
            string poruka = "Molimo popravite sledece greske u unosu: " + "\n";
            bool ispravno = true;
            if (TxtUlica.Text.Equals(""))
            {
                poruka += "- Niste uneli Ulicu" + "\n";
                ispravno = false;
            }
            if (TxtBroj.Text.Equals(""))
            {
                poruka += "- Niste uneli Broj" + "\n";
                ispravno = false;
            }
            if (TxtGrad.Text.Equals(""))
            {
                poruka += "- Niste uneli Grad" + "\n";
                ispravno = false;
            }
            if (TxtDrzava.Text.Equals(""))
            {
                poruka += "- Niste uneli Drzavu" + "\n";
                ispravno = false;
            }
            if (ispravno == false)
            {
                MessageBox.Show(poruka, "Greska");
            }
            return ispravno;
        }
    }
}
