using System.ComponentModel;
using System.Collections.ObjectModel;
using Entities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Business
{
    public class DataController : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private DBHandler handler;
        private ObservableCollection<Bike> bikes;
        private ObservableCollection<Rentee> renters = new ObservableCollection<Rentee>();//Testing//{ new Rentee("fr","fefrt","4556435",DateTime.Now,2) };
        private ObservableCollection<Order> orders;


        public ObservableCollection<Bike> Bikes
        {
            get => bikes;
            private set
            {
                bikes = value;
                OnPropertyChanged();
            }
        }



        public ObservableCollection<Rentee> Renters
        {
            get => renters;
            private set
            {
                renters = value;
                OnPropertyChanged();
            }
        }



        public ObservableCollection<Order> Orders
        {
            get => orders;
            private set
            {
                orders = value;
                OnPropertyChanged();
            }
        }

        public DataController(string connectionString)
        {
            handler = new DBHandler(connectionString);
        }

        /// <summary>
        /// Inserts a new entity into the database. And reloads the observable collection for the type of entity.
        /// For example: If the entity is a bike, then the bike collection will be reloaded after inserting.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool NewEntity(IPersistable entity)
        {
            bool success;
            switch (entity)
            {
                case Bike b:
                    success = handler.NewBike(b);
                    ReloadEntities<Bike>();
                    return success;
                case Order o:
                    success = handler.NewOrder(o);
                    ReloadEntities<Order>();
                    return success;
                case Rentee r:
                    success = handler.NewRentee(r);
                    ReloadEntities<Rentee>();
                    return success;
                default:
                    return false;
            }
        }


        /// <summary>
        /// Updates an entity in the database. And reloads the observable collection connected to the entity type.
        /// For example: If the entity is a bike, then the bike collection will be reloaded after updating.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateEntity(IPersistable entity)
        {
            bool success;
            switch (entity)
            {
                case Bike b:
                    success = handler.UpdateBike(b);
                    ReloadEntities<Bike>();
                    return success;
                case Order o:
                    success = handler.UpdateOrder(o);
                    ReloadEntities<Order>();
                    return success;
                case Rentee r:
                    success = handler.UpdateRentee(r);
                    ReloadEntities<Rentee>();
                    return success;
                default:
                    return false;
            }
        }

        public bool DeleteEntity(IPersistable entity)
        {
            bool success;
            switch (entity)
            {
                case Bike b:
                    success = handler.DeleteBike(b);
                    ReloadEntities<Bike>();
                    return success;
                case Order o:
                    success = handler.DeleteOrder(o);
                    ReloadEntities<Order>();
                    return success;
                case Rentee r:
                    success = handler.DeleteRentee(r);
                    ReloadEntities<Rentee>();
                    return success;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityKind"></param>
        /// <returns></returns>
        public List<IPersistable> GetEntities(IPersistableKind entityKind)
        {
            switch (entityKind)
            {
                case IPersistableKind.Bike:
                    return handler.GetAllBikes().Cast<IPersistable>().ToList();
                case IPersistableKind.Order:
                    return handler.GetAllOrders().Cast<IPersistable>().ToList();
                case IPersistableKind.Rentee:
                    return handler.GetAllRentees().Cast<IPersistable>().ToList();
                default:
                    return new List<IPersistable>();
            }
        }

        public IPersistable GetEntity(IPersistableKind entityKind, int id)
        {
            switch (entityKind)
            {
                case IPersistableKind.Bike:
                    return handler.GetBike(id);
                case IPersistableKind.Order:
                    return handler.GetOrder(id);
                case IPersistableKind.Rentee:
                    return handler.GetRentee(id);
                default:
                    return null;
            }

        }

        /// <summary>
        /// Reloads the observable collection for the type specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ReloadEntities<T>() where T : IPersistable
        {
            Type type = typeof(T);

            switch (type.Name)
            {
                case "Bike":
                    bikes = new ObservableCollection<Bike>(handler.GetAllBikes());
                    break;
                case "Order":
                    orders = new ObservableCollection<Order>(handler.GetAllOrders());
                    break;
                case "Rentee":
                    Renters = new ObservableCollection<Rentee>(handler.GetAllRentees());
                    break;
                default:
                    break;
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string s = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

    }

    public enum IPersistableKind
    {
        Bike,
        Order,
        Rentee
    }
}
