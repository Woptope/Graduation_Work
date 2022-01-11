using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class MessageType
    {
        public MessageType()
        {
            Messages = new HashSet<Message>();
        }

        public Guid Id { get; set; }
        public string Type { get; set; }

        public  ICollection<Message> Messages { get; set; }
    }
}
