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
    /// Interaction logic for AddOrUpdateOrder.xaml
    /// </summary>
    public partial class AddOrUpdateOrder : Window
    {
        Order order;

        private bool isCancelled = true;

        public bool IsCancelled
        {
            get { return isCancelled; }
            set { isCancelled = value; }
        }

        public AddOrUpdateOrder(Order order,List<Rentee> renters,List<Bike> bikes)
        {
            InitializeComponent();
            this.order = order;
            DataContext = order;

            Bikes.ItemsSource = bikes;
            Bikes.SelectedItem = order.Bike;

            Renters.ItemsSource = renters;
            Renters.SelectedItem = order.Rentee;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            order.Bike = (Bike)Bikes.SelectedItem;
            order.Rentee = (Rentee)Renters.SelectedItem;
            IsCancelled = false;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            IsCancelled = true;
            Close();
        }

    }
}
