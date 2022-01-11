using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class GroupRepository : IRepository<Group>
    {
        private DduContext _dduContext;//контекс базы данных

        public GroupRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public Group Get(Guid id)
        {
            return _dduContext.Groups.Include(x => x.ChildrenInGroup).ThenInclude(x => x.Child).Where(x => x.Id == id).First();
        }
        public List<Group> GetAll()
        {
            return _dduContext.Groups.Include(x => x.ChildrenInGroup).ThenInclude(x => x.Child).ToList();
        }
        public void Add(Group Group)
        {
            _dduContext.Groups.Add(Group);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid id)
        {

            Group Group = _dduContext.Groups.Find(id);

            if (Group != null)
            {
                _dduContext.Groups.Remove(Group);
                _dduContext.SaveChanges();
            }
        }
        public void Update(Group Group)
        {
            _dduContext.Entry(Group).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
