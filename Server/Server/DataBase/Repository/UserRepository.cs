using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class UserRepository : IRepository<User>
    {
        private DduContext _dduContext;//контекс базы данных

        public UserRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public User Get(Guid id)
        {
            return _dduContext.Users.Include(x => x.Account).ThenInclude(x => x.AccountType).Where(x => x.Id == id).First();
        }
        public List<User> GetAll()
        {
            return _dduContext.Users.ToList();
        }
        public Guid Add(User User)
        {
            _dduContext.Users.Add(User);
            _dduContext.SaveChanges();
            return User.Id;
        }
        public void Delete(Guid id)
        {

            User User = _dduContext.Users.Find(id);

            if (User != null)
            {
                _dduContext.Users.Remove(User);
                _dduContext.SaveChanges();
            }
        }
        public void Update(User User)
        {
            _dduContext.Entry(User).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
