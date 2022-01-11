using System;
using System.Collections.Generic;

namespace Client.Models.ServerModels
{
    public partial class ChildInGroup
    {
        public Guid GroupId { get; set; }
        public Guid ChildId { get; set; }

        public  Child Child { get; set; }
        public  Group Group { get; set; }
    }
}
