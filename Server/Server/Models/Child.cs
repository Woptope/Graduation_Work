using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class Child
    {
        public Child()
        {
            ChildInGroups = new HashSet<ChildInGroup>();
        }

        public Guid Id { get; set; }
        public DateTime? Birthsday { get; set; }
        public string Fio { get; set; }
        public string Gender { get; set; }
        public Guid? MotherId { get; set; }
        public Guid? FatherId { get; set; }

        public  Parent Father { get; set; }
        public  Parent Mother { get; set; }
        public  ICollection<ChildInGroup> ChildInGroups { get; set; }
    }
}
