using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DBHandler : CommonDB
    {
        public DBHandler(string connectionString) : base(connectionString)
        {
            //Should be empty
        }

        public Rentee GetRentee(int id)
        {
            throw new NotImplementedException();
        }
        public Rentee GetOrder(int id)
        {
            throw new NotImplementedException();
        }
        public Rentee GetBike(int id)
        {
            throw new NotImplementedException();
        }

        public bool NewRentee(Rentee rentee)
        {
            throw new NotImplementedException();
        }

        public bool NewOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool NewBike(Bike bike)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRentee(Rentee rentee)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBike(Bike bike)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRentee(Rentee rentee)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBike(Bike bike)
        {
            throw new NotImplementedException();
        }

        public List<Bike> GetAllBikes()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public List<Rentee> GetAllRentees()
        {
            throw new NotImplementedException();
        }
    }
}
