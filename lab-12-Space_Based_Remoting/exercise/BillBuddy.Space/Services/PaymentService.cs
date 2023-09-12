using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.Remoting;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using BillBuddy.Common.Contracts;
using GigaSpaces.Core;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;

namespace BillBuddy.Space.Services
{
    [SpaceRemotingService]
    [BasicProcessingUnitComponent(Name = "PaymentService")]
    public class PaymentService : IPaymentService
    {
        private ISpaceProxy _spaceProxy;

        [ContainerInitialized]
        public void Init(BasicProcessingUnitContainer container)
        {
            _spaceProxy = container.GetSpaceProxy("BillBuddySpace");
        }



        public Payment[] findTop5PaymentsPerCategory(CategoryType categoryType)
        {
            Utility.LogMessage("Find Top 5 Payments Per Category");
            List<Payment> allPayments = new List<Payment>();

            // Search the space for all Merchant per selected category - prepare the query template
            Payment[] top5Payments;
            Merchant merchantTemplate = new Merchant();
            merchantTemplate.Category = categoryType;

            // Execute Merchant query to the space

            Merchant[] merchantList = _spaceProxy.ReadMultiple(merchantTemplate, int.MaxValue);

            // In case Merchant query result set has more then one Merchant

            if (merchantList.Length > 0)
            {

                SqlQuery<Payment> paymentQuery;
                Utility.LogMessage("Number of merchants found: " + merchantList.Length + " for category: " + categoryType);

                // Loop thru the list of Merchants

                foreach (Merchant merchant in merchantList)
                {

                    // Prepare Payment query to get top 5 payments per merchant
                    paymentQuery = new SqlQuery<Payment>(
                            "ReceivingMerchantId = ? order by PaymentAmount desc");

                    paymentQuery.SetParameter(1, merchant.MerchantAccountId);

                    Payment[] payments = _spaceProxy.ReadMultiple<Payment>(paymentQuery, 5);

                    // Add top 5 payments to the summary collection of all top payments

                    allPayments.AddRange(payments);
                }
            }

            // Sort the entire payments results by payment amount from largest to smallest

            if (allPayments.Count > 0)
            {
                allPayments.Sort(new PaymentComparer());
            }

            // return only top 5 payments for the requested category

            if (allPayments.Count > 5)
            {
                top5Payments = allPayments.Take(5).ToArray();
            }
            else
            {
                top5Payments = allPayments.Take(allPayments.Count).ToArray(); ;
            }
            Utility.LogMessage("Found: " + top5Payments.Length + " payments for category: " + categoryType);

            return top5Payments;
        }

    }
}
