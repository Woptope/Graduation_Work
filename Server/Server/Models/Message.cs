using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class Message
    {
        public Guid Id { get; set; }
        public string MessageText { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime MessageDateTime { get; set; }
        public Guid MessageTypeId { get; set; }
        public Guid UserId { get; set; }
        public Guid? EventId { get; set; }
        public Guid? HomeworkId { get; set; }

        public  Event Event { get; set; }
        public  Homework Homework { get; set; }
        public  MessageType MessageType { get; set; }
        public  User User { get; set; }
    }
}
