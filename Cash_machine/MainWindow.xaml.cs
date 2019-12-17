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

        MySqlConnection conn = new MySqlConnection(@"server=localhost;user=root;port=3306;database=Cash_machine;password=;");

        private void ConnectToSql()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Available_Click(object sender, RoutedEventArgs e)
        {
            ConnectToSql();
            try
            {
                String query = $"SELECT acc_balance FROM cards WHERE Card_nr={Login.CurrentCardNr}";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                Acc_balance balance = new Acc_balance();

                balance.balance.Text = String.Format("{0:0,0.00}", cmd.ExecuteScalar());

                balance.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
