using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Client.Models
{
    public class AccountModel
    {
        public Guid AccountId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid AccountTypeId { get; set; }
        public IEnumerable<SelectListItem> AccountTypes { get; set; }
        public Guid UserId { get; set; }
        public string Fio { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Birthsday { get; set; }
        public string BirthsdayStr { get; set; }
        public Guid ParentId { get; set; }
        public bool LargeFamilie { get; set; }
        public string LargeFamilieStr { get; set; }
        public Guid TeacherId { get; set; }
        public string Portfolio { get; set; }
        public string Experience { get; set; }
        public string AccountType { get; set; }

    }
}