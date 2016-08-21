using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMVCProject.Services.Interfaces
{
    public interface IService<T> : IDisposable
    {
        bool PutItem(T item);
        T GetItem(int id);
        IQueryable<T> GetItems();       
        bool Get(int id);
        bool Any(int id);
        T PostItem(T item);
    }
}