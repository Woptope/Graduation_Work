using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Models
{
    public class EventChatModel
    {
        public Guid EventId { get; set; }
        public Guid MessageTypeId { get; set; }
        public string Name { get; set; }
        public string MessageText { get; set; }
        public bool IsAnonymous { get; set; }
        public string EventType { get; set; }
        public string EventDateTimeStr { get; set; }
        public string Messages { get; set; }
        public IEnumerable<SelectListItem> MessageTypes { get; set; }
    }
}