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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using miniKIFIR;
using System.Data;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace miniKIFIR
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Students> studentsList = new ObservableCollection<Students>();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV fájl(*.csv)|*.csv|JSON fájl(.json)|*.json";
            ofd.Title = "Importálás";
            if (ofd.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(ofd.FileName);
                ObservableCollection<Students> studentsList = new ObservableCollection<Students>();
                if (System.IO.Path.GetExtension(ofd.FileName).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    var options = new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true
                    };

                    foreach (string line in lines)
                    {
                         Students student = JsonSerializer.Deserialize<Students>(line, options);
                         studentsList.Add(student);
                    }
                }
                else if (System.IO.Path.GetExtension(ofd.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string line = lines[i];
                        string[] studentArray = line.Split(";");
                        if (studentArray.Length == 7)
                        {
                            Students student = new Students(
                                studentArray[0],
                                studentArray[1],
                                studentArray[2],
                                DateTime.Parse(studentArray[3]),
                                studentArray[4],
                                int.Parse(studentArray[5]),
                                int.Parse(studentArray[6])
                            );
                            studentsList.Add(student);
                        }

                    }
                }
                if (dgFelvetelizok.ItemsSource != null)
                {
                    var addResult = MessageBox.Show("A már betöltött adatok mögé szeretné fűzni?", "Importálás", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (addResult == MessageBoxResult.Yes)
                    {
                        ObservableCollection<Students> existingList = (ObservableCollection<Students>)dgFelvetelizok.ItemsSource;

                        foreach (var student in studentsList)
                        {
                            existingList.Add(student);
                        }
                    }
                    else if (addResult == MessageBoxResult.No)
                    {
                        dgFelvetelizok.ItemsSource = studentsList;
                    }
                }
                else
                {
                    dgFelvetelizok.ItemsSource = studentsList;
                }
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV fájl(*.csv)|*.csv|JSON fájl(.json)|*.json";
            sfd.Title = "Exportálás";
            ObservableCollection<Students> studentsList = (ObservableCollection<Students>)dgFelvetelizok.ItemsSource;
            if (dgFelvetelizok.Items.Count > 0)
            {
                sfd.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nincs betöltött adat!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (sfd.FileName != "" && dgFelvetelizok.Items.Count > 0)
            {
                if (System.IO.Path.GetExtension(sfd.FileName).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    var options = new JsonSerializerOptions();
                    options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    options.WriteIndented = true;


                    foreach (Students student in studentsList)
                    {
                        string line = JsonSerializer.Serialize(student, options);
                        File.AppendAllText(sfd.FileName, line + "\n");
                    }
                }
                else if (System.IO.Path.GetExtension(sfd.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    string header = "azonosito;nev;email;szuletesiDatum;ertesitesiCim;matekPont;magyarPont";
                    File.WriteAllText(sfd.FileName, header + "\n");
                    foreach (Students student in studentsList)
                    {
                        string line = $"{student.OM_Azonosito};{student.Neve};{student.Email};{student.SzuletesiDatum};{student.ErtesitesiCime};{student.Matematika};{student.Magyar}";
                        File.AppendAllText(sfd.FileName, line + "\n");
                    }
                }

            }
        }

        private void btnTorol_Click(object sender, RoutedEventArgs e)
        {
            if (dgFelvetelizok.SelectedItems.Count > 0)
            {
                ObservableCollection<Students> studentsList = (ObservableCollection<Students>)dgFelvetelizok.ItemsSource;
                List<Students> selectedStudents = new List<Students>(dgFelvetelizok.SelectedItems.Cast<Students>());

                foreach (Students selectedStudent in selectedStudents)
                {
                    studentsList.Remove(selectedStudent);
                }

                dgFelvetelizok.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Válassz ki egy vagy több diákot!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {
            NewStudentWindow newStudent = new NewStudentWindow(true);
            newStudent.ShowDialog();

            if (newStudent.StudentCreated)
            {
                newStudent.Close();
            }
            else if (newStudent.ChangesSaved)
            {
                dgFelvetelizok.Items.Refresh();
            }
        }

        private void windowCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnModositas_Click(object sender, RoutedEventArgs e)
        {
            if (dgFelvetelizok.SelectedItem != null)
            {
                Students selectedStudent = (Students)dgFelvetelizok.SelectedItem;
                NewStudentWindow editStudent = new NewStudentWindow(false);
                editStudent.SetSelectedStudent(selectedStudent);

                editStudent.ShowDialog();

                if (editStudent.ChangesSaved)
                {
                    dgFelvetelizok.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Válassz ki egy diákot a szerkesztéshez!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}