using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class Homework
    {
        public Homework()
        {
            HomeworkForGroups = new HashSet<HomeworkForGroup>();
            Messages = new HashSet<Message>();
            RatingHomework = new HashSet<RatingHomework>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime HomeWorkDateTime { get; set; }
        public int LikesCount { get; set; }
        public int DisLikesCount { get; set; }
        public string LinkFile { get; set; }
        public Guid HomeworkTypeId { get; set; }
        public Guid TeacherId { get; set; }

        public  HomeworkType HomeworkType { get; set; }
        public  Teacher Teacher { get; set; }
        public  ICollection<HomeworkForGroup> HomeworkForGroups { get; set; }
        public  ICollection<Message> Messages { get; set; }
        public ICollection<RatingHomework> RatingHomework { get; set; }
    }
}
