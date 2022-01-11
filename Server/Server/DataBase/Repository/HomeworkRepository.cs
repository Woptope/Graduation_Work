using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class HomeworkRepository : IRepository<Homework>
    {
        private DduContext _dduContext;//контекс базы данных

        public HomeworkRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public Homework Get(Guid id)
        {
            return _dduContext.Homework.Include(x => x.HomeworkType).Include(x => x.RatingHomework).Include(x => x.HomeworkForGroups).ThenInclude(x => x.Group).Include(x => x.Teacher).ThenInclude(x => x.User).Where(x => x.Id == id).First();
        }
        public List<Homework> GetAll()
        {
            return _dduContext.Homework.Include(x => x.Teacher).ThenInclude(x => x.User).Include(x => x.HomeworkType).Include(x => x.RatingHomework).Include(x => x.HomeworkForGroups).ToList();
        }
        public Guid Add(Homework Homework)
        {
            _dduContext.Homework.Add(Homework);
            _dduContext.SaveChanges();
            return Homework.Id;
        }
        public void Delete(Guid id)
        {

            Homework Homework = _dduContext.Homework.Find(id);

            if (Homework != null)
            {
                _dduContext.Homework.Remove(Homework);
                _dduContext.SaveChanges();
            }
        }
        public void Update(Homework Homework)
        {
            _dduContext.Entry(Homework).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }

        public void AddLike(Guid id)//добаление лайка для мероприятия
        {
            Homework Homework = _dduContext.Homework.Find(id);
            Homework.LikesCount++;

            _dduContext.Entry(Homework).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
        public void DeleteLike(Guid id)//добаление лайка для мероприятия
        {
            Homework Homework = _dduContext.Homework.Find(id);
            Homework.LikesCount--;

            _dduContext.Entry(Homework).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }

        public void AddDisLike(Guid id)//добаление дизлайка для мероприятия
        {
            Homework Homework = _dduContext.Homework.Find(id);
            Homework.DisLikesCount++;

            _dduContext.Entry(Homework).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
        public void DeleteDisLike(Guid id)//добаление дизлайка для мероприятия
        {
            Homework Homework = _dduContext.Homework.Find(id);
            Homework.DisLikesCount--;

            _dduContext.Entry(Homework).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
