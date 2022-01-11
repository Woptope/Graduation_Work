using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class ParentRepository : IRepository<Parent>
    {
        private DduContext _dduContext;//контекс базы данных

        public ParentRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public Parent Get(Guid id)
        {
            Parent parent = _dduContext.Parents.Include(x => x.User).ThenInclude(x => x.Account).ThenInclude(x => x.AccountType).Where(x => x.Id == id).First();

            parent.MothersChildren = _dduContext.Children.Where(x => x.MotherId == parent.Id).ToList();
            parent.FathersChildren = _dduContext.Children.Where(x => x.FatherId == parent.Id).ToList();

            return parent;
        }
    public List<Parent> GetAll()
    {
        List<Parent> parents = _dduContext.Parents.Include(x => x.User).ThenInclude(x => x.Account).ThenInclude(x => x.AccountType).ToList();
        foreach (var item in parents)
        {
                item.MothersChildren = _dduContext.Children.Where(x => x.MotherId == item.Id).ToList();
                item.FathersChildren = _dduContext.Children.Where(x => x.FatherId == item.Id).ToList();
            }
        return _dduContext.Parents.Include(x => x.FathersChildren).Include(x => x.MothersChildren).Include(x => x.User).ThenInclude(x => x.Account).ThenInclude(x => x.AccountType).ToList();
    }
    public void Add(Parent Parent)
    {
        _dduContext.Parents.Add(Parent);
        _dduContext.SaveChanges();
    }
    public void Delete(Guid id)
    {

        Parent Parent = _dduContext.Parents.Find(id);

        if (Parent != null)
        {
            _dduContext.Parents.Remove(Parent);
            _dduContext.SaveChanges();
        }
    }
    public void Update(Parent Parent)
    {
        _dduContext.Entry(Parent).State = EntityState.Modified;
        _dduContext.SaveChanges();
    }
}
}
