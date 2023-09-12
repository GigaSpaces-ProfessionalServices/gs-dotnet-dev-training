using BillBuddy.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.Common.Contracts
{
    public interface ICountPaymentsByCategoryService
    {
        int findPaymentCountByCategory(CategoryType categoryType);
    }
}
