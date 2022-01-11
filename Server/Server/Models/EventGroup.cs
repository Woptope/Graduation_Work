using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class EventGroup
    {
        public Guid EventId { get; set; }
        public Guid GroupId { get; set; }

        public  Event Event { get; set; }
        public  Group Group { get; set; }
    }
}
