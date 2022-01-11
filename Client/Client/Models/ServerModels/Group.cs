using System;
using System.Collections.Generic;

namespace Client.Models.ServerModels
{
    public partial class Group
    {
        public Group()
        {
            ChildrenInGroup = new HashSet<ChildInGroup>();
            EventsGroup = new HashSet<EventGroup>();
            HomeworkForGroup = new HashSet<HomeworkForGroup>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Timetable { get; set; }
        public string Description { get; set; }
        public Guid? TeacherId { get; set; }

        public  Teacher Teacher { get; set; }
        public  ICollection<ChildInGroup> ChildrenInGroup { get; set; }
        public  ICollection<EventGroup> EventsGroup { get; set; }
        public  ICollection<HomeworkForGroup> HomeworkForGroup { get; set; }
    }
}
