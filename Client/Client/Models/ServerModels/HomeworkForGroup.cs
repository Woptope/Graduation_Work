using System;
using System.Collections.Generic;

namespace Client.Models.ServerModels
{
    public partial class HomeworkForGroup
    {
        public Guid GroupId { get; set; }
        public Guid HomeworkId { get; set; }

        public  Group Group { get; set; }
        public  Homework Homework { get; set; }
    }
}
