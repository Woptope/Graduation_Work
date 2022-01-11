using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase.Repository
{
    public class MessageRepository : IRepository<Message>
    {
        private DduContext _dduContext;//контекс базы данных

        public MessageRepository(DduContext dduContext)
        {
            _dduContext = dduContext;
        }

        public Message Get(Guid id)
        {
            return _dduContext.Messages.Find(id);
        }
        public List<Message> GetAll()
        {
            return _dduContext.Messages.ToList();
        }
        public List<Message> GetHomeworkMessages(Guid homeworkId)
        {
            return _dduContext.Messages.Include(x => x.MessageType).Include(x => x.User).Where(x => x.HomeworkId == homeworkId).ToList();
        }

        public List<Message> GetEventMessages(Guid eventId)
        {
            return _dduContext.Messages.Include(x => x.MessageType).Include(x => x.User).Where(x => x.EventId == eventId).ToList();
        }
        public void Add(Message Message)
        {
            _dduContext.Messages.Add(Message);
            _dduContext.SaveChanges();
        }
        public void Delete(Guid id)
        {

            Message Message = _dduContext.Messages.Find(id);

            if (Message != null)
            {
                _dduContext.Messages.Remove(Message);
                _dduContext.SaveChanges();
            }
        }
        public void Update(Message Message)
        {
            _dduContext.Entry(Message).State = EntityState.Modified;
            _dduContext.SaveChanges();
        }
    }
}
