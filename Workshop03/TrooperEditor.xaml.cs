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

namespace Workshop03
{
    /// <summary>
    /// Interaction logic for TrooperEditor.xaml
    /// </summary>
    public partial class TrooperEditor : Window
    {
        private Troop trooper;
        public TrooperEditor(Troop trooper)
        {
            this.trooper = trooper;
            InitializeComponent();
            tbox_power.Text = trooper.Strength.ToString();
            tbox_vitality.Text = trooper.Vitality.ToString();
            tbox_cost.Text = trooper.Cost.ToString();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            trooper.Strength = int.Parse(tbox_power.Text);
            trooper.Vitality = int.Parse(tbox_vitality.Text);
            trooper.Cost = int.Parse(tbox_cost.Text);

            Close();
        }
    }
}
