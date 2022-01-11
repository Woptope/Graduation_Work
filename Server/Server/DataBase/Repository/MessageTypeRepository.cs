using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class MessageTypeRepository : IRepository<MessageType>
    {
        private DduContext _dduContext;//контекс базы данных

        public MessageTypeRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public MessageType Get(Guid id)
        {
            return _dduContext.MessageTypes.Find(id);
        }
        public List<MessageType> GetAll()
        {
            return _dduContext.MessageTypes.ToList();
        }
        public void Add(MessageType MessageType)
        {
            _dduContext.MessageTypes.Add(MessageType);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid id)
        {

            MessageType MessageType = _dduContext.MessageTypes.Find(id);

            if (MessageType != null)
            {
                _dduContext.MessageTypes.Remove(MessageType);
                _dduContext.SaveChanges();
            }
        }
        public void Update(MessageType MessageType)
        {
            _dduContext.Entry(MessageType).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
