using Business;
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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        DataController dataController;

        public Orders(DataController controller)
        {
            InitializeComponent();
            dataController = controller;
            DataContext = dataController;
            dataController.ReloadEntities<Order>();
        }

        private void OrdersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedOrder.DataContext = OrdersList.SelectedItem;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.SelectedItem == null)
            {
                MessageBox.Show("Du skal vælge en ordre for at kunne slette den");
                return;
            }

            dataController.DeleteEntity((IPersistable)OrdersList.SelectedItem);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            dataController.ReloadEntities<Bike>();
            dataController.ReloadEntities<Rentee>();
            List<Bike> bikes = dataController.Bikes.ToList();
            List<Rentee> renters = dataController.Renters.ToList();
            Order order = new Order(bikes[0], renters[0], DateTime.Now, DateTime.Now);
            AddOrUpdateOrder add = new AddOrUpdateOrder(order, renters, bikes);
            add.Title = "Tilføj";
            add.ShowDialog();

            if (!add.IsCancelled)
            {
                dataController.NewEntity(order);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            dataController.ReloadEntities<Bike>();
            dataController.ReloadEntities<Rentee>();

            if (OrdersList.SelectedItem == null)
            {
                MessageBox.Show("Du skal vælge en ordre for at kunne redigere den");
                return;
            }

            Order order = (Order)OrdersList.SelectedItem;
            order = new Order(order.Bike, order.Rentee, order.RentDate, order.DeliveryDate,order.ID);
            AddOrUpdateOrder add = new AddOrUpdateOrder(order, dataController.Renters.ToList(),dataController.Bikes.ToList());
            add.Title = "Rediger";
            add.ShowDialog();

            if (!add.IsCancelled)
            {
                dataController.UpdateEntity(order);
            }
        }
    }
}
