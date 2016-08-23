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
        // private ExpatMigEntities2 db = new ExpatMigEntities2();

        private static readonly List<Item> mockDatabase = new List<Item>
        {
            new Item{Id = GetIndex(),    Username = "John Doe",    Place = 4,DateOfTravel="12/01/2017",FlightName="SGA",FlightCost="45000",Website="Skyscanner"},
            new Item{Id = GetIndex(),    Username = "Jane Doe",   Place = 4,DateOfTravel="12/02/2017",FlightName="CP",FlightCost="67000",Website="goibibo"},
            new Item{Id = GetIndex(),    Username = "Avon McNulty",     Place = 5,DateOfTravel="13/01/2017",FlightName="BA",FlightCost="65999",Website="travel agent"},
            new Item{Id = GetIndex(),    Username = "Jessica Bing",   Place = 5,DateOfTravel="15/06/2017",FlightName="AI",FlightCost="23456",Website="travel agent"},

        };
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
            return mockDatabase.Any(item => item.Id == id);
        }

        bool IService<Item>.Get(int id)
        {
            // Item updateone = (from x in db.Items
            //                   where x.Id == id
            //                   select x).First();
            //db.Items.Remove(updateone);
            // db.SaveChanges();
            // return true;
            var itemToRemove = mockDatabase.SingleOrDefault(item => item.Id == id);
            if (itemToRemove == null) return false;
            return mockDatabase.Remove(itemToRemove);
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        
        Item IService<Item>.GetItem(int id)
        {
            //Item updateone = (from x in db.Items
            //                  where x.Id == id
            //                  select x).First();
            //return updateone;
            return mockDatabase.SingleOrDefault(item => item.Id == id);
        }

        IEnumerable<Item> IService<Item>.GetItems()
        {

            //var itemList = db.Items
            //.ToList()
            //.Select(item =>
            //    new Item
            //    {
            //        Username = item.Username,
            //        Place = item.Place,
            //        DateOfTravel = item.DateOfTravel,
            //        FlightName = item.FlightName,
            //        FlightCost = item.FlightCost,
            //        Website = item.Website,
            //        City = item.City
            //    })
            //.AsQueryable();
            // return itemList;
            //return db.Items;
            return mockDatabase;
        }

        Item IService<Item>.PostItem(Item item)
        {
            //db.Items.Add(item);
            //item.Id = GetIndex();

            //db.SaveChanges();
            //return item;

            mockDatabase.Add(item);
            item.Id = GetIndex();
            return item;
        }
       
        bool IService<Item>.PutItem(Item updatedItem)
        { // Null check
        //    if (uitem == null) return false;

        //    // Item must exists
        //    if (!this.Any(uitem.Id)) return false;

        //    Item updateone = (from x in db.Items
        //                  where x.Id == uitem.Id
        //                  select x).First();

        //    updateone.City = uitem.City;
        //    db.SaveChanges();           
        //    return true;
          if (updatedItem == null) return false;

            // Item must exists
            if (!this.Any(updatedItem.Id)) return false;

            var serverItem = mockDatabase.Find(item => item.Id == updatedItem.Id);

             serverItem.Username = updatedItem.Username;
             serverItem.Place = updatedItem.Place;

            return true;
        }

        private bool Any(int id)
        {
            //Item updateone = (from x in db.Items
            //                  where x.Id == id
            //                  select x).First();
            //if (updateone != null)
            //    return true;
            //else
            //    return false;
            Item updateone= mockDatabase.SingleOrDefault(item => item.Id == id);
            if (updateone != null)
                return true;
            else
                return false;
        }


    }
}
