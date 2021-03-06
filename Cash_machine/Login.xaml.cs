﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static string CurrentCardNr;

        public Login()
        {
            InitializeComponent();
        }

        private bool IsAllDigits(string s)
        {
            return s.All(char.IsDigit);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var new1 = IsAllDigits(textCardnumber.Text);
            var new2 = IsAllDigits(textPassword.Password);
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
                String query = $"SELECT COUNT(1) FROM cards WHERE Card_nr={textCardnumber.Text} AND Pass={textPassword.Password}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count != 1 || new1 == false || new2 == false)
                {
                    MessageBox.Show("Błędne dane logowania. Spróbuj ponownie...", "ERROR", MessageBoxButton.OK);
                }
                else
                {
                    CurrentCardNr = textCardnumber.Text;
                    MainWindow Cash_mainmenu = new MainWindow();
                    Cash_mainmenu.Show();
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

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nr_Karty: 123456789 Pin: 1234" + "\n" + "Nr_Karty: 987654321 Pin: 4321", "Przykładowe dane logowania", MessageBoxButton.OK);
        }
    }
}
