using System.Data.SqlTypes;
using System.Data.SqlClient;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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
            SqlCommand command = new SqlCommand("SELECT * FROM Renters WHERE ID=@id");
            command.Parameters.AddWithValue("@id", id);

            DataSet set = ExecuteQuery(command, QueryType.Query);
            DataRow row = set.Tables[0].Rows[0];

            string name = (string)row["Name"];
            string phonenumber = (string)row["Phonenumber"];
            string address = (string)row["PhysAddress"];
            DateTime registerDate = (DateTime)row["RegisterDate"];

            return new Rentee(name, address, phonenumber, registerDate, id);

        }
        public Order GetOrder(int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE ID=@id");
            command.Parameters.AddWithValue("@id", id);

            DataSet set = ExecuteQuery(command, QueryType.Query);
            DataRow row = set.Tables[0].Rows[0];

            DateTime deliveryDate = (DateTime)row["DeliveryDate"];
            DateTime OrderDate = (DateTime)row["OrderDate"];

            Bike bike = GetBike((int)row["BikeID"]);
            Rentee rentee = GetRentee((int)row["RenteeID"]);

            return new Order(bike, rentee, OrderDate, deliveryDate, id);
        }
        public Bike GetBike(int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Bikes WHERE ID=@id");
            command.Parameters.AddWithValue("@id", id);

            DataSet set = ExecuteQuery(command, QueryType.Query);
            DataRow row = set.Tables[0].Rows[0];

            string bikeDescription = (string)row["BikeDescription"];
            decimal pricePerDay = (decimal)row["PricePerDay"];
            Bikekind kind = (Bikekind)row["BikeKind"];

            return new Bike(pricePerDay, bikeDescription, kind, id);
        }

        public List<Bike> GetAllBikes()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Bikes");

            List<Bike> bikes = new List<Bike>();

            DataSet set = ExecuteQuery(command, QueryType.Query);

            foreach (DataRow row in set.Tables[0].Rows)
            {
                string bikeDescription = (string)row["BikeDescription"];
                decimal pricePerDay = (decimal)row["PricePerDay"];
                Bikekind kind = (Bikekind)row["BikeKind"];
                int id = (int)row["ID"];
                bikes.Add(new Bike(pricePerDay, bikeDescription, kind, id));
            }

            return bikes;
        }

        public List<Order> GetAllOrders()
        {
            List<Bike> bikes = GetAllBikes();
            List<Rentee> renters = GetAllRentees();

            SqlCommand command = new SqlCommand("SELECT * FROM Orders");

            List<Order> orders = new List<Order>();

            DataSet set = ExecuteQuery(command, QueryType.Query);

            foreach (DataRow row in set.Tables[0].Rows)
            {
                DateTime deliveryDate = (DateTime)row["DeliveryDate"];
                DateTime OrderDate = (DateTime)row["OrderDate"];
                int id = (int)row["ID"];
                int bikeID = (int)row["BikeID"];
                int renteeID = (int)row["RenteeID"];

                Bike bike = bikes.Where(x => x.ID == bikeID).First();
                Rentee rentee = renters.Where(x => x.ID == renteeID).First();

                orders.Add(new Order(bike, rentee, OrderDate, deliveryDate, id));
            }

            return orders;
        }

        public List<Rentee> GetAllRentees()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Renters");

            List<Rentee> renters = new List<Rentee>();

            DataSet set = ExecuteQuery(command, QueryType.Query);

            foreach (DataRow row in set.Tables[0].Rows)
            {
                string name = (string)row["Name"];
                string phonenumber = (string)row["Phonenumber"];
                string address = (string)row["PhysAddress"];
                DateTime registerDate = (DateTime)row["RegisterDate"];
                int id = (int)row["ID"];

                renters.Add(new Rentee(name, address, phonenumber, registerDate, id));
            }

            return renters;
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

    }
}
