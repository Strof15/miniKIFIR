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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace miniKIFIR
{
    public partial class ProgressWindow : Window
    {
        public ProgressWindow()
        {
            InitializeComponent();
            myProgressBar.Loaded += ProgressBar_Loaded;
        }

        private void ProgressBar_Loaded(object sender, RoutedEventArgs e)
        {
            StartProgressBarAnimation();
        }

        private void ProgressBarAnimationCompleted(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartProgressBarAnimation()
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 100,
                Duration = TimeSpan.FromSeconds(3)
            };

            animation.Completed += ProgressBarAnimationCompleted;

            myProgressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
        }

       
    }
}
