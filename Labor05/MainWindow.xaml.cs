using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Labor05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class RaceViewModel : ObservableRecipient
    {
        private IRaceLogic logic;
        private Athlete selectedFromRace;
        private Athlete selectedFromAthletes;

        public Athlete SelectedFromRace { get => selectedFromRace;
            set {
                SetProperty(ref selectedFromRace, value);
                (RemoveFromRaceCommand as RelayCommand).NotifyCanExecuteChanged();
            } }
        public Athlete SelectedFromAthletes { get => selectedFromAthletes;
            set {
                SetProperty(ref selectedFromAthletes, value);
                (AddToRaceCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public ObservableCollection<Athlete> athletes {  get; set; }
        public ObservableCollection<Athlete> race {  get; set; }


        public ICommand AddToRaceCommand { get; set; }
        public ICommand RemoveFromRaceCommand { get; set; }




        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public RaceViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IRaceLogic>()) { }

        public RaceViewModel(IRaceLogic logic)
        {
            this.logic = logic;
            race = new ObservableCollection<Athlete>();
            athletes =  new ObservableCollection<Athlete>();

            logic.SetupCollections(race, athletes);

            AddToRaceCommand = new RelayCommand(
                () => logic.AddToRace(selectedFromAthletes),
                () => selectedFromAthletes != null && selectedFromAthletes.HavePermission
                );

            RemoveFromRaceCommand = new RelayCommand(
                () => logic.RemoveFromRace(selectedFromRace),
                () => selectedFromRace != null
                );
        }

    }

    public interface IRaceLogic
    {
        void AddToRace(Athlete athlete);
        void RemoveFromRace(Athlete athlete);
        void SetupCollections(IList<Athlete> race, IList<Athlete> athletes);
    }

    public class RaceLogic : IRaceLogic
    {
        IList<Athlete> athletes;
        IList<Athlete> race;
        IMessenger messenger;

        public RaceLogic(IMessenger messenger)
        {
            race = new List<Athlete>();
            athletes = new List<Athlete>();
            this.messenger = messenger;
        }

        public void AddToRace(Athlete athlete)
        {
            race.Add(athlete);
            messenger.Send("Athlete added", "RaceInfo");
        }
        public void RemoveFromRace(Athlete athlete)
        {
            race.Remove(athlete);
            messenger.Send("Athlete removed", "RaceInfo");
        }

        public void SetupCollections(IList<Athlete> race, IList<Athlete> athletes)
        {
            this.race = race;
            this.athletes = athletes;
        }
    }

    public class Athlete : ObservableObject
    {
        private string name;
        private double personalRecord;
        private double seasonalRecord;
        private bool havePermission;
        private bool haveOrganization;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public double PersonalRecord { get => personalRecord; set => SetProperty(ref personalRecord, value); }
        public double SeasonalRecord { get => seasonalRecord; set => SetProperty(ref seasonalRecord, value); }
        public bool HavePermission { get => havePermission; set => SetProperty(ref havePermission, value); } 
        public bool HaveOrganization { get => haveOrganization; set => SetProperty(ref haveOrganization, value); }
        public double MarketValue { get => SeasonalRecord * PersonalRecord; }

    }
}
