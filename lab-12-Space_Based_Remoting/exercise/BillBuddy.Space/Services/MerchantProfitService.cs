using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.Remoting;
using BillBuddy.Common.Contracts;
using BillBuddy.Common.Utils;
using BillBuddy.Common.Entities;
using GigaSpaces.Core;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;

namespace BillBuddy.Space.Services
{
   
    [BasicProcessingUnitComponent(Name = "MerchantProfitService")]
    public class MerchantProfitService : IMerchantProfitService
    {
        private ISpaceProxy _spaceProxy;

        [ContainerInitialized]
        public void Init(BasicProcessingUnitContainer container)
        {
            _spaceProxy = container.GetSpaceProxy("BillBuddySpace");
        }
        //TODO: Implement the 'getMerchantProfit' method.

        public double GetMerchantProfit(int? merchantAccountId)
        {
            Utility.LogHeader("Start Merchant Profit service");

            Merchant merchant = null;

            double? merchantProfit = 0d;

            //TODO: Read the merchant and calculate the merchant's profit
            //TODO: The profit is calculated by:
            //TODO: merchantProfit = merchant.Receipts - merchant.FeeAmount

            Utility.LogHeader("Finish Merchant Profit service for {0}...", merchant.Name);

            return merchantProfit.Value;
        }
    }
}
