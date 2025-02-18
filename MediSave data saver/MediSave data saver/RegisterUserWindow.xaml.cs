using Microsoft.Data.Sqlite;
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

namespace MediSave_data_saver
{
    public partial class RegisterUserWindow : Window
    {
        private string dbPath = LoginWindow.GetDatabasePath();

        public RegisterUserWindow()
        {
            InitializeComponent();
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            string role = cmbRole.Text;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();
            string confirmPassword = txtConfirmPassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var connection = new SqliteConnection($"Data Source={dbPath};"))
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, Role, Password) VALUES (@Username, @Role, @Password)";

                using (var cmd = new SqliteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@Password", password);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Admin account created!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
        }
    }
}