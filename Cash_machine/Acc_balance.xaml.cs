using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logika interakcji dla klasy Acc_balance.xaml
    /// </summary>
    public partial class Acc_balance : Window
    {
        public Acc_balance()
        {
            InitializeComponent();
            Acc_balance_query();
        }

        private void Acc_balance_query()
        {
            MySqlConnection conn = new MySqlConnection(@"server=localhost;user=root;port=3306;database=Cash_machine;password=;");
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
            try
            {
                String query = $"SELECT acc_balance FROM cards WHERE Card_nr={Login.CurrentCardNr}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                balance.Text = String.Format("{0:0,0.00}", cmd.ExecuteScalar());
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
