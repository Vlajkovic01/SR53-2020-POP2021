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
        private Adresa adresa;
        public AddEditUsersAddressWindow(Adresa adresa, EOdabraniStatus status)
        {
            InitializeComponent();
            this.izabraniStatus = status;

        }

        private void BtnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
