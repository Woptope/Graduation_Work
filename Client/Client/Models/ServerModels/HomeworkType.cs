using System;
using System.Collections.Generic;

namespace Client.Models.ServerModels
{
    public partial class HomeworkType
    {
        public HomeworkType()
        {
            Homework = new HashSet<Homework>();
        }

        public Guid Id { get; set; }
        public string Type { get; set; }

        public  ICollection<Homework> Homework { get; set; }
    }
}
