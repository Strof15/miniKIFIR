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
using System.IO;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace miniKIFIR
{
    public partial class NewStudentWindow : Window
    {
        private bool isNewStudentMode;

        public NewStudentWindow(bool isNewStudent = true)
        {
            InitializeComponent();
            isNewStudentMode = isNewStudent;
            if (!isNewStudentMode)
            {
                txtOmAzonosito.IsReadOnly = true;
                lblDiak.Content = "Diák módosítása";
                btnRogzit.Content = "Módosítás";
            }
            else
            {
                txtOmAzonosito.Text = "12345678911";
                txtNev.Text = "Ban Lac";
                txtEmail.Text = "valami@val.com";
                txtErtesitesiLakcim.Text = "debrecen";

            }
        }
        public bool StudentCreated { get; private set; } = false;

        public bool ChangesSaved { get; private set; } = false;

        private Students selectedStudent;
        public void SetSelectedStudent(Students student)
        {
            txtOmAzonosito.Text = student.OM_Azonosito;
            txtNev.Text = student.Neve;
            txtEmail.Text = student.Email;
            txtErtesitesiLakcim.Text = student.ErtesitesiCime;
            txtMatekPont.Text = student.Matematika.ToString();
            txtMagyarPont.Text = student.Magyar.ToString();
            selectedStudent = student;
        }
        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtOmAzonosito.Text.Length == 11 && txtOmAzonosito.Text.All(char.IsDigit))
                {
                    string[] nameParts = txtNev.Text.Split(' ');
                    if (nameParts.Length >= 2 && nameParts.All(part => !string.IsNullOrWhiteSpace(part) && char.IsUpper(part[0])))
                    {
                        if (!string.IsNullOrWhiteSpace(txtErtesitesiLakcim.Text))
                        {
                            if (txtEmail.Text.Count(c => c == '@') == 1 && !txtEmail.Text.Contains(" ") && txtEmail.Text.Split('@').All(part => !string.IsNullOrWhiteSpace(part)))
                            {
                                DateTime szuletesiDatum;
                                if (DateTime.TryParse(DtSzulIdo.Text, out szuletesiDatum))
                                {
                                    int matekPont, magyarPont;
                                    if (int.TryParse(txtMatekPont.Text, out matekPont) && int.TryParse(txtMagyarPont.Text, out magyarPont))
                                    {
                                        if (matekPont >= 0 && matekPont <= 50 && magyarPont >= 0 && magyarPont <= 50)
                                        {
                                            if (isNewStudentMode)
                                            {
                                                Students student = new Students(
                                                    txtOmAzonosito.Text,
                                                    txtNev.Text,
                                                    txtEmail.Text,
                                                    DateTime.Parse(DtSzulIdo.Text),
                                                    txtErtesitesiLakcim.Text,
                                                    matekPont,
                                                    magyarPont
                                                );

                                                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                                                if (mainWindow != null)
                                                {
                                                    mainWindow.studentsList.Add(student);

                                                    if (mainWindow.dgFelvetelizok.ItemsSource != null)
                                                    {
                                                        var addResult = MessageBox.Show("A már betöltött adatok mögé szeretné fűzni?", "Importálás", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                                        if (addResult == MessageBoxResult.Yes)
                                                        {
                                                            ObservableCollection<Students> existingList = (ObservableCollection<Students>)mainWindow.dgFelvetelizok.ItemsSource;
                                                            List<Students> studentsToAdd = new List<Students>();

                                                            foreach (var student2 in mainWindow.studentsList)
                                                            {
                                                                studentsToAdd.Add(student2);
                                                            }
      
                                                        }
                                                        else if (addResult == MessageBoxResult.No)
                                                        {
                                                            mainWindow.dgFelvetelizok.ItemsSource = mainWindow.studentsList;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mainWindow.dgFelvetelizok.ItemsSource = mainWindow.studentsList;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                selectedStudent.OM_Azonosito = txtOmAzonosito.Text;
                                                selectedStudent.Neve = txtNev.Text;
                                                selectedStudent.Email = txtEmail.Text;
                                                selectedStudent.SzuletesiDatum = DateTime.Parse(DtSzulIdo.Text);
                                                selectedStudent.ErtesitesiCime = txtErtesitesiLakcim.Text;
                                                selectedStudent.Matematika = matekPont;
                                                selectedStudent.Magyar = magyarPont;

                                                ChangesSaved = true;
                                            }
                                            StudentCreated = true;
                                            Close();
                                        }
                                        else
                                        {
                                            MessageBox.Show("A magyar és matematika pontoknak 0 és 50 között kell lennie!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                                            txtMatekPont.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("A pontoknak számoknak kell lenniük!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                                        txtMatekPont.Focus();
                                        txtMatekPont.Clear();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Hibás dátum formátum!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Hibás email cím!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                                txtEmail.Focus();
                                txtEmail.Clear();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Az értesítési cím nem lehet üres!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("A név mező hibásan lett megadva!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                        txtNev.Focus();
                        txtNev.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Hibás OM azonosító! A számsornak 11 karakter hosszúnak kell lennie és csak számokat tartalmazhat!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtOmAzonosito.Focus();
                    txtOmAzonosito.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
