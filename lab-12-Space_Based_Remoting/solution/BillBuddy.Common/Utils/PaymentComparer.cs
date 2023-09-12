using BillBuddy.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.Common.Utils
{
    public class PaymentComparer : IComparer<Payment>
    {
        public int Compare(Payment p1, Payment p2)
        {
            if (p1.PaymentAmount < p2.PaymentAmount)
                return 1;
            else if (p1.PaymentAmount > p2.PaymentAmount)
                return -1;
            else
                return 0;
        }
    }
}
