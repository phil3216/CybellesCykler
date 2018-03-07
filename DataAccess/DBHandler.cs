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
            SqlCommand command = new SqlCommand("INSERT INTO Renters ([Name],Phonenumber,PhysAddress,RegisterDate) VALUES(@name,@phonenumber,@physAddress,@registerDate)");
            command.Parameters.AddWithValue("@name",rentee.Name);
            command.Parameters.AddWithValue("@phonenumber",rentee.Phonenumber);
            command.Parameters.AddWithValue("@physAddress",rentee.Address);
            command.Parameters.AddWithValue("@registerDate",rentee.RegisterDate);

            return ExecuteNonQuery(command);
        }

        public bool NewOrder(Order order)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Orders (BikeID,DeliveryDate,OrderDate,RenteeID) VALUES(@bikeID,@deliveryDate,@orderDate,@renteeID)");
            command.Parameters.AddWithValue("@bikeID", order.Bike.ID);
            command.Parameters.AddWithValue("@deliveryDate", order.DeliveryDate);
            command.Parameters.AddWithValue("@orderDate", order.RentDate);
            command.Parameters.AddWithValue("@renteeID", order.Rentee.ID);

            return ExecuteNonQuery(command);
        }

        public bool NewBike(Bike bike)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Bikes (BikeDescription,PricePerDay,BikeKind) VALUES(@bikeDescription,@pricePerDay,@bikeKind)");
            command.Parameters.AddWithValue("@bikeDescription", bike.Description);
            command.Parameters.AddWithValue("@pricePerDay", bike.PricePerDay);
            command.Parameters.AddWithValue("@bikeKind", bike.Kind);

            return ExecuteNonQuery(command);
        }

        public bool UpdateRentee(Rentee rentee)
        {
            SqlCommand command = new SqlCommand("UPDATE Renters SET [Name]=@name,Phonenumber=@phonenumber,PhysAddress=@address,RegisterDate=@registerDate WHERE ID=@id");
            command.Parameters.AddWithValue("@id", rentee.ID);
            command.Parameters.AddWithValue("@name", rentee.Name);
            command.Parameters.AddWithValue("@phonenumber", rentee.Phonenumber);
            command.Parameters.AddWithValue("@address", rentee.Address);
            command.Parameters.AddWithValue("@registerDate", rentee.RegisterDate);

            return ExecuteNonQuery(command);
        }

        public bool UpdateOrder(Order order)
        {
            SqlCommand command = new SqlCommand("UPDATE Orders SET BikeID=@bikeID,DeliveryDate=@deliveryDate,OrderDate=@orderDate,RenteeID=@renteeID WHERE ID=@id");
            command.Parameters.AddWithValue("@id", order.ID);
            command.Parameters.AddWithValue("@bikeID",order.Bike.ID);
            command.Parameters.AddWithValue("@deliveryDate", order.DeliveryDate);
            command.Parameters.AddWithValue("@orderDate", order.RentDate);
            command.Parameters.AddWithValue("@renteeID", order.Rentee.ID);

            return ExecuteNonQuery(command);
        }

        public bool UpdateBike(Bike bike)
        {
            SqlCommand command = new SqlCommand("UPDATE Bikes SET BikeDescription=@bikeDescription,PricePerDay=@pricePerDay,BikeKind=@bikeKind WHERE ID=@id");
            command.Parameters.AddWithValue("@id", bike.ID);
            command.Parameters.AddWithValue("@bikeDescription", bike.Description);
            command.Parameters.AddWithValue("@pricePerDay", bike.PricePerDay);
            command.Parameters.AddWithValue("@bikeKind", bike.Kind);

            return ExecuteNonQuery(command);

        }

        public bool DeleteRentee(Rentee rentee)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Orders WHERE RenteeID=@id;" +
                                                "DELETE FROM Renters WHERE ID=@id;");
            command.Parameters.AddWithValue("@id", rentee.ID);

            return ExecuteNonQuery(command);
        }

        public bool DeleteOrder(Order order)
        {

            SqlCommand command = new SqlCommand("DELETE FROM Orders WHERE ID=@id;");
            command.Parameters.AddWithValue("@id",order.ID);

            return ExecuteNonQuery(command);

        }

        public bool DeleteBike(Bike bike)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Orders WHERE BikeID=@id;" +
                                                "DELETE FROM Bikes WHERE ID=@id");
            command.Parameters.AddWithValue("@id", bike.ID);

            return ExecuteNonQuery(command);
        }

    }
}
