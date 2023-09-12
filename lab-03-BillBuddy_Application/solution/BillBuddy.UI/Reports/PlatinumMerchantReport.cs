using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using BillBuddy.Common.Contracts;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.XAP.Remoting.Executors;
using GigaSpaces.Core;

namespace BillBuddy.UI.Reports
{
    public class PlatinumMerchantReport
    {
        private ISpaceProxy _spaceProxy;

        public PlatinumMerchantReport(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void ExecuteReport()
        {
            Utility.LogHeader("Starting PlatinumMerchantReport");

            ExecutorBroadcastRemotingProxyBuilder<IPlatinumMerchantService> proxyBuilder = new ExecutorBroadcastRemotingProxyBuilder<IPlatinumMerchantService>(_spaceProxy);

            proxyBuilder.ResultReducer = new PlatinumMerchantReducer();

            IPlatinumMerchantService platinumMerchantService = proxyBuilder.CreateProxy();

            Merchant[] merchants = platinumMerchantService.FindPlatinumMerchants();

            foreach (Merchant merchant in merchants)
            {
                Utility.LogMessage("Merchant Name: {0} Fee Amount: {1}", merchant.Name, merchant.FeeAmount);
            }

            Utility.LogHeader("Finished PlatinumMerchantReport");
        }
    }
}
