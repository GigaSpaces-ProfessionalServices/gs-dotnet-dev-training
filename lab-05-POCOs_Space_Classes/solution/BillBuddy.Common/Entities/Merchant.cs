using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core.Metadata;

namespace  BillBuddy.Common.Entities
{

    [SpaceClass]
    public class Merchant
    {
        public Merchant(int? merchantAccountId)
        {
            MerchantAccountId = merchantAccountId;
        }

        public Merchant()
        {
        }

        [SpaceID(AutoGenerate = false)]
        [SpaceRouting]
        public int? MerchantAccountId { get; set; }

        public string Name { get; set; }

        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public CategoryType? Category { get; set; }

        public AccountStatus? Status { get; set; }

        public double? Receipts { get; set; }

        public double? FeeAmount { get; set; }
    }
}
