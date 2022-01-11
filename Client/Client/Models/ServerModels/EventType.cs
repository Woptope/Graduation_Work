using System;
using System.Collections.Generic;

namespace Client.Models.ServerModels
{
    public partial class EventType
    {
        public EventType()
        {
            Events = new HashSet<Event>();
        }

        public Guid Id { get; set; }
        public string Type { get; set; }

        public  ICollection<Event> Events { get; set; }
    }
}
