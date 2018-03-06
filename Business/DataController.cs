using Entities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class DataController
    {
        private DBHandler handler;

        public DataController(string connectionString)
        {
            handler = new DBHandler(connectionString);
        }

        public bool NewEntity(IPersistable entity)
        {
            switch (entity)
            {
                case Bike b:
                    return handler.NewBike(b);
                case Order o:
                    return handler.NewOrder(o);
                case Rentee r:
                    return handler.NewRentee(r);
                default:
                    return false;
            }
        }

        public bool UpdateEntity(IPersistable entity)
        {
            switch (entity)
            {
                case Bike b:
                    return handler.UpdateBike(b);
                case Order o:
                    return handler.UpdateOrder(o);
                case Rentee r:
                    return handler.UpdateRentee(r);
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
    }

    public enum IPersistableKind
    {
        Bike,
        Order,
        Rentee
    }
}
