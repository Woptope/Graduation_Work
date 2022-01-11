using System;
using System.Collections.Generic;

namespace Client.Models.ServerModels
{
    public partial class Account
    {

        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid AccountTypeId { get; set; }

        public  AccountType AccountType { get; set; }
        public  User User { get; set; }
    }
}
