using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.Common.Entities
{
         public enum TransactionStatus
    {
        NEW,
        AUDITED,
        OPEN,
        CLOSED,
        CANCELLED,
        PROCESSED
    }
}
