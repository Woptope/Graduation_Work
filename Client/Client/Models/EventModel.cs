using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Models
{
    public class EventModel
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        public DateTime? EventDateFrom { get; set; }
        public DateTime? EventDateTo { get; set; }
        public DateTime EventDateTime { get; set; }
        public int? LikesCountFrom { get; set; }
        public int? LikesCountTo { get; set; }
        public string EventDateTimeStr { get; set; }
        public int LikesCount { get; set; }
        public int DisLikesCount { get; set; }
        public Guid EventTypeId { get; set; }
        public Guid[] GroupIds { get; set; }
        public string GroupsForShow { get; set; }
        public MultiSelectList Groups { get; set; }
        public IEnumerable<SelectListItem> EventTypes { get; set; }
        public IEnumerable<EventModel> Events { get; set; }
        public ICollection<EventGroup> EventForGroups { get; set; }
    }
}