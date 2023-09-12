using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.Core;
using GigaSpaces.Core.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.Common.Tasks
{
    [Serializable]
    public class CountPaymentsByCategoryTask : IDistributedSpaceTask<long, int>
    {
        private CategoryType categoryType;

        public CountPaymentsByCategoryTask(CategoryType categoryType)
        {
            this.categoryType = categoryType;
        }

        public int Execute(ISpaceProxy spaceProxy, ITransaction tx)
        {
            Utility.LogMessage("CountPaymentsByCategoryTask - Start Execute.");
            Utility.LogMessage("Search Payments Count with category: " + categoryType);

            int paymentCount = 0;

            // Search the space for all Merchant per selected category - prepare the query
            Merchant merchantTemplate = new Merchant();
            merchantTemplate.Category = categoryType;

            // Execute Merchant query to the space
            Merchant[] merchantList = spaceProxy.ReadMultiple(merchantTemplate, int.MaxValue);

            // In case Merchant query result set has more then one Merchant
            if (merchantList.Length > 0)
            {


                Utility.LogMessage("Number of merchantList found: " + merchantList.Length + " for category: " + categoryType);

                // Loop thru the list of Merchants
                foreach (Merchant merchant in merchantList)
                {

                    // Prepare Payment query to count number of Payments per each merchant
                    Payment paymentTemplate = new Payment();
                    paymentTemplate.ReceivingMerchantId = merchant.MerchantAccountId;

                    // Execute query to count number of Payments per Merchant
                    paymentCount += spaceProxy.Count(paymentTemplate);
                }

                Utility.LogMessage("Number of Payments found: " + paymentCount + " for category: " + categoryType);
            }
            else
            {
                Utility.LogMessage("No Merchants found for category: " + categoryType + " !!");
            }

            Utility.LogMessage("CountPaymentsByCategoryTask- End Execute.");
            return paymentCount;

        }

        public long Reduce(SpaceTaskResultsCollection<int> results)
        {
            Utility.LogMessage("CountPaymentsByCategoryTask- Start Reduce.");
            long paymentCountPerCategoryInSpace = 0;

            // Loop thru all the list of results returning from the different partitions
            foreach (SpaceTaskResult<int> result in results)
            {
                if (result.Exception != null)
                {
                    throw result.Exception;
                }

                // Sum all transaction retrieved per partition to a single count
                paymentCountPerCategoryInSpace += result.Result;
            }

            if (paymentCountPerCategoryInSpace == 0)
            {
                Utility.LogMessage("No Payments were found for category " + categoryType + " PLease re-run CountPaymentsByCategoryTask project to select random category.");
            }

            Utility.LogMessage("CountPaymentsByCategoryTask- End Reduce.");

            return paymentCountPerCategoryInSpace;
        }
    }
}
