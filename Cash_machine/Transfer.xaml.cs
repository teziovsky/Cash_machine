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
    /// Logika interakcji dla klasy Transfer.xaml
    /// </summary>
    public partial class Transfer : Window
    {
        public Transfer()
        {
            InitializeComponent();
        }

        private bool IsAllDigits(string s)
        {
            return s.All(char.IsDigit);
        }

        private void Transfervalue_Click(object sender, RoutedEventArgs e)
        {
            var new1 = IsAllDigits(textReceivercardnr.Text);
            var new2 = IsAllDigits(textTransfervalue.Text);

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
                String query = $"SELECT COUNT(1) FROM cards WHERE Card_nr={textReceivercardnr.Text}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                String query1 = $"SELECT acc_balance FROM cards WHERE Card_nr={Login.CurrentCardNr}";
                MySqlCommand cmd1 = new MySqlCommand(query1, conn);
                String query2 = $"SELECT acc_balance FROM cards WHERE Card_nr={textReceivercardnr.Text}";
                MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                int amount = Convert.ToInt32(cmd1.ExecuteScalar());
                int amount2 = Convert.ToInt32(cmd2.ExecuteScalar());
                int.TryParse(textTransfervalue.Text, out int amount3);
                if (count != 1)
                {
                    MessageBox.Show("Błędny numer karty. Wpisz poprawny numer karty...", "ERROR", MessageBoxButton.OK);
                }
                else if (Login.CurrentCardNr == textReceivercardnr.Text)
                {
                    MessageBox.Show("Wpisałeś swój numer konta. Wpisz inny numer konta, żeby przelać środki...", "ERROR", MessageBoxButton.OK);
                }
                else if (amount3 > amount)
                {
                    MessageBox.Show("Podana kwota jest wyższa niż twój stan konta...", "ERROR", MessageBoxButton.OK);
                }
                else if (amount3 < 0)
                {
                    MessageBox.Show("Podana kwota jest ujemna. Proszę wpisać poprawną kwotę...", "ERROR", MessageBoxButton.OK);
                }
                else if (new1 == false)
                {
                    MessageBox.Show("Podane dane zawierają litery. Kwota oraz numer karty muszą składać się wyłącznie z cyfr...", "ERROR", MessageBoxButton.OK);
                }
                else
                {
                    int amount4 = amount2 + amount3;
                    String query3 = $"UPDATE cards SET acc_balance = {amount4} WHERE Card_nr = {textReceivercardnr.Text}";
                    MySqlCommand cmd3 = new MySqlCommand(query3, conn);
                    cmd3.ExecuteScalar();
                    amount = amount - amount3;
                    String query4 = $"UPDATE cards SET acc_balance = {amount} WHERE Card_nr = {Login.CurrentCardNr}";
                    MySqlCommand cmd4 = new MySqlCommand(query4, conn);
                    cmd4.ExecuteScalar();
                    MessageBox.Show($"Pomyślnie przelano {amount3} zł, na kartę o numerze - {textReceivercardnr.Text}.", "SUKCES", MessageBoxButton.OK);
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
