using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class ChildRepository : IRepository<Child>
    {
        private DduContext _dduContext;//контекс базы данных

        public ChildRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public Child Get(Guid id)
        {
            return _dduContext.Children.Find(id);
        }
        public List<Child> GetAll()
        {
            return _dduContext.Children.Include(x => x.Father).ThenInclude(x => x.User).Include(x => x.Mother).ThenInclude(x => x.User).ToList();
        }

        public List<Child> GetAllChildrenForParent(Guid parentId)
        {
            return _dduContext.Children.Include(x => x.Father).ThenInclude(x => x.User).Include(x => x.Mother).ThenInclude(x => x.User).Where(x => x.FatherId == parentId || x.MotherId == parentId).ToList();
        }

        public void Add(Child Child)
        {
            _dduContext.Children.Add(Child);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid id)
        {

            Child Child = _dduContext.Children.Find(id);

            if (Child != null)
            {
                _dduContext.Children.Remove(Child);
                _dduContext.SaveChanges();
            }
        }
        public void Update(Child Child)
        {
            _dduContext.Entry(Child).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
