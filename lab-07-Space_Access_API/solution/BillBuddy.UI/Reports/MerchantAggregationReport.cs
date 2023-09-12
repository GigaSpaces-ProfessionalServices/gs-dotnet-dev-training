using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core;
using BillBuddy.Common.Utils;
using BillBuddy.Common.Entities;

namespace BillBuddy.UI.Reports
{
    class MerchantAggregationReport
    {
        private ISpaceProxy _spaceProxy;

        public MerchantAggregationReport(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void Run()
        {
            Utility.LogHeader("Merchants Aggregation Report");

            int merchantCount = _spaceProxy.Count(new Merchant());
            // Select a Random Merchant
            int randomMerchant = new Random().Next(1,merchantCount);
            Utility.LogHeader("Selected merchant id is:" + randomMerchant);

            Merchant merchant = _spaceProxy.ReadById<Merchant>(randomMerchant);
            SqlQuery<Payment> query = new SqlQuery<Payment>("ReceivingMerchantId=?");
            query.SetParameter(1,randomMerchant);

            AggregationSet aggregationSet = new AggregationSet();
            aggregationSet.MaxEntry("PaymentAmount");
            aggregationSet.MinEntry("PaymentAmount");
            aggregationSet.Sum("PaymentAmount");
            aggregationSet.Average("PaymentAmount");
            aggregationSet.MinValue("PaymentAmount");
            aggregationSet.MaxValue("PaymentAmount");

            IAggregationResult result = _spaceProxy.Aggregate(query, aggregationSet);

            if (result.Get(2) != null) {
                Payment maxPayment = (Payment)result.Get(0);
                Payment minPayment = (Payment)result.Get(1);
                double sum = (double)result.Get(2);
                double average = (double)result.Get(3);
                double min = (double)result.Get(4);
                double max = (double)result.Get(5);
                Utility.LogMessage("Report for Merchant ID {0}, name: {1}", merchant.MerchantAccountId, merchant.Name);
                Utility.LogMessage("Avarage Payment size is: {0}", average);
                Utility.LogMessage("Sum of all Payments is: {0}", sum);
                Utility.LogMessage("Minimum Payment made is {0} on date {1} ", min, minPayment.CreatedDate);
                Utility.LogMessage("Maximum Payment made is {0} on date {1} ", max, maxPayment.CreatedDate);
            } else {
                Utility.LogMessage("Report for Merchant ID {0}, name: {1}", merchant.MerchantAccountId, merchant.Name);
                Utility.LogMessage("No payments were made yet.");
            }



            }
        }
}
