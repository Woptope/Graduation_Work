using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    interface IRepository<T> where T : class
    {
        T Get(Guid id);
        List<T> GetAll();
        //Guid Add(T item);
        void Update(T item);
        //void Delete(Guid item);
    }
}
