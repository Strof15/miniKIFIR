using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace miniKIFIR
{
    public partial class StudentModifyWindow : Window
    {
        private Students editedStudent;
        public StudentModifyWindow(Students student)
        {
            InitializeComponent();
            editedStudent = student;
            txtOmAzonosito.Text = editedStudent.OM_Azonosito;
        }

        public bool ChangesSaved { get; private set; } = false;

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            editedStudent.OM_Azonosito = txtOmAzonosito.Text;
            ChangesSaved = true;
            Close();
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

