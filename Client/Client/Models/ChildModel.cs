using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Models
{
    public class ChildModel
    {
        public Guid ChildId { get; set; }
        public DateTime? Birthsday { get; set; }
        public string BirthsdayStr { get; set; }
        public string Fio { get; set; }
        public string Gender { get; set; }
        public Guid? MotherId { get; set; }
        public Guid? FatherId { get; set; }
        public string MotherFio { get; set; }
        public string FatherFio { get; set; }
        public List<SelectListItem> Parents { get; set; }

        public Parent Father { get; set; }
        public Parent Mother { get; set; }
        public ICollection<ChildInGroup> ChildInGroups { get; set; }
    }
}