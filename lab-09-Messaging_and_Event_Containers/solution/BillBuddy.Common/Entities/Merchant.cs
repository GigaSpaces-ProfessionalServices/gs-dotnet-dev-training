using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core.Metadata;

namespace BillBuddy.Common.Entities
{

    [SpaceClass]
    public class Merchant
    {
        public Merchant(int? merchantAccountId)
        {
            this.MerchantAccountId = merchantAccountId;
        }

        public Merchant() { }

        [SpaceID(AutoGenerate = false)]
        [SpaceRouting]
        public int? MerchantAccountId { get; set; }

        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public string Name { get; set; }

        public CategoryType? Category { get; set; }

        public AccountStatus? Status { get; set; }

        public double? Receipts { get; set; }

        public double? FeeAmount { get; set; }
    }
}
