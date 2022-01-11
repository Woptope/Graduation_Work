using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class EventTypeRepository : IRepository<EventType>
    {
        private DduContext _dduContext;//контекс базы данных

        public EventTypeRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public EventType Get(Guid id)
        {
            return _dduContext.EventTypes.Find(id);
        }
        public List<EventType> GetAll()
        {
            return _dduContext.EventTypes.ToList();
        }
        public void Add(EventType EventType)
        {
            _dduContext.EventTypes.Add(EventType);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid id)
        {

            EventType EventType = _dduContext.EventTypes.Find(id);

            if (EventType != null)
            {
                _dduContext.EventTypes.Remove(EventType);
                _dduContext.SaveChanges();
            }
        }
        public void Update(EventType EventType)
        {
            _dduContext.Entry(EventType).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
