using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
using Path = System.IO.Path;

namespace MediSave_data_saver
{
    /// <summary>
    /// Interaction logic for DisplayData.xaml
    /// </summary>
    public partial class DisplayData : UserControl
    {
        public DisplayData()
        {
            InitializeComponent();
            LoadData();
            // Get the screen resolution and adjust the window size
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            // Optionally adjust the size (e.g., 80% of the screen size)
            this.Width = screenWidth * 0.8;
            this.Height = screenHeight * 0.8;
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

        private void LoadData()
        {
            string dbPath = GetDatabasePath(); // Specify your database path
            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, FullName, Address, TelNo, Gender, DateOfBirth, MedicalAid, RecordNo, Occupation, Employer, Allergies, SpecialFeatures, Notes FROM Patients";

                using (var cmd = new SqliteCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    var patients = new List<Patient>();
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            Id = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            Address = reader.GetString(2),
                            TelNo = reader.GetString(3),
                            Gender = reader.GetString(4),
                            DateOfBirth = reader.GetString(5),
                            MedicalAid = reader.GetString(6),
                            RecordNo = reader.GetString(7),
                            Occupation = reader.GetString(8),
                            Employer = reader.GetString(9),
                            Allergies = reader.GetString(10),
                            SpecialFeatures = reader.GetString(11),
                            Notes = reader.GetString(12)
                        });
                    }

                    // Bind the list of patients to the DataGrid
                    patientsDataGrid.ItemsSource = patients;
                }
            }
        }

        // Handle View Patient button click event
        private void ViewPatient_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var patient = button?.DataContext as Patient;

            if (patient != null)
            {
                // Open the ViewPatient window without closing the main window
                var viewPatientWindow = new VeiwPatient(patient.RecordNo);
                viewPatientWindow.Show();
            }
        }
        // Handle Edit button click event
        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var patient = button?.DataContext as Patient;

            if (patient != null)
            {
                // Open the EditData window without closing the main window
                var editDataWindow = new EditData(patient.RecordNo);
                editDataWindow.Show();
            }
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchField = ((ComboBoxItem)SearchFieldComboBox.SelectedItem)?.Content.ToString();
            string searchTerm = SearchTextBox.Text.Trim();
            Trace.WriteLine(searchField);

            if (string.IsNullOrEmpty(searchField) || string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Please select a field and enter a search term.");
                return;
            }

            // Ensure the searchField exists in the database schema
            string query = $"SELECT * FROM Patients WHERE LOWER(\"{searchField}\") LIKE LOWER(@searchTerm) ORDER BY Id ASC";

            using (var connection = new SqliteConnection($"Data Source={GetDatabasePath()}"))
            {
                connection.Open();
                using (var cmd = new SqliteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    var reader = cmd.ExecuteReader();
                    var results = new List<Patient>();

                    while (reader.Read())
                    {
                        results.Add(new Patient
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            TelNo = reader.GetString(reader.GetOrdinal("TelNo")),
                            Gender = reader.GetString(reader.GetOrdinal("Gender")),
                            DateOfBirth = reader.GetString(reader.GetOrdinal("DateOfBirth")),
                            MedicalAid = reader.GetString(reader.GetOrdinal("MedicalAid")),
                            RecordNo = reader.GetString(reader.GetOrdinal("RecordNo")),
                            Occupation = reader.GetString(reader.GetOrdinal("Occupation")),
                            Employer = reader.GetString(reader.GetOrdinal("Employer")),
                            Allergies = reader.GetString(reader.GetOrdinal("Allergies")),
                            SpecialFeatures = reader.GetString(reader.GetOrdinal("SpecialFeatures")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes"))
                        });
                    }

                    patientsDataGrid.ItemsSource = results;
                }
            }
        }



        // Back action
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow)?.ShowMenu();
        }

       


        // Define the patient class to map data
        public class Patient
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Address { get; set; }
            public string TelNo { get; set; }
            public string Gender { get; set; }
            public string DateOfBirth { get; set; }
            public string MedicalAid { get; set; }
            public string RecordNo { get; set; }
            public string Occupation { get; set; }
            public string Employer { get; set; }
            public string Allergies { get; set; }
            public string SpecialFeatures { get; set; }
            public string Notes { get; set; }
        }
    }
   
}

