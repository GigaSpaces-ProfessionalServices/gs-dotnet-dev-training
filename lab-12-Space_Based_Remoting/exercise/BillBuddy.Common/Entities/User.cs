using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core.Metadata;

namespace BillBuddy.Common.Entities
{
        [SpaceClass]
    public class User
    {
        public User(int? userAccountId)
        {
            this.UserAccountId = userAccountId;
        }

        public User() { }

        [SpaceID(AutoGenerate = false)]
        [SpaceRouting]
        public int? UserAccountId { get; set; }

        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public string Name { get; set; }

        public double? Balance { get; set; }

        [SpaceIndex(Type = SpaceIndexType.Extended)]
        public double? CreditLimit { get; set; }

        public AccountStatus? Status { get; set; }

        [SpaceProperty(StorageType = StorageType.Binary)]
        public Address Address { get; set; }
    }
}
