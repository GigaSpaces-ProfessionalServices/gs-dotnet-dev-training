using BillBuddy.Common.Contracts;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.Core;
using GigaSpaces.XAP.Remoting.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.UI.Reports
{
    public class CountPaymentByCategoryReport
    {
        private ISpaceProxy _spaceProxy;

        public CountPaymentByCategoryReport(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void ExecuteReport()
        {
            int result = 0; 
            Utility.LogMessage("Starting CountPaymentByCategoryReport");

            CategoryType categoryType = Utility.FetchRandomEnumValue<CategoryType>();

            Utility.LogMessage("Search for Payment Count for the following category: " + categoryType);

            //TODO: use the remote service to retrieve Payment count in a specific Category
            

            Utility.LogMessage("Payment Count for the following category: " + categoryType + " is: " + result);

            Utility.LogMessage("Finished CountPaymentByCategoryReport");
        }
    }
}
