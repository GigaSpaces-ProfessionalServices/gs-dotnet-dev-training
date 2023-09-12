using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core.Metadata;

namespace BillBuddy.Common.Entities
{
    [SpaceClass(Persist = true)]
    [Serializable]
    public class ProcessingFee
    {
        public ProcessingFee(string processingFeeId)
        {
            this.ProcessingFeeId = processingFeeId;
        }

        public ProcessingFee() { }

        [SpaceID(AutoGenerate = true)]
        public string ProcessingFeeId { get; set; }

        [SpaceRouting]
        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public int? PayingAccountId { get; set; }

        public string Description { get; set; }

        public double? Amount { get; set; }

        public TransactionStatus? Status { get; set; }

        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public DateTime? CreatedDate { get; set; }

        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public string DependentPaymentId { get; set; }
    }

}
