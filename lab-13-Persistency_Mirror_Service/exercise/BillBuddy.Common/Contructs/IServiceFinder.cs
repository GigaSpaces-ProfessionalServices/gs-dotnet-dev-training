using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillBuddy.Common.Entities;

namespace BillBuddy.Common.Contructs
{
    public interface IServiceFinder
    {

        Payment[] FindTop10Payments();

        Merchant[] FindTop5MerchantFeeAmount();
    }
}
