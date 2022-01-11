using Server.Models;
using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class RatingHomework
    {
        public Guid UserId { get; set; }
        public Guid HomeworkId { get; set; }
        public bool Rating { get; set; }

        public  Homework Homework { get; set; }
        public  User User { get; set; }
    }
}
