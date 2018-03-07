using Entities;
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

namespace CybellesCykler
{
    /// <summary>
    /// Interaction logic for AddAndUpdate.xaml
    /// </summary>
    public partial class AddOrUpdateBike : Window
    {
        private Bike bike;

        private bool isCancelled = true;

        public bool IsCancelled
        {
            get { return isCancelled; }
            set { isCancelled = value; }
        }



        public AddOrUpdateBike(Bike bike)
        {
            InitializeComponent();
            this.bike = bike;
            DataContext = bike;

            switch (bike.Kind)
            {
                case Bikekind.Mountain:
                    BikeType.SelectedItem = MountainType;
                    break;
                case Bikekind.City:
                    BikeType.SelectedItem = CityType;
                    break;
                case Bikekind.Tandem:
                    BikeType.SelectedItem = TandemType;
                    break;
                case Bikekind.Unicycle:
                    BikeType.SelectedItem = UnicycleType;
                    break;
                case Bikekind.PennyFarthing:
                    BikeType.SelectedItem = PennyFarthingType;
                    break;
                case Bikekind.PediCab:
                    BikeType.SelectedItem = PediCabType;
                    break;
                default:
                    break;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            IsCancelled = false;

            switch ((string)((ComboBoxItem)BikeType.SelectedItem).Content)
            {
                case "Mountain":
                    bike.Kind = Bikekind.Mountain;
                    break;
                case "By":
                    bike.Kind = Bikekind.City;
                    break;
                case "Tandem":
                    bike.Kind = Bikekind.Tandem;
                    break;
                case "Unicykel":
                    bike.Kind = Bikekind.Unicycle;
                    break;
                case "Cykeltaxi":
                    bike.Kind = Bikekind.PediCab;
                    break;
                case "Væltepeter":
                    bike.Kind = Bikekind.PennyFarthing;
                    break;
                default:
                    break;
            }

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            IsCancelled = true;
            Close();
        }

    }
}
