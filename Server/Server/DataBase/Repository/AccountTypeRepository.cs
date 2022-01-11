using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class AccountTypeRepository : IRepository<AccountType>
    {
        private DduContext _dduContext;//контекс базы данных

        public AccountTypeRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public AccountType Get(Guid id)
        {
            return _dduContext.AccountTypes.Find(id);
        }
        public List<AccountType> GetAll()
        {
            return _dduContext.AccountTypes.ToList();
        }
        public void Add(AccountType accountType)
        {
            _dduContext.AccountTypes.Add(accountType);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid id)
        {

            AccountType accountType = _dduContext.AccountTypes.Find(id);

            if (accountType != null)
            {
                _dduContext.AccountTypes.Remove(accountType);
                _dduContext.SaveChanges();
            }
        }
        public void Update(AccountType accountType)
        {
            _dduContext.Entry(accountType).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
