using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class Order : INotifyPropertyChanged, IPersistable, IEquatable<Order>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected Bike bike;
        protected int id;
        protected Rentee rentee;
        protected DateTime rentDate;
        protected DateTime deliveryDate;

        public Bike Bike
        {
            get => bike;
            set
            {
                (bool b, string s) = ValidateBike(value);
                if (!b) throw new ArgumentException(s);

                bike = value;
                OnPropertyChanged();
            }
        }



        public int ID
        {
            get => id;
            set
            {
                (bool b, string s) = ValidateID(value);
                if (!b) throw new ArgumentException(s);

                id = value;
                OnPropertyChanged();
            }
        }




        public Rentee Rentee
        {
            get => rentee;
            set
            {
                (bool b, string s) = ValidateRentee(value);
                if (!b) throw new ArgumentException(s);

                rentee = value;
                OnPropertyChanged();
            }
        }



        public DateTime RentDate
        {
            get => rentDate;
            set
            {
                (bool b, string s) = ValidateRentDate(value);
                if (!b) throw new ArgumentException(s);

                rentDate = value;
                OnPropertyChanged();
            }
        }



        public DateTime DeliveryDate
        {
            get => deliveryDate;
            set
            {
                (bool b, string s) = ValidateDeliveryDate(value);
                if (!b) throw new ArgumentException(s);

                deliveryDate = value;
                OnPropertyChanged();
            }
        }

        public static (bool, string) ValidateBike(Bike value)
        {
            if (value == null)
                return (false, "Bike cannot be null");

            return (true, String.Empty);
        }

        public static (bool, string) ValidateID(int value)
        {
            if (value < 1)
                return (false, "ID cannot be less than 1");

            return (true, String.Empty);
        }

        public static (bool, string) ValidateRentee(Rentee value)
        {
            if (value == null)
                return (false, "Rentee cannot be null");

            return (true, String.Empty);
        }

        public static (bool, string) ValidateRentDate(DateTime value)
        {
            return (true, String.Empty);
        }

        public static (bool, string) ValidateDeliveryDate(DateTime value)
        {
            return (true, String.Empty);
        }

        public decimal GetPrice()
        {
            throw new NotImplementedException();
        }

        public Order(Bike bike,Rentee rentee,DateTime rentDate,DateTime deliveryDate,int id)
        {
            Bike = bike;
            Rentee = rentee;
            RentDate = rentDate;
            DeliveryDate = deliveryDate;
            ID = id;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string s = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

        public override bool Equals(object obj)
        {
            return Equals(obj as Order);
        }

        public bool Equals(Order other)
        {
            return other != null &&
                   EqualityComparer<Bike>.Default.Equals(Bike, other.Bike) &&
                   ID == other.ID &&
                   EqualityComparer<Rentee>.Default.Equals(Rentee, other.Rentee) &&
                   RentDate == other.RentDate &&
                   DeliveryDate == other.DeliveryDate;
        }

        public override int GetHashCode()
        {
            var hashCode = -1740589705;
            hashCode = hashCode * -1521134295 + EqualityComparer<Bike>.Default.GetHashCode(Bike);
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Rentee>.Default.GetHashCode(Rentee);
            hashCode = hashCode * -1521134295 + RentDate.GetHashCode();
            hashCode = hashCode * -1521134295 + DeliveryDate.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Order order1, Order order2)
        {
            return EqualityComparer<Order>.Default.Equals(order1, order2);
        }

        public static bool operator !=(Order order1, Order order2)
        {
            return !(order1 == order2);
        }
        public override string ToString()
        {
            return $"Bike: {Bike},Rentee: {Rentee},RentDate: {RentDate},DeliveryDate: {DeliveryDate},ID: {ID}";
        }
    }
}
