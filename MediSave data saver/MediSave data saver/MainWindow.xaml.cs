using Microsoft.Data.Sqlite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;
using System;
using System.IO;
using Path = System.IO.Path;
using Microsoft.Win32;
using MediSave_data_saver.Properties;
namespace MediSave_data_saver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  // Initialize UI first

            GetDatabasePath();
            InitializeDatabase();

            // Check if user is already logged in
            if (!Settings.Default.IsLoggedIn)
            {
                LoginWindow login = new LoginWindow();
                bool? loginResult = login.ShowDialog();

                if (loginResult == true)  // If login is successful
                {
                    Settings.Default.IsLoggedIn = true;
                    Settings.Default.Save();
                }
                else
                {
                    Application.Current.Shutdown(); // Close the app if login fails
                    return;
                }
            }
            else
            {
                MessageBox.Show($"Welcome back, {Settings.Default.LoggedInUser}!", "Login Restored");
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Clear saved login state
            Settings.Default.IsLoggedIn = false;
            Settings.Default.LoggedInUser = string.Empty;
            Settings.Default.Save();

            MessageBox.Show("You have been logged out.", "Logout", MessageBoxButton.OK, MessageBoxImage.Information);

            // Show the login screen again
            LoginWindow login = new LoginWindow();
            bool? loginResult = login.ShowDialog();

            if (loginResult == true)
            {
                // User successfully re-logged in, continue running the app
            }
            else
            {
                // Close the app if login is not successful
                Application.Current.Shutdown();
            }
        }


        public void InitializeDatabase()
        {
            string dbPath = GetDatabasePath();

            if (!File.Exists(dbPath))
            {
                MessageBoxResult result = MessageBox.Show(
                    "Database not found. Would you like to create a new one?",
                    "Database Setup",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {
                    CreateDatabase();
                    MessageBox.Show("New database created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (result == MessageBoxResult.No)
                {
                    ImportDatabase();
                }
                else
                {
                    MessageBox.Show("Operation cancelled. The application may not work correctly without a database.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        public void CreateDatabase()
        {
            string dbPath = GetDatabasePath();

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText =
                @"
        CREATE TABLE IF NOT EXISTS Patients (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            RecordNo TEXT NOT NULL UNIQUE,
            FullName TEXT NOT NULL,
            Address TEXT NOT NULL,
            Gender TEXT NOT NULL,
            DateOfBirth TEXT NOT NULL,
            TelNo TEXT NOT NULL,
            MedicalAid TEXT,
            MemberNo TEXT,
            SpecialFeatures TEXT,
            Employer TEXT,
            Occupation TEXT,
            Allergies TEXT,
            Notes TEXT
        );
        ";
                createTableCmd.ExecuteNonQuery();
            }
        }


        private void ImportDatabase()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select SQLite Database File",
                Filter = "SQLite Database (*.db)|*.db",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string sourceFilePath = openFileDialog.FileName;
                string destinationFilePath = GetDatabasePath();

                try
                {
                    File.Copy(sourceFilePath, destinationFilePath, true);
                    MessageBox.Show("Database imported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error importing database: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string GetDatabasePath()
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
            return Path.Combine(mediSaveFolder, "MediSaveDB.db");
        }


        private void OpenTab_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            string tabTag = clickedButton.Tag.ToString();

            UserControl selectedControl = null;

            // Load the correct UserControl based on button clicked
            switch (tabTag)
            {
                case "PatientData":
                    selectedControl = new PatientData(); // Load PatientData UserControl
                    break;
                case "Display":
                  selectedControl = new DisplayData(); // Replace with actual UserControl2
                 break;
                //case "3":
                //    selectedControl = new UserControl3(); // Replace with actual UserControl3
                //    break;
                //case "4":
                //    selectedControl = new UserControl4(); // Replace with actual UserControl4
                //    break;
                //case "5":
                //    selectedControl = new UserControl5(); // Replace with actual UserControl5
                //    break;
            }

            if (selectedControl != null)
            {
                // Hide Menu and Show Selected UserControl
                MenuPanel.Visibility = Visibility.Collapsed;
                ContentArea.Content = selectedControl;
                ContentArea.Visibility = Visibility.Visible;
            }
        }

        public void ShowMenu()
        {
            // Show Menu and Hide Content
            MenuPanel.Visibility = Visibility.Visible;
            ContentArea.Visibility = Visibility.Collapsed;
            ContentArea.Content = null;
        }
    }
}