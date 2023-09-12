using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using GigaSpaces.Core;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using BillBuddy.Common.Contracts;
using GigaSpaces.XAP.Remoting.Executors;

namespace BillBuddy.UI.Reports
{

    public class MerchantProfitReport
    {
        private ISpaceProxy _spaceProxy;

        public MerchantProfitReport(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void ExecuteReport()
        {
            Utility.LogHeader("Starting Merchant Profit Report");

            //TODO: add Executor to use the space remote service
           
            

            Double merchantProfit = 0d;
            Random random = new Random();

            int merchantCount = _spaceProxy.Count(new Merchant());

            if (merchantCount == 0)
            {
                Utility.LogMessage("Could not find merchants, did you write any?");
                throw new Exception("Could not find merchants, did you write any?");
            }

            //TODO:	Select a random merchant (replace teh 0 with a random formula)

            int merchantId = 0; //TODO:	Select a random merchant


            Merchant merchant = _spaceProxy.ReadById<Merchant>(merchantId);

            if (merchant != null)
            {
                //TODO: execute the merchantProfitService.
                //TODO: HINT - use -  interfaceName.method

                Utility.LogMessage("Merchant Name: {0} Profit Amount is: {1}", merchant.Name, merchantProfit);
            }

            Utility.LogHeader("Finished Merchant Profit Report");
        }
    }

}
