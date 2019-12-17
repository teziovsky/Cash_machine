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
    /// Logika interakcji dla klasy ChangePass.xaml
    /// </summary>
    public partial class ChangePass : Window
    {
        MySqlConnection conn = new MySqlConnection(@"server=localhost;user=root;port=3306;database=Cash_machine;password=;");

        public ChangePass()
        {
            InitializeComponent();
            ChangePass_connection();
        }

        private bool IsAllDigits(string s)
        {
            return s.All(char.IsDigit);
        }

        private void ChangePass_connection()
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

        private void ChangePass_query()
        {
            try
            {
                String query = $"SELECT Pass FROM cards WHERE Card_nr = {Login.CurrentCardNr}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                if (cmd.ExecuteScalar().ToString() == textNewPassword.Password)
                {
                    MessageBox.Show("To twój aktualny kod PIN. Jeżeli chcesz zmienić, wpisz inny kod PIN...", "ERROR", MessageBoxButton.OK);
                }
                else
                {
                    String query1 = $"UPDATE cards SET Pass = {textNewPassword.Password} WHERE Card_nr = {Login.CurrentCardNr}";
                    MySqlCommand cmd1 = new MySqlCommand(query1, conn);
                    cmd1.ExecuteScalar();
                    MessageBox.Show("Pomyślnie zmieniono kod PIN!", "SUKCES", MessageBoxButton.OK);
                    conn.Close();
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Changepass_Click(object sender, RoutedEventArgs e)
        {
            var new1 = IsAllDigits(textNewPassword.Password.ToString());
            var new2 = IsAllDigits(textNew1Password.Password.ToString());

            if (textNewPassword.Password != textNew1Password.Password){
                MessageBox.Show("Twoja kody PIN różnią się. Wpisz takie same kody PIN...", "ERROR", MessageBoxButton.OK);
            }
            else if (textNewPassword.Password.Length != 4 || textNew1Password.Password.Length != 4){
                MessageBox.Show("Kod PIN musi się składać z 4 cyfr. Wpisz poprawne kody PIN...", "ERROR", MessageBoxButton.OK);
            }
            else if (new1 == false || new2 == false) {
                MessageBox.Show("Twoj kody PIN zawierają litery. Kod PIN musi się składać wyłącznie z cyfr....", "ERROR", MessageBoxButton.OK);
            }
            else{
                ChangePass_query();
            }
        }
    }
}
