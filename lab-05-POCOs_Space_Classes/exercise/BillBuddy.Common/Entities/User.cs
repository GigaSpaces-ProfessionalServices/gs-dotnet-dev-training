using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core.Metadata;
using GigaSpaces.Core;
using System.Collections;

namespace BillBuddy.Common.Entities
{
    public class User
    {
        public User(int? userAccountId) { UserAccountId = userAccountId; }

        public User() { }

        public int? UserAccountId { get; set; }

        public string Name { get; set; }

        public double? Balance { get; set; }

        public double? CreditLimit { get; set; }

        public AccountStatus? Status { get; set; }
    }
}