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
using System.Windows.Shapes;

namespace Cash_machine
{
    /// <summary>
    /// Logika interakcji dla klasy Withdrawal.xaml
    /// </summary>
    public partial class Withdrawal : Window
    {
        public Withdrawal()
        {
            InitializeComponent();
        }

        private bool IsAllDigits(string s)
        {
            return s.All(char.IsDigit);
        }

        private void Withdrawalsubmit_Click(object sender, RoutedEventArgs e)
        {
            var new1 = IsAllDigits(textWithdrawalnumber.Text);

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
                int amount = Convert.ToInt32(cmd.ExecuteScalar());
                int.TryParse(textWithdrawalnumber.Text, out int amount2);
                if (amount2 > amount)
                {
                    MessageBox.Show("Podana kwota jest wyższa niż twój stan konta...", "ERROR", MessageBoxButton.OK);
                }
                else if (amount2 < 0)
                {
                    MessageBox.Show("Podana kwota jest ujemna. Proszę wpisać poprawną kwotę...", "ERROR", MessageBoxButton.OK);
                }
                else if (new1 == false)
                {
                    MessageBox.Show("Podana kwota zawiera litery. Kwota musi się składać wyłącznie z cyfr...", "ERROR", MessageBoxButton.OK);
                }
                else
                {
                    int amount3 = amount - amount2;
                    String query1 = $"UPDATE cards SET acc_balance = {amount3} WHERE Card_nr = {Login.CurrentCardNr}";
                    MySqlCommand cmd1 = new MySqlCommand(query1, conn);
                    cmd1.ExecuteScalar();
                    MessageBox.Show($"Pomyślnie wypłacono {amount2} zł.", "SUKCES", MessageBoxButton.OK);
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
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
