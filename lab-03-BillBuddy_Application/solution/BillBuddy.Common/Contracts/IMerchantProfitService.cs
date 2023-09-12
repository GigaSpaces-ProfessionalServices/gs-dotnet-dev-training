using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.Remoting.Routing;

namespace BillBuddy.Common.Contracts
{
    public interface IMerchantProfitService
    {
        double GetMerchantProfit([ServiceRouting] int? merchantAccountId);
    }
}
