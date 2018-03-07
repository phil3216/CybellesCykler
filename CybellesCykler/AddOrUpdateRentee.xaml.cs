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
    /// Interaction logic for AddOrUpdateRentee.xaml
    /// </summary>
    public partial class AddOrUpdateRentee : Window
    {
        Rentee rentee;

        private bool isCancelled = true;

        public bool IsCancelled
        {
            get { return isCancelled; }
            set { isCancelled = value; }
        }


        public AddOrUpdateRentee(Rentee rentee)
        {
            InitializeComponent();
            this.rentee = rentee;
            DataContext = rentee;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
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
