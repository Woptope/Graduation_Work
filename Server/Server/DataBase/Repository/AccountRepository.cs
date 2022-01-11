using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.DataBase.Repository
{
    public class AccountRepository : IRepository<Account>
    {
        private DduContext _dduContext;//контекс базы данных

        public AccountRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public Account Get(Guid id)
        {
            return _dduContext.Accounts.Find(id);
        }
        public List<Account> GetAll()
        {
            return _dduContext.Accounts.Include(x => x.AccountType).Include(x => x.User).ToList();
        }
        public Guid Add(Account Account)
        {
            _dduContext.Accounts.Add(Account);
            _dduContext.SaveChanges();
            return Account.Id;
        }
        public void Delete(Guid id)
        {

            Account Account = _dduContext.Accounts.Find(id);

            if (Account != null)
            {
                _dduContext.Accounts.Remove(Account);
                _dduContext.SaveChanges();
            }
        }
        public void Update(Account Account)
        {
            _dduContext.Entry(Account).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
