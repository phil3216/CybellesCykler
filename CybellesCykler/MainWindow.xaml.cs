using Entities;
using Business;
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

namespace CybellesCykler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataController dataController;

        public MainWindow()
        {
            InitializeComponent();
            dataController = new DataController(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Cycling;Integrated Security=True");
        }

        private void BtnShowRentees_Click(object sender, RoutedEventArgs e)
        {
            dataController.ReloadEntities<Rentee>();
            BindingOperations.SetBinding(DtgSelected, DataGrid.ItemsSourceProperty, new Binding("Renters") { Source = dataController });
        }

        private void BtnShowBikes_Click(object sender, RoutedEventArgs e)
        {
            dataController.ReloadEntities<Bike>();
            BindingOperations.SetBinding(DtgSelected, DataGrid.ItemsSourceProperty, new Binding("Bikes") { Source = dataController});
        }

        private void BtnShowOrders_Click(object sender, RoutedEventArgs e)
        {
            Orders orders = new Orders(dataController);
            orders.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }
    }
}
