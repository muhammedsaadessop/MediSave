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
using System;
using System.IO;
using System.Windows;
using Microsoft.Data.Sqlite;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Path = System.IO.Path;

namespace MediSave_data_saver
{
    /// <summary>
    /// Interaction logic for VeiwPatient.xaml
    /// </summary>
    public partial class VeiwPatient : Window
    {
        private string recordNo;
        public string PatientNotes { get; set; }

        public VeiwPatient(string recordNo)
        {
            
            InitializeComponent();
            this.recordNo = recordNo;
            LoadPatientData(recordNo);
        }
        private void LoadPatientData(string recordNo)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MediSave", "AppData", "MediSaveDB.db");
            string connectionString = $"Data Source={dbPath}";

            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Patients WHERE RecordNo = @RecordNo";
                using (SqliteCommand cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RecordNo", recordNo);
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtHeading.Text = $"{reader["FullName"]} - {recordNo}";
                            txtPatientDetails.Text =
                                "Personal Information\n" +
                                "---------------------------\n" +
                                $"Full Name: {reader["FullName"]}\n" +
                                $"Gender: {reader["Gender"]}\n" +
                                $"Date of Birth: {reader["DateOfBirth"]}\n\n" +
                                "Contact Details\n" +
                                "---------------------------\n" +
                                $"Address: {reader["Address"]}\n" +
                                $"Phone: {reader["TelNo"]}\n\n" +
                                "Employment Details\n" +
                                "---------------------------\n" +
                                $"Occupation: {reader["Occupation"]}\n" +
                                $"Employer: {reader["Employer"]}\n\n" +
                                "Medical Details\n" +
                                "---------------------------\n" +
                                $"Medical Aid: {reader["MedicalAid"]}\n" +
                                $"Member No: {reader["MemberNo"]}\n" +
                                $"Allergies: {reader["Allergies"]}\n" +
                                $"Special Features: {reader["SpecialFeatures"]}\n\n";

                            // Append the Notes here for the UI display (TextBox)
                            string notesText = $"Additional Notes\n---------------------------\n{reader["Notes"]}\n";
                            txtPatientDetails.Text += notesText;
                        }
                    }
                }
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MediSave", "AppData", "MediSaveDB.db");
            string connectionString = $"Data Source={dbPath}";

            // Open the database connection
            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                // Query for the patient data
                string query = "SELECT * FROM Patients WHERE RecordNo = @RecordNo";
                using (SqliteCommand cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RecordNo", recordNo);

                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string fullName = reader["FullName"].ToString();
                            string gender = reader["Gender"].ToString();
                            string dateOfBirth = reader["DateOfBirth"].ToString();
                            string address = reader["Address"].ToString();
                            string phone = reader["TelNo"].ToString();
                            string occupation = reader["Occupation"].ToString();
                            string employer = reader["Employer"].ToString();
                            string medicalAid = reader["MedicalAid"].ToString();
                            string memberNo = reader["MemberNo"].ToString();
                            string allergies = reader["Allergies"].ToString();
                            string specialFeatures = reader["SpecialFeatures"].ToString();
                            string notes = reader["Notes"].ToString();

                            // Generate the PDF
                            string exportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{fullName}_{recordNo}.pdf");
                            PdfDocument pdf = new PdfDocument();
                            PdfPage page = pdf.AddPage();
                            XGraphics gfx = XGraphics.FromPdfPage(page);
                            XFont titleFont = new XFont("Arial", 14, XFontStyleEx.Bold);
                            XFont sectionFont = new XFont("Arial", 12, XFontStyleEx.Bold);
                            XFont contentFont = new XFont("Arial", 11);

                            double yPosition = 30;

                            // Title
                            gfx.DrawString($"{fullName} - {recordNo}", titleFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 20;

                            // Personal Information Section
                            gfx.DrawString("Personal Information", sectionFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 5;
                            gfx.DrawLine(XPens.Black, 20, yPosition, 550, yPosition); // Line under section
                            yPosition += 10; // Space after the line
                            gfx.DrawString($"Full Name: {fullName}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString($"Gender: {gender}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString($"Date of Birth: {dateOfBirth}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 25; // Add space after section

                            // Contact Details Section
                            gfx.DrawString("Contact Details", sectionFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 5;
                            gfx.DrawLine(XPens.Black, 20, yPosition, 550, yPosition); // Line under section
                            yPosition += 10; // Space after the line
                            gfx.DrawString($"Address: {address}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString($"Phone: {phone}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 25; // Add space after section

                            // Employment Details Section
                            gfx.DrawString("Employment Details", sectionFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 5;
                            gfx.DrawLine(XPens.Black, 20, yPosition, 550, yPosition); // Line under section
                            yPosition += 10; // Space after the line
                            gfx.DrawString($"Occupation: {occupation}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString($"Employer: {employer}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 25; // Add space after section

                            // Medical Details Section
                            gfx.DrawString("Medical Details", sectionFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 5;
                            gfx.DrawLine(XPens.Black, 20, yPosition, 550, yPosition); // Line under section
                            yPosition += 10; // Space after the line
                            gfx.DrawString($"Medical Aid: {medicalAid}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString($"Member No: {memberNo}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString($"Allergies: {allergies}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString($"Special Features: {specialFeatures}", contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 25; // Add space after section

                            // Notes Section
                            gfx.DrawString("Notes", sectionFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 5;
                            gfx.DrawLine(XPens.Black, 20, yPosition, 550, yPosition); // Line under section
                            yPosition += 20; // Space after the line

                            // Fetch and display the notes
                            string[] noteLines = notes.Split('\n');
                            foreach (string note in noteLines)
                            {
                                gfx.DrawString(note, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                                yPosition += 15;
                            }

                            // Save the PDF
                            pdf.Save(exportPath);
                            MessageBox.Show($"Exported to {exportPath}", "Export Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }





        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            recordNo = null;
        }
    }
}
