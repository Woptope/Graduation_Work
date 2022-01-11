using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }
        public string MessageText { get; set; }
        public string UserFio { get; set; }
        public string MessageType { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime MessageDateTime { get; set; }
        public Guid MessageTypeId { get; set; }
        public Guid UserId { get; set; }
        public Guid? EventId { get; set; }
        public Guid? HomeworkId { get; set; }
    }
}