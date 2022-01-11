using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class EventRepository : IRepository<Event>
    {
        private DduContext _dduContext;//контекс базы данных

        public EventRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public Event Get(Guid id)
        {
            return _dduContext.Events.Include(x => x.EventType).Include(x => x.RatingEvent).Include(x => x.EventGroups).ThenInclude(x => x.Group).Where(x => x.Id == id).First();
        }
        public List<Event> GetAll()
        {
            return _dduContext.Events.Include(x => x.EventType).Include(x => x.RatingEvent).Include(x => x.EventGroups).ToList();
        }
        public Guid Add(Event Event)
        {
            _dduContext.Events.Add(Event);
            _dduContext.SaveChanges();
            return Event.Id;
        }
        public void Delete(Guid id)
        {

            Event Event = _dduContext.Events.Find(id);

            if (Event != null)
            {
                _dduContext.Events.Remove(Event);
                _dduContext.SaveChanges();
            }
        }
        public void Update(Event Event)
        {
            _dduContext.Entry(Event).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }

        public void AddLike(Guid id)//добаление лайка для мероприятия
        {
            Event Event = _dduContext.Events.Find(id);
            Event.LikesCount++;

            _dduContext.Entry(Event).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
        public void DeleteLike(Guid id)//добаление лайка для мероприятия
        {
            Event Event = _dduContext.Events.Find(id);
            Event.LikesCount--;

            _dduContext.Entry(Event).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }

        public void AddDisLike(Guid id)//добаление дизлайка для мероприятия
        {
            Event Event = _dduContext.Events.Find(id);
            Event.DisLikesCount++;

            _dduContext.Entry(Event).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
        public void DeleteDisLike(Guid id)//добаление дизлайка для мероприятия
        {
            Event Event = _dduContext.Events.Find(id);
            Event.DisLikesCount--;

            _dduContext.Entry(Event).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
