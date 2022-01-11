using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Models
{
    public class HomeworkChatModel
    {
        public Guid HomeworkId { get; set; }
        public Guid MessageTypeId { get; set; }
        public string Name { get; set; }
        public string MessageText { get; set; }
        public bool IsAnonymous { get; set; }
        public string TeacherFio { get; set; }
        public string HomeworkType { get; set; }
        public string HomeWorkDateTimeStr { get; set; }
        public string Messages { get; set; }
        public IEnumerable<SelectListItem> MessageTypes { get; set; }
    }
}