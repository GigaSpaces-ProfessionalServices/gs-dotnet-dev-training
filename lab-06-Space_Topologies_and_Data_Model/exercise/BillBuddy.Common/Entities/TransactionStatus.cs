using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.Common.Utils
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
