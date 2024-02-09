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

namespace miniKIFIR
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        MainWindow newwindow = new MainWindow();
        private string fullText = "Üdvözöljük a miniKIFIR projektben!";
        private int currentIndex = 0;

        public LoadingWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await TypeTextAsync();
        }

        private async Task TypeTextAsync()
        {
            while (currentIndex < fullText.Length)
            {
                lblfelirat.Text += fullText[currentIndex];
                currentIndex++;
                await Task.Delay(100);
            }
            await Task.Delay(1000);
            this.Close();
            newwindow.Show();
        }
    }
}
