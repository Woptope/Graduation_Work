using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class HomeworkForGroupRepository : IRepository<HomeworkForGroup>
    {
        private DduContext _dduContext;//контекс базы данных

        public HomeworkForGroupRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public HomeworkForGroup Get(Guid id)
        {
            return _dduContext.HomeworkForGroups.Find(id);
        }
        public List<HomeworkForGroup> GetAll()
        {
            return _dduContext.HomeworkForGroups.ToList();
        }
        public void Add(HomeworkForGroup HomeworkForGroups)
        {
            _dduContext.HomeworkForGroups.Add(HomeworkForGroups);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid idHomework, Guid idGroup)
        {

            HomeworkForGroup HomeworkForGroups = _dduContext.HomeworkForGroups.Where(x => x.HomeworkId == idHomework && x.GroupId == idGroup).First();


            if (HomeworkForGroups != null)
            {
                _dduContext.HomeworkForGroups.Remove(HomeworkForGroups);
                _dduContext.SaveChanges();
            }
        }
        public void Update(HomeworkForGroup HomeworkForGroups)
        {
            _dduContext.Entry(HomeworkForGroups).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
