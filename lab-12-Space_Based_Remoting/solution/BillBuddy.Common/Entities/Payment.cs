using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core.Metadata;

namespace BillBuddy.Common.Entities
{
        [SpaceClass]
    [Serializable]
    public class Payment
    {
        public Payment(string paymentId)
        {
            this.PaymentId = paymentId;
        }

        public Payment() { }

        [SpaceID(AutoGenerate = true)]
        public string PaymentId { get; set; }

        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public int? PayingAccountId { get; set; }

        [SpaceRouting]
        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public int? ReceivingMerchantId { get; set; }

        public string Description { get; set; }

        public TransactionStatus? Status { get; set; }

        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public DateTime? CreatedDate { get; set; }

        public double? PaymentAmount { get; set; }

    }
}
