using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class RatingHomeworkRepository : IRepository<RatingHomework>
    {
        private DduContext _dduContext;//контекс базы данных

        public RatingHomeworkRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public RatingHomework Get(Guid id)
        {
            return _dduContext.RatingHomeworks.Find(id);
        }
        public List<RatingHomework> GetAll()
        {
            return _dduContext.RatingHomeworks.ToList();
        }
        public void Add(RatingHomework ratingHomework)
        {
            _dduContext.RatingHomeworks.Add(ratingHomework);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid idUser, Guid idHomework)
        {
            RatingHomework ratingHomework = _dduContext.RatingHomeworks.Where(x => x.UserId == idUser && x.HomeworkId == idHomework).First();

            if (ratingHomework != null)
            {
                _dduContext.RatingHomeworks.Remove(ratingHomework);
                _dduContext.SaveChanges();
            }
        }
        public void Update(RatingHomework ratingHomework)
        {
            _dduContext.Entry(ratingHomework).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
