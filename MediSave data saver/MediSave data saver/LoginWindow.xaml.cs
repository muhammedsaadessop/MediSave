using Microsoft.Data.Sqlite;
using System.Windows;
using System.Windows.Shapes;
using System.IO;
using Path = System.IO.Path;
using System.Windows.Input;

namespace MediSave_data_saver
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>

    public partial class LoginWindow : Window
        {
            private string dbPath = GetDatabasePath();
            public bool IsAuthenticated { get; private set; } = false;

            public LoginWindow()
            {
                InitializeComponent();
                CheckAndCreateUserDatabase();
            }

            private void Login_Click(object sender, RoutedEventArgs e)
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Password.Trim();

                if (AuthenticateUser(username, password))
                {
                    IsAuthenticated = true;
                    this.DialogResult = true;  // Close login window
                }
                else
                {
                    MessageBox.Show("Invalid credentials. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_Click(sender, e); // Simulate button click
            }
        }
        private bool AuthenticateUser(string username, string password)
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath};"))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username=@Username AND Password=@Password";

                using (var cmd = new SqliteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); // ⚠️ Use hashing in production!

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Persist login session
                        Properties.Settings.Default.IsLoggedIn = true;
                        Properties.Settings.Default.LoggedInUser = username;
                        Properties.Settings.Default.Save();  // Save settings

                        return true;
                    }
                    return false;
                }
            }
        }

        private void CheckAndCreateUserDatabase()
        {
            string dbPath = GetDatabasePath();

            // Check if the database file exists
            if (!File.Exists(dbPath))
            {
                MessageBox.Show("No user database found. Please create an admin account.", "Setup Required", MessageBoxButton.OK, MessageBoxImage.Information);
                CreateUserDatabase();
            }
            else
            {
                // If the database exists, check if the Users table contains any data
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM Users"; // Make sure to use the actual table name here (Users)

                    long userCount = (long)command.ExecuteScalar();

                    // If no users are found in the Users table, prompt to create an admin account
                    if (userCount == 0)
                    {
                        MessageBox.Show("The user database is empty. Please create an admin account.", "Setup Required", MessageBoxButton.OK, MessageBoxImage.Information);
                        CreateUserDatabase();
                    }
                }
            }
        }


        private void CreateUserDatabase()
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath};"))
                {
                    connection.Open();
                    string query = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Username TEXT PRIMARY KEY,
                        Role TEXT NOT NULL,
                        Password TEXT NOT NULL
                    );
                ";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                // Open user creation window
                RegisterUserWindow registerWindow = new RegisterUserWindow();
                if (registerWindow.ShowDialog() == true)
                {
                    MessageBox.Show("Admin account created successfully! Please log in.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Account setup is required to use the application.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
            }

        public static string GetDatabasePath()
        {
            // Get the path to the "Documents" folder
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Define the path to the MediSave folder and then AppData folder inside it
            string mediSaveFolder = Path.Combine(documentsFolder, "MediSave", "AppData");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(mediSaveFolder))
            {
                Directory.CreateDirectory(mediSaveFolder);
            }

            // Return full database path inside the AppData folder
            return Path.Combine(mediSaveFolder, "Users.db");
        }


    }
}





