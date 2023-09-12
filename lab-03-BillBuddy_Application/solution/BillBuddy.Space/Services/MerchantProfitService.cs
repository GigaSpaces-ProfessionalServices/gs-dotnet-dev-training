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
    [SpaceRemotingService]
    [BasicProcessingUnitComponent(Name = "MerchantProfitService")]
    public class MerchantProfitService : IMerchantProfitService
    {
        private ISpaceProxy _spaceProxy;

        [ContainerInitialized]
        public void Init(BasicProcessingUnitContainer container)
        {
            _spaceProxy = container.GetSpaceProxy("BillBuddySpace");
        }

        public double GetMerchantProfit(int? merchantAccountId)
        {
            Utility.LogHeader("Start Merchant Profit service");

            double? merchantProfit = 0d;

            Merchant merchant = _spaceProxy.ReadById<Merchant>(merchantAccountId);

            if (merchant != null)
            {
                merchantProfit = merchant.Receipts - merchant.FeeAmount;

                Utility.LogMessage("Profit for {0} is: {1}", merchant.Name, merchantProfit);
            }

            Utility.LogHeader("Finish Merchant Profit service for {0}...", merchant.Name);

            return merchantProfit.Value;
        }
    }
}
