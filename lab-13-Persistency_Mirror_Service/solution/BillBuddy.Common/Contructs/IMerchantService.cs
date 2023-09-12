using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using GigaSpaces.XAP.Remoting.Routing;
using BillBuddy.Common.Entities;

namespace BillBuddy.Common.Contructs
{
    public interface IMerchantService
    {

        double GetMerchantProfit([ServiceRouting("MerchantAccountId")] Merchant merchant);

        // Other option for Routing on remoting call
        //	Double getMerchantProfit(@Routing Integer merchantAccountId);

    }
}
