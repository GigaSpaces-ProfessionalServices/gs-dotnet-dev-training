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
            Utility.LogMessage("Starting CountPaymentByCategoryReport");

            CategoryType categoryType = Utility.FetchRandomEnumValue<CategoryType>();

            Utility.LogMessage("Search for Payment Count for the following category: " + categoryType);

            ExecutorBroadcastRemotingProxyBuilder<ICountPaymentsByCategoryService> proxyBuilder = new ExecutorBroadcastRemotingProxyBuilder<ICountPaymentsByCategoryService>(_spaceProxy);

            proxyBuilder.ResultReducer = new CountPaymenByCategoryReducer();

            ICountPaymentsByCategoryService iCategoryTransactionVolumeService = proxyBuilder.CreateProxy();

            int result = iCategoryTransactionVolumeService.findPaymentCountByCategory(categoryType);
            Utility.LogMessage("Payment Count for the following category: " + categoryType + " is: " + result);

            Utility.LogMessage("Finished CountPaymentByCategoryReport");
        }
    }
}
