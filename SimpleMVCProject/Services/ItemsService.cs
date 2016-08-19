using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMVCProject.Models;
using SimpleMVCProject.Services.Interfaces;

namespace SimpleMVCProject.Services
{
    /// <summary>
    /// Interface resolved by simple injector
    /// </summary>
    public interface IItemsService : IService<Item>
    { }
    public class ItemsService : IItemsService
    {
        private ExpatMigEntities1 db = new ExpatMigEntities1();
        #region Auto increment index

        private static int autoIncrementIndex = 0;

        private static int GetIndex()
        {
            autoIncrementIndex++;
            return autoIncrementIndex;
        }

        #endregion
      
        bool IService<Item>.Any(int id)
        {
            return db.Items.Any(item => item.Id == id);
        }

        bool IService<Item>.Get(int id)
        {
            Item updateone = (from x in db.Items
                              where x.Id == id
                              select x).First();
           db.Items.Remove(updateone);
            db.SaveChanges();
            return true;
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        
        Item IService<Item>.GetItem(int id)
        {
            Item updateone = (from x in db.Items
                              where x.Id == id
                              select x).First();
            return updateone;
        }

        IEnumerable<Item> IService<Item>.GetItems()
        {
            return db.Items;
        }

        Item IService<Item>.PostItem(Item item)
        {
            db.Items.Add(item);
            item.Id = GetIndex();
            db.SaveChanges();
            return item;
        }
       
        bool IService<Item>.PutItem(Item uitem)
        { // Null check
            if (uitem == null) return false;

            // Item must exists
            if (!this.Any(uitem.Id)) return false;

            Item updateone = (from x in db.Items
                          where x.Id == uitem.Id
                          select x).First();
            updateone.FlightCost = uitem.FlightCost;
            updateone.DateOfTravel = uitem.DateOfTravel;
            updateone.FlightName = uitem.FlightName;
            updateone.Place = uitem.Place;
            db.SaveChanges();           
            return true;

        }

        private bool Any(int id)
        {
            Item updateone = (from x in db.Items
                              where x.Id == id
                              select x).First();
            if (updateone != null)
                return true;
            else
                return false;
        }
    }
}
