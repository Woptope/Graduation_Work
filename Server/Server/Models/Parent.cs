using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class Parent
    {
        public Parent()
        {
            FathersChildren = new HashSet<Child>();
            MothersChildren = new HashSet<Child>();
        }

        public Guid Id { get; set; }
        public bool LargeFamilie { get; set; }

        public  User User { get; set; }
        public  ICollection<Child> FathersChildren { get; set; }
        public  ICollection<Child> MothersChildren { get; set; }
    }
}
