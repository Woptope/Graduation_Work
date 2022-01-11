using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Models
{
    public class GroupModel
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public string Timetable { get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; }
        public string TeacherFio { get; set; }
        public Teacher Teacher { get; set; }
        public Guid ChildId { get; set; }
        public IEnumerable<ChildModel> Children { get; set; }
        public IEnumerable<SelectListItem> Teachers { get; set; }
        public IEnumerable<SelectListItem> ChildrenAdd { get; set; }
        public ICollection<ChildInGroup> ChildrenInGroup { get; set; }
        public ICollection<EventGroup> EventsGroup { get; set; }
        public ICollection<HomeworkForGroup> HomeworkForGroup { get; set; }
    }
}