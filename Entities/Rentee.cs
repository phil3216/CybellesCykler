using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Entities
{
    public class Rentee : INotifyPropertyChanged , IPersistable, IEquatable<Rentee>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string name;
        protected string address;
        protected string phonenumber;
        protected int id;
        protected DateTime registerDate;

        public string Name
        {
            get => name;
            set
            {
                (bool b, string s) = ValidateName(value);
                if (!b) throw new ArgumentException(s);

                name = value;
                OnPropertyChanged();
            }
        }




        public string Address
        {
            get => address;
            set
            {
                (bool b, string s) = ValidateAddress(value);
                if (!b) throw new ArgumentException(s);

                address = value;
                OnPropertyChanged();
            }
        }



        public string Phonenumber
        {
            get => phonenumber;
            set
            {
                (bool b, string s) = ValidatePhonenumber(value);
                if (!b) throw new ArgumentException(s);

                phonenumber = value;
                OnPropertyChanged();
            }
        }



        public DateTime RegisterDate
        {
            get => registerDate;
            set
            {
                (bool b, string s) = ValidateRegisterDate(value);
                if (!b) throw new ArgumentException(s);

                registerDate = value;
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


        public static (bool, string) ValidateName(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return (false,"Name cannot be empty");

            return (true, String.Empty);
        }

        public static (bool, string) ValidateAddress(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return (false, "Address cannot be empty");

            return (true, String.Empty);
        }

        public static (bool, string) ValidatePhonenumber(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return (false, "A phonenumber cannot be empty");

            int phonenumberLength = value.Where(x => Char.IsDigit(x)).Count();

            if (phonenumberLength < 5) return (false, "A phonenumber cannot be shorter than 5 digits");
            if (phonenumberLength > 20) return (false, "A phonenumber cannot be longer than 20 digits");

            return (true, String.Empty);
        }

        public static (bool, string) ValidateID(int value)
        {
            if (value < 1)
                return (false, "ID cannot be less than 1");

            return (true, String.Empty);
        }


        public static (bool, string) ValidateRegisterDate(DateTime value)
        {
            return (true, String.Empty);
        }

        public Rentee(string name, string address, string phonenumber, DateTime registerDate, int id) : this(name,address,phonenumber,registerDate)
        {
            ID = id;
        }

        public Rentee(string name,string address,string phonenumber,DateTime registerDate)
        {
            Name = name;
            Address = address;
            Phonenumber = phonenumber;
            RegisterDate = registerDate;
        }



        protected virtual void OnPropertyChanged([CallerMemberName] string s = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

        public override bool Equals(object obj)
        {
            return Equals(obj as Rentee);
        }

        public bool Equals(Rentee other)
        {
            return other != null &&
                   Name == other.Name &&
                   Address == other.Address &&
                   Phonenumber == other.Phonenumber &&
                   RegisterDate == other.RegisterDate &&
                   ID == other.ID;
        }

        public override int GetHashCode()
        {
            var hashCode = 1289178531;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phonenumber);
            hashCode = hashCode * -1521134295 + RegisterDate.GetHashCode();
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Rentee rentee1, Rentee rentee2)
        {
            return EqualityComparer<Rentee>.Default.Equals(rentee1, rentee2);
        }

        public static bool operator !=(Rentee rentee1, Rentee rentee2)
        {
            return !(rentee1 == rentee2);
        }

        public override string ToString()
        {
            return $"Name: {Name},Address: {Address},Phonenumber: {Phonenumber},RegisterDate: {RegisterDate},ID: {ID}";
        }
    }
}
