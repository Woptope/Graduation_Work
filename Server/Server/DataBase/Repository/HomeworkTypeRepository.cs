using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class HomeworkTypeRepository : IRepository<HomeworkType>
    {
        private DduContext _dduContext;//контекс базы данных

        public HomeworkTypeRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public HomeworkType Get(Guid id)
        {
            return _dduContext.HomeworkTypes.Find(id);
        }
        public List<HomeworkType> GetAll()
        {
            return _dduContext.HomeworkTypes.ToList();
        }
        public void Add(HomeworkType HomeworkType)
        {
            _dduContext.HomeworkTypes.Add(HomeworkType);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid id)
        {

            HomeworkType HomeworkType = _dduContext.HomeworkTypes.Find(id);

            if (HomeworkType != null)
            {
                _dduContext.HomeworkTypes.Remove(HomeworkType);
                _dduContext.SaveChanges();
            }
        }
        public void Update(HomeworkType HomeworkType)
        {
            _dduContext.Entry(HomeworkType).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
