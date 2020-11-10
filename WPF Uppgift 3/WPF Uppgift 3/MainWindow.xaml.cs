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

namespace WPF_Uppgift_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            transbutton.Click += translate;

        }

        private void translate(object sender, RoutedEventArgs e)
        {
            textbox.Text = textbox.Text.ToLower().Replace("u", "ö");
            textbox.Text = textbox.Text.ToLower().Replace("o", "u");
            textbox.Text = textbox.Text.ToLower().Replace("s", "sh");
            textbox.Text = textbox.Text.ToLower().Replace("r", "rrr");
            textbox.Text = textbox.Text.ToLower().Replace("t", "ta");
            textbox.Text = textbox.Text.ToLower().Replace(".", ", arrrrr!");

        }
    }
}
