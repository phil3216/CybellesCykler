using System.Configuration;
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
        bool AreBikesShown = false;

        public MainWindow()
        {
            InitializeComponent();
            
            dataController = new DataController(ConfigurationManager.ConnectionStrings["Standard"].ConnectionString);
            BindingOperations.SetBinding(DtgSelected, DataGrid.ItemsSourceProperty, new Binding("Renters") { Source = dataController });
            dataController.ReloadEntities<Rentee>();
        }

        private void BtnShowRentees_Click(object sender, RoutedEventArgs e)
        {
            dataController.ReloadEntities<Rentee>();
            BindingOperations.SetBinding(DtgSelected, DataGrid.ItemsSourceProperty, new Binding("Renters") { Source = dataController });
            AreBikesShown = false;
        }

        private void BtnShowBikes_Click(object sender, RoutedEventArgs e)
        {
            dataController.ReloadEntities<Bike>();
            BindingOperations.SetBinding(DtgSelected, DataGrid.ItemsSourceProperty, new Binding("Bikes") { Source = dataController });
            AreBikesShown = true;
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (AreBikesShown)
            {
                Bike bike = new Bike(0, " ", Bikekind.City);
                AddOrUpdateBike add = new AddOrUpdateBike(bike);
                add.Title = "Tilføj";
                add.ShowDialog();

                if (!add.IsCancelled)
                {
                    dataController.NewEntity(bike);
                }
            }
            else
            {
                Rentee rentee = new Rentee("Navn","Addresse","000-000-000",DateTime.Now);
                AddOrUpdateRentee add = new AddOrUpdateRentee(rentee);
                add.Title = "Tilføj";
                add.ShowDialog();

                if (!add.IsCancelled)
                {
                    dataController.NewEntity(rentee);
                }
            }

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (DtgSelected.SelectedItem == null)
            {
                MessageBox.Show("Du skal vælge et row for at ændre det");
                return;
            }
            else
            {
                if (AreBikesShown)
                {
                    Bike bike = (Bike)DtgSelected.SelectedCells[0].Item;
                    bike = new Bike(bike.PricePerDay,bike.Description,bike.Kind,bike.ID);
                    AddOrUpdateBike edit = new AddOrUpdateBike(bike);
                    edit.Title = "Rediger";
                    edit.ShowDialog();

                    if (!edit.IsCancelled)
                    {
                        dataController.UpdateEntity(bike);
                    }
                }
                else
                {
                    Rentee rentee = (Rentee)DtgSelected.SelectedCells[0].Item;
                    rentee = new Rentee(rentee.Name,rentee.Address,rentee.Phonenumber,rentee.RegisterDate,rentee.ID);
                    AddOrUpdateRentee edit = new AddOrUpdateRentee(rentee);
                    edit.Title = "Rediger";
                    edit.ShowDialog();

                    if (!edit.IsCancelled)
                    {
                        dataController.UpdateEntity(rentee);
                    }

                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DtgSelected.SelectedItem == null)
            {
                MessageBox.Show("Du skal vælge et row for at slette det");
                return;
            }
            else
            {

                if (MessageBox.Show($"Dette sletter også alle ordrer tilsluttet til denne " + (AreBikesShown ? "cykel" : "lejer") + ". Er du sikker på at du vil slette dem?", "Advarsel!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    dataController.DeleteEntity((IPersistable)DtgSelected.SelectedCells[0].Item);
                }

            }
        }

    }
}
