using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Windows;

namespace MediSave_data_saver
{
    public partial class EditData : Window
    {
        private string recordNo;

        public EditData(string recordNo)
        {
            InitializeComponent();
            this.recordNo = recordNo;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Concatenate notes
            string mainComplaint = MainComplaintTextBox.Text;
            string examination = ExaminationTextBox.Text;
            string results = ResultsTextBox.Text;
            string treatment = TreatmentTextBox.Text;

            string note = $"\n\nStart Note Date Time: {DateTime.Now}\n\n" +
                          $"Main Complaint:\n{mainComplaint}\n\n" +
                          $"Examination:\n{examination}\n\n" +
                          $"Results:\n{results}\n\n" +
                          $"Treatment:\n{treatment}\n\n" +
                          $"END NOTE";

            // Save to database
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MediSave", "AppData", "MediSaveDB.db");
            string connectionString = $"Data Source={dbPath}";

            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Patients SET Notes = Notes || @Note WHERE RecordNo = @RecordNo";
                using (SqliteCommand cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Note", note);
                    cmd.Parameters.AddWithValue("@RecordNo", recordNo);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Notes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to save notes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
