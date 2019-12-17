using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cash_machine
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            Payment payment = new Payment();
            payment.Show();
            this.Close();
        }

        private void Available_Click(object sender, RoutedEventArgs e)
        {
            Acc_balance acc_balance = new Acc_balance();
            acc_balance.Show();
            this.Close();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePass changepass = new ChangePass();
            changepass.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            Transfer transfer = new Transfer();
            transfer.Show();
            this.Close();
        }
    }
}
