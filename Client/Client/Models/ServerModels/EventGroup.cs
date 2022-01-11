using System;
using System.Collections.Generic;

namespace Client.Models.ServerModels
{
    public partial class EventGroup
    {
        public Guid EventId { get; set; }
        public Guid GroupId { get; set; }

        public  Event Event { get; set; }
        public  Group Group { get; set; }
    }
}
