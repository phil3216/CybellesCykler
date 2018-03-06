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
    }
}
