using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_Uppgift_3
{
    class MultiLineTextBox : TextBox
    {
        public MultiLineTextBox()
        {
            FontSize = 15.5;
            Background = Brushes.Black;
            Foreground = Brushes.LightGreen;
            FontFamily = new FontFamily("Courier New");
            TextWrapping = System.Windows.TextWrapping.Wrap;
        }
    }
}
