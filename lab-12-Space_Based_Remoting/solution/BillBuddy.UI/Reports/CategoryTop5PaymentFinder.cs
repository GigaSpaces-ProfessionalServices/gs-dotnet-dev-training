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
    public class CategoryTop5PaymentFinder
    {
        private ISpaceProxy _spaceProxy;

        public CategoryTop5PaymentFinder(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void ExecuteReport()
        {
            Utility.LogMessage("Starting CategoryTop5PaymentFinder");

            CategoryType categoryType = Utility.FetchRandomEnumValue<CategoryType>();

            ExecutorBroadcastRemotingProxyBuilder<IPaymentService> proxyBuilder = new ExecutorBroadcastRemotingProxyBuilder<IPaymentService>(_spaceProxy);

            proxyBuilder.ResultReducer = new CategoryTop5PaymentReducer();

            IPaymentService paymentService = proxyBuilder.CreateProxy();

            Payment[] payments = paymentService.findTop5PaymentsPerCategory(categoryType);

            if (payments != null)
            {
                Utility.LogMessage("Category: " + categoryType);
                foreach (Payment payment in payments)
                {
                    Utility.LogMessage("Payment Id: " + payment.PaymentId + " Payment Amount: " + payment.PaymentAmount);
                }
            }
            Utility.LogMessage("Finished CategoryTop5PaymentFinder");
        }
    }
}
