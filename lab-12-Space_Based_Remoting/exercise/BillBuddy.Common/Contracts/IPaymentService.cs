using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillBuddy.Common.Entities;

namespace BillBuddy.Common.Contracts
{
    public interface IPaymentService
    {
         Payment[] findTop5PaymentsPerCategory(CategoryType categoryType);
    }
}
