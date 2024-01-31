using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace miniKIFIR
{
    /// <summary>
    /// Interaction logic for DatabaseWindow.xaml
    /// </summary>
    public partial class DatabaseWindow : Window
    {

        ProgressWindow Progwindow = new ProgressWindow();
        MainWindow mainWindow = new MainWindow();
        public DatabaseWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            DefaultDatas();
        }
        private void DefaultDatas()
        {
            txtRoute.Text = "127.0.0.1";
            txtPort.Text = "3306";
            txtUsername.Text = "root";
            txtDatabaseName.Text = "minikifir";
        }
        public void importTable()
        {

            string connectionString = $"datasource={txtRoute.Text};port={txtPort.Text};username={txtUsername.Text};password=;database={txtDatabaseName.Text};";
            MySqlConnection connection;
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                string queryText = "SELECT * FROM felvetelizok";
                MySqlCommand command = new MySqlCommand(queryText, connection);
                MySqlDataReader reader = command.ExecuteReader();

                mainWindow.studentsList.Clear();

                while (reader.Read())
                {
                    string omAzonosito = reader.GetString(0);
                    string nev = reader.GetString(1);
                    string email = reader.GetString(2);
                    DateTime szuletes = reader.GetDateTime(3);
                    string ertesites = reader.GetString(4);
                    int matematika = reader.GetInt32(5);
                    int magyar = reader.GetInt32(6);
                    Students newStudent = new(omAzonosito, nev, email, szuletes, ertesites, matematika, magyar);
                    mainWindow.studentsList.Add(newStudent);
                }

                reader.Close();
                mainWindow.dgFelvetelizok.ItemsSource = null;
                mainWindow.dgFelvetelizok.ItemsSource = mainWindow.studentsList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void exportTable()
        {
            string connectionString = $"datasource={txtRoute.Text};port={txtPort.Text};username={txtUsername.Text};password=;database={txtDatabaseName.Text};";
            MySqlConnection connection;
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                string clearTableQuery = "DELETE FROM felvetelizok";
                MySqlCommand clearTableCommand = new MySqlCommand(clearTableQuery, connection);
                clearTableCommand.ExecuteNonQuery();

                string insertQuery = "INSERT INTO felvetelizok (azonosito, nev, email, szuletesiDatum, ertesitesiCim, matekPont, magyarPont) VALUES (@omAzonosito, @nev, @email, @szuletes, @ertesites, @matematika, @magyar)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                ObservableCollection<Students> existingList = (ObservableCollection<Students>)mainWindow.dgFelvetelizok.ItemsSource;
                foreach (var student in existingList)
                {
                    insertCommand.Parameters.Clear();
                    insertCommand.Parameters.AddWithValue("@omAzonosito", student.OM_Azonosito);
                    insertCommand.Parameters.AddWithValue("@nev", student.Neve);
                    insertCommand.Parameters.AddWithValue("@email", student.Email);
                    insertCommand.Parameters.AddWithValue("@szuletes", student.SzuletesiDatum);
                    insertCommand.Parameters.AddWithValue("@ertesites", student.ErtesitesiCime);
                    insertCommand.Parameters.AddWithValue("@matematika", student.Matematika);
                    insertCommand.Parameters.AddWithValue("@magyar", student.Magyar);
                    insertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void windowCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
            }
        }

        private void btnCsatlakozas_Click(object sender, RoutedEventArgs e)
        {





            if (cbEvent.SelectedItem == cbImport)
            {
                try
                {
                    Progwindow.ShowDialog();
                    importTable();
                    MessageBox.Show("Az importálás sikeres volt!", "Importálás", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Az importálás sikertelen volt! {ex.Message}", "Importálás", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (cbEvent.SelectedItem == cbExport && mainWindow.dgFelvetelizok.Items.Count > 0)
            {
                try
                {
                    exportTable();
                    MessageBox.Show("Az exportálás sikeres volt!", "Exportálás", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Az exportálás sikertelen volt! {ex.Message}", "Exportálás", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (cbEvent.SelectedItem == cbExport && mainWindow.dgFelvetelizok.Items.Count == 0)
            {
                MessageBox.Show($"Nincs betöltött adat!", "Exportálás", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
