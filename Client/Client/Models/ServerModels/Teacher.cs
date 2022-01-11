using System;
using System.Collections.Generic;

namespace Client.Models.ServerModels
{
    public partial class Teacher
    {
        public Teacher()
        {
            Groups = new HashSet<Group>();
            Homework = new HashSet<Homework>();
        }

        public Guid Id { get; set; }
        public string Portfolio { get; set; }
        public string Experience { get; set; }

        public  User User { get; set; }
        public  ICollection<Group> Groups { get; set; }
        public  ICollection<Homework> Homework { get; set; }
    }
}
