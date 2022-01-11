using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Models
{
    public class HomeworkModel
    {
        public Guid HomeworkId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime HomeworkDateTime { get; set; }
        public DateTime? HomeworkDateFrom { get; set; }
        public DateTime? HomeworkDateTo { get; set; }
        public int? LikesCountFrom { get; set; }
        public int? LikesCountTo { get; set; }
        public string HomeWorkDateTimeStr { get; set; }
        public int LikesCount { get; set; }
        public int DisLikesCount { get; set; }
        public string LinkFile { get; set; }
        public Guid HomeworkTypeId { get; set; }
        public Guid TeacherId { get; set; }
        public string TeacherFio { get; set; }
        public string HomeworkType { get; set; }
        public Teacher Teacher { get; set; }
        public Guid[] GroupIds { get; set; }
        public string GroupsForShow { get; set; }
        public MultiSelectList Groups { get; set; }
        public IEnumerable<SelectListItem> Teachers { get; set; }
        public IEnumerable<SelectListItem> HomeworkTypes { get; set; }
        public ICollection<HomeworkForGroup> HomeworkForGroups { get; set; }
        public ICollection<Message> Messages { get; set; }
        public IEnumerable<HomeworkModel> Homeworks { get; set; }
    }
}