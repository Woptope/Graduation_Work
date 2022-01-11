using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class RatingEventRepository : IRepository<RatingEvent>
    {
        private DduContext _dduContext;//контекс базы данных

        public RatingEventRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public RatingEvent Get(Guid id)
        {
            return _dduContext.RatingEvents.Find(id);
        }
        public List<RatingEvent> GetAll()
        {
            return _dduContext.RatingEvents.ToList();
        }
        public void Add(RatingEvent RatingEvent)
        {
            _dduContext.RatingEvents.Add(RatingEvent);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid idUser, Guid idEvent)
        {
            RatingEvent RatingEvent = _dduContext.RatingEvents.Where(x => x.UserId == idUser && x.EventId == idEvent).First();

            if (RatingEvent != null)
            {
                _dduContext.RatingEvents.Remove(RatingEvent);
                _dduContext.SaveChanges();
            }
        }
        public void Update(RatingEvent RatingEvent)
        {
            _dduContext.Entry(RatingEvent).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
