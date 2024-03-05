using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Workshop03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BindingList<Troop> selectedTroops;
        public static BindingList<Troop> troopsTypes;

        static MainWindow()
        {
            troopsTypes = new BindingList<Troop>()
            {
                new Troop("marine", 8, 6, 6),
                new Troop("pilot", 7, 3, 10),
                new Troop("infantry", 6, 8, 10),
                new Troop("sniper", 3, 3, 7),
                new Troop("engineer", 5, 6, 8),
            };
        }

        public MainWindow()
        {
            selectedTroops = new BindingList<Troop>();
            InitializeComponent();
            
            listbox_troop_types.ItemsSource = troopsTypes;
            listbox_troop_collection.ItemsSource = selectedTroops;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            if (listbox_troop_types.SelectedItem != null)
                selectedTroops.Add(listbox_troop_types.SelectedItem as Troop);

            label_total_cost.Content = selectedTroops.Sum(x => x.TotalCost);
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            if (listbox_troop_types.SelectedItem != null && selectedTroops.Contains(listbox_troop_types.SelectedItem))
                selectedTroops.Remove(listbox_troop_types.SelectedItem as Troop);

            label_total_cost.Content = selectedTroops.Sum(x => x.TotalCost);
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (listbox_troop_types.SelectedItem != null)
                new TrooperEditor(listbox_troop_types.SelectedItem as Troop).ShowDialog();
        }
    }

    public class Troop : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string name;
        private int strength;
        private int vitality;
        private int cost;

        public string Name { get => name; }
        public int Strength { get => strength; set { strength = value; OnChanged(); } }
        public int Vitality { get => vitality; set { vitality = value; OnChanged(); } }
        public int Cost { get => cost; set { cost = value; OnChanged(); } }
        public int TotalCost { get => strength * vitality * cost; }

        public Troop()
        {
            name = string.Empty;
            strength = 0;
            vitality = 0;
            cost = 0;
        }
        public Troop(string name, int strength, int vitality, int cost)
        {
            this.name = name;
            this.strength = strength;
            this.vitality = vitality;
            this.cost = cost;
        }

        public override string ToString()
        {
            return $"{name}    {strength}    {vitality}    {cost}";
        }

        private void OnChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ValueColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int number) return Brushes.Transparent;

            if (number >= 7) return Brushes.Green;
            else if (number >= 4) return Brushes.Yellow;
            else return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not SolidColorBrush brush) return -1;

            if (brush == Brushes.Green) return 3;
            else if (brush == Brushes.Yellow) return 2;
            else return 1;
        }
    }

    public class ValueWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int number) return Brushes.Transparent;

            return (number / 10.0) * 85;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int width) return -1;

            return (width * 10.0) / 85;
        }
    }
}
