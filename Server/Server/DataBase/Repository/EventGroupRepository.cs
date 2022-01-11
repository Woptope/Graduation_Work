using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class EventGroupRepository : IRepository<EventGroup>
    {
        private DduContext _dduContext;//контекс базы данных

        public EventGroupRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public EventGroup Get(Guid id)
        {
            return _dduContext.EventGroups.Find(id);
        }
        public List<EventGroup> GetAll()
        {
            return _dduContext.EventGroups.ToList();
        }
        public void Add(EventGroup EventGroup)
        {
            _dduContext.EventGroups.Add(EventGroup);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid idEvent, Guid idGroup)
        {
            EventGroup EventGroup = _dduContext.EventGroups.Where(x => x.EventId == idEvent && x.GroupId == idGroup).First();

            if (EventGroup != null)
            {
                _dduContext.EventGroups.Remove(EventGroup);
                _dduContext.SaveChanges();
            }
        }
        public void Update(EventGroup EventGroup)
        {
            _dduContext.Entry(EventGroup).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
