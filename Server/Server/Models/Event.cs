using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class Event
    {
        public Event()
        {
            EventGroups = new HashSet<EventGroup>();
            Messages = new HashSet<Message>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }
        public int LikesCount { get; set; }
        public int DisLikesCount { get; set; }
        public Guid EventTypeId { get; set; }

        public  EventType EventType { get; set; }
        public  ICollection<EventGroup> EventGroups { get; set; }
        public  ICollection<Message> Messages { get; set; }
        public ICollection<RatingEvent> RatingEvent { get; set; }
    }
}
