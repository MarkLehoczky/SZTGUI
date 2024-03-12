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

namespace Labor05
{
    /// <summary>
    /// Interaction logic for AthleteWindow.xaml
    /// </summary>
    public partial class AthleteWindow : Window
    {
        public AthleteWindow(Athlete athlete)
        {
            InitializeComponent();

            DataContext = athlete;
        }
    }
}
