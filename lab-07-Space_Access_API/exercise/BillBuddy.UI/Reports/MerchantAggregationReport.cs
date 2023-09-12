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

            // TODO: write an Aggregation Set Query to extract Payment aggregation information for payment for the random Merchant.
            // USE: MaxEntry, MinEntry, Sum, Average, MinValue & MaxValue

            // TODO: Use spaceProxy.Aggregate function to run query & aggregation set
            // Grab the results into IAggregationResult
            
            // TODO: Extract aggregation result information
            // Extract: maxPayment, minPayment,sum, average, min, max into the already defined local variables
            
            Payment maxPayment=null;
            Payment minPayment=null;
            double sum=0;
            double average=0;
            double min=0;
            double max=0;
            Utility.LogMessage("Report for Merchant ID {0}, name: {1}", merchant.MerchantAccountId, merchant.Name);
            Utility.LogMessage("Avarage Payment size is: {0}", average);
            Utility.LogMessage("Sum of all Payments is: {0}", sum);
            Utility.LogMessage("Minimum Payment made is {0} on date {1} ", min, minPayment.CreatedDate);
            Utility.LogMessage("Maximum Payment made is {0} on date {1} ", max, maxPayment.CreatedDate);

            }
        }
}
