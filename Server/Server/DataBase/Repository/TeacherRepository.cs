using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class TeacherRepository : IRepository<Teacher>
    {
        private DduContext _dduContext;//контекс базы данных

        public TeacherRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public Teacher Get(Guid id)
        {
            return _dduContext.Teachers.Include(x => x.User).ThenInclude(x => x.Account).ThenInclude(x => x.AccountType).Where(x => x.Id ==id).First();
        }
        public List<Teacher> GetAll()
        {
            return _dduContext.Teachers.Include(x => x.User).ThenInclude(x => x.Account).ThenInclude(x => x.AccountType).ToList();
        }
        public void Add(Teacher Teacher)
        {
            _dduContext.Teachers.Add(Teacher);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid id)
        {

            Teacher Teacher = _dduContext.Teachers.Find(id);

            if (Teacher != null)
            {
                _dduContext.Teachers.Remove(Teacher);
                _dduContext.SaveChanges();
            }
        }
        public void Update(Teacher Teacher)
        {
            _dduContext.Entry(Teacher).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
