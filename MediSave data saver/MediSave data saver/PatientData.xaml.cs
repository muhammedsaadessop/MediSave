using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace MediSave_data_saver
{
    /// <summary>
    /// Interaction logic for PatientData.xaml
    /// </summary>
    public partial class PatientData : UserControl
    {
        public PatientData()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string dbPath = Path.Combine(documentsFolder, "MediSave", "AppData", "MediSaveDB.db");
                string connectionString = $"Data Source={dbPath}";


                using (SqliteConnection conn = new SqliteConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Check if the RecordNo already exists
                        string checkRecordQuery = "SELECT COUNT(*) FROM Patients WHERE RecordNo = @RecordNo";
                        using (SqliteCommand checkCmd = new SqliteCommand(checkRecordQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@RecordNo", txtRecordNo.Text.Trim());

                            int recordCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                            if (recordCount > 0)
                            {
                                MessageBox.Show("Record Number already exists. Please use a unique Record Number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;  // Exit the method without saving the data
                            }
                        }
                        string formattedNotes = $"Date noted: {DateTime.Now:yyyy-MM-dd}\n{txtNotes.Text.Trim()}\nEnd note";

                        // Insert Patient Data
                        string patientQuery = @"
        INSERT INTO Patients 
        (RecordNo, FullName, Address, Gender, DateOfBirth, TelNo, MedicalAid, MemberNo, SpecialFeatures, Employer, Occupation, Allergies, Notes)
        VALUES 
        (@RecordNo, @FullName, @Address, @Gender, @DateOfBirth, @TelNo, @MedicalAid, @MemberNo, @SpecialFeatures, @Employer, @Occupation, @Allergies, @Notes)";

                        using (SqliteCommand cmd = new SqliteCommand(patientQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@RecordNo", txtRecordNo.Text.Trim());
                            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                            cmd.Parameters.AddWithValue("@Gender", (cmbGender.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "");

                            cmd.Parameters.AddWithValue("@DateOfBirth", dpDateOfBirth.SelectedDate?.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@TelNo", txtPhoneNumber.Text.Trim());
                            cmd.Parameters.AddWithValue("@MedicalAid", txtMedicalAid.Text.Trim());
                            cmd.Parameters.AddWithValue("@MemberNo", txtMemberNo.Text.Trim());
                            cmd.Parameters.AddWithValue("@SpecialFeatures", txtSpecialFeatures.Text.Trim());
                            cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text.Trim());
                            cmd.Parameters.AddWithValue("@Occupation", txtOccupation.Text.Trim());
                            cmd.Parameters.AddWithValue("@Allergies", txtAllergies.Text.Trim());
                            cmd.Parameters.AddWithValue("@Notes", formattedNotes);

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Patient data saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }



        private bool ValidateInputs()
        {
            // Validate RecordNo
            if (string.IsNullOrWhiteSpace(txtRecordNo.Text))
            {
                MessageBox.Show("Record Number is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Full Name
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Address
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Address is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Gender
            if (cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Date of Birth
            if (!dpDateOfBirth.SelectedDate.HasValue)
            {
                MessageBox.Show("Date of Birth is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Phone Number
            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text) || !Regex.IsMatch(txtPhoneNumber.Text.Trim(), @"^\d{10}$"))
            {
                MessageBox.Show("Invalid Phone Number. Must be 10 digits.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Medical Aid
            if (string.IsNullOrWhiteSpace(txtMedicalAid.Text))
            {
                MessageBox.Show("Medical Aid is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Member Number
            if (string.IsNullOrWhiteSpace(txtMemberNo.Text))
            {
                MessageBox.Show("Member Number is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Special Features
            if (string.IsNullOrWhiteSpace(txtSpecialFeatures.Text))
            {
                MessageBox.Show("Special Features are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Employer
            if (string.IsNullOrWhiteSpace(txtEmployer.Text))
            {
                MessageBox.Show("Employer is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Occupation
            if (string.IsNullOrWhiteSpace(txtOccupation.Text))
            {
                MessageBox.Show("Occupation is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Allergies
            if (string.IsNullOrWhiteSpace(txtAllergies.Text))
            {
                MessageBox.Show("Allergies are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate Notes
            if (string.IsNullOrWhiteSpace(txtNotes.Text))
            {
                MessageBox.Show("Notes are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow)?.ShowMenu();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string dbPath = Path.Combine(documentsFolder, "MediSave", "AppData", "MediSaveDB.db");
            string connectionString = $"Data Source={dbPath}";

            string exportFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "patients_export.pdf");

            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Id, FullName, Address, TelNo, Gender, DateOfBirth, MedicalAid, RecordNo, Occupation, Employer, Allergies, SpecialFeatures, Notes FROM Patients";

                    using (var command = new SqliteCommand(query, conn))
                    using (var reader = command.ExecuteReader())
                    {
                        // Create the PDF document
                        PdfDocument pdfDoc = new PdfDocument();
                        PdfPage page = pdfDoc.AddPage();
                        XGraphics gfx = XGraphics.FromPdfPage(page);

                        // Define fonts
                        XFont titleFont = new XFont("Arial", 12, XFontStyleEx.Bold); // Title font
                        XFont contentFont = new XFont("Arial", 10); // Content font

                        double yPosition = 20; // Initial Y position to start drawing

                        while (reader.Read())
                        {
                            // Patient data
                            string fullName = reader["FullName"].ToString();
                            string address = reader["Address"].ToString();
                            string telephone = reader["TelNo"].ToString();
                            string sex = reader["Gender"].ToString();
                            string dateOfBirth = reader["DateOfBirth"].ToString();
                            string medicalAid = reader["MedicalAid"].ToString();
                            string recordNumber = reader["RecordNo"].ToString();
                            string occupation = reader["Occupation"].ToString();
                            string employer = reader["Employer"].ToString();
                            string allergies = reader["Allergies"].ToString();
                            string specialFeatures = reader["SpecialFeatures"].ToString();
                            string notes = reader["Notes"].ToString();

                            // Print patient data
                            gfx.DrawString("Patient Data", titleFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 20;

                            gfx.DrawString("Full Name: " + fullName, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString("Address: " + address, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString("Telephone: " + telephone, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString("Gender: " + sex, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString("Date of Birth: " + dateOfBirth, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString("Medical Aid: " + medicalAid, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString("Record Number: " + recordNumber, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString("Occupation: " + occupation, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString("Employer: " + employer, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString("Allergies: " + allergies, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;
                            gfx.DrawString("Special Features: " + specialFeatures, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 20;

                            // Notes section (always at the bottom)
                            gfx.DrawString("Notes:", titleFont, XBrushes.Black, new XPoint(20, yPosition));
                            yPosition += 15;

                            // Split the notes into lines based on new lines
                            string[] noteLines = notes.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                            // Print each note line
                            foreach (var line in noteLines)
                            {
                                gfx.DrawString(line, contentFont, XBrushes.Black, new XPoint(20, yPosition));
                                yPosition += 12; // Adjust line height
                            }

                            yPosition += 20; // Leave some space at the bottom for the next entry

                            // Check if we need to add a new page
                            if (yPosition > page.Height - 100) // If it's near the bottom of the page
                            {
                                page = pdfDoc.AddPage(); // Add a new page
                                gfx = XGraphics.FromPdfPage(page); // Get graphics object for the new page
                                yPosition = 20; // Reset Y position
                            }
                        }

                        // Save the PDF document
                        pdfDoc.Save(exportFilePath);

                        MessageBox.Show($"Data exported successfully to {exportFilePath}", "Export Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting data: " + ex.Message, "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
