using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class ChildInGroupRepository : IRepository<ChildInGroup>
    {
        private DduContext _dduContext;//контекс базы данных

        public ChildInGroupRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public ChildInGroup Get(Guid id)
        {
            return _dduContext.ChildInGroups.Find(id);
        }
        public List<ChildInGroup> GetAll()
        {
            return _dduContext.ChildInGroups.ToList();
        }
        public void Add(ChildInGroup ChildInGroup)
        {
            _dduContext.ChildInGroups.Add(ChildInGroup);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid idChild, Guid idGroup)
        {

            ChildInGroup ChildInGroup = _dduContext.ChildInGroups.Where(x => x.ChildId == idChild && x.GroupId == idGroup).First();

            if (ChildInGroup != null)
            {
                _dduContext.ChildInGroups.Remove(ChildInGroup);
                _dduContext.SaveChanges();
            }
        }
        public void Update(ChildInGroup ChildInGroup)
        {
            _dduContext.Entry(ChildInGroup).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
