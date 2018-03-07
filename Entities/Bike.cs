using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Entities
{
    public class Bike : INotifyPropertyChanged, IPersistable, IEquatable<Bike>
    {


        public event PropertyChangedEventHandler PropertyChanged;

        protected decimal pricePerDay;
        protected string description;
        protected int id;
        protected Bikekind kind;

        public decimal PricePerDay
        {
            get => pricePerDay;
            set
            {
                (bool b, string s) = ValidatePricePerDay(value);
                if (!b) throw new ArgumentException(s);

                pricePerDay = value;
                OnPropertyChanged();
            }
        }





        public string Description
        {
            get => description;
            set
            {
                (bool b, string s) = ValidateBikeDescription(value);
                if (!b) throw new ArgumentException(s);

                description = value;
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






        public Bikekind Kind
        {
            get => kind;
            set
            {
                (bool b, string s) = ValidateKind(value);
                if (!b) throw new ArgumentException(s);

                kind = value;
                OnPropertyChanged();
            }
        }

        public static (bool, string) ValidatePricePerDay(decimal value)
        {
            if (value < 0)
                return (false,"Price per day cannot be negative");

            return (true, String.Empty);
        }

        public static (bool, string) ValidateID(int value)
        {
            if (value < 1)
                return (false, "ID cannot be less than 1");

            return (true, String.Empty);
        }

        public static (bool, string) ValidateBikeDescription(string value)
        {
            return (true, String.Empty);
        }

        public static (bool, string) ValidateKind(Bikekind value)
        {
            return (true, String.Empty);
        }

        public Bike(decimal pricePerDay,string bikeDescription,Bikekind kind,int id) : this(pricePerDay,bikeDescription,kind)
        {
            ID = id;
        }

        public Bike(decimal pricePerDay, string bikeDescription, Bikekind kind)
        {
            PricePerDay = pricePerDay;
            Description = bikeDescription;
            Kind = kind;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string s = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

        public override bool Equals(object obj)
        {
            return Equals(obj as Bike);
        }

        public bool Equals(Bike other)
        {
            return other != null &&
                   PricePerDay == other.PricePerDay &&
                   Description == other.Description &&
                   ID == other.ID &&
                   Kind == other.Kind;
        }

        public override int GetHashCode()
        {
            var hashCode = 1574458350;
            hashCode = hashCode * -1521134295 + PricePerDay.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + Kind.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Bike bike1, Bike bike2)
        {
            return EqualityComparer<Bike>.Default.Equals(bike1, bike2);
        }

        public static bool operator !=(Bike bike1, Bike bike2)
        {
            return !(bike1 == bike2);
        }

        public override string ToString()
        {
            return $"PricePerDay: {PricePerDay},BikeDescription: {Description},Kind: {Kind},ID: {ID}";
        }
    }
}
