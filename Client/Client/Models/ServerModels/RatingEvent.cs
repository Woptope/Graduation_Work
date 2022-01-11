using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models.ServerModels
{
    public class RatingEvent
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public bool Rating { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }
    }
}