using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillBuddy.Common.Contracts;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.XAP.Remoting.Executors;
using GigaSpaces.Core;

namespace BillBuddy.UI.Reports
{
    public class Top10PaymentFinder
    {
        private ISpaceProxy _spaceProxy;

        public Top10PaymentFinder(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void ExecuteReport()
        {
            Utility.LogHeader("Starting Top10PaymentFinder");

            ExecutorBroadcastRemotingProxyBuilder<IPaymentService> proxyBuilder = new ExecutorBroadcastRemotingProxyBuilder<IPaymentService>(_spaceProxy);

            proxyBuilder.ResultReducer = new Top10PaymentReducer();

            IPaymentService paymentService = proxyBuilder.CreateProxy();

            Payment[] payments = paymentService.FindTop10Payments();

            if (payments != null)
            {
                foreach (Payment payment in payments)
                {                   
                    Utility.LogMessage("Payment Id: {0} Payment Amount: {1}", payment.PaymentId, payment.PaymentAmount);
                }
            }

            Utility.LogHeader("Finished Top10PaymentFinder");
        }
    }

}
