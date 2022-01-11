using System;
using System.Collections.Generic;

namespace Server.Models
{
    public class AccountType
    {
        public AccountType()
        {
            Accounts = new HashSet<Account>();
        }

        public Guid Id { get; set; }
        public string Type { get; set; }

        public  ICollection<Account> Accounts { get; set; }
    }
}
