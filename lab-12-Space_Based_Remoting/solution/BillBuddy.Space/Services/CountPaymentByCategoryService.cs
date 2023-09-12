using BillBuddy.Common.Contracts;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.Core;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using GigaSpaces.XAP.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.Space.Services
{
    [SpaceRemotingService]
    [BasicProcessingUnitComponent(Name = "CountPaymentByCategoryService")]
    public class CountPaymentByCategoryService : ICountPaymentsByCategoryService
    {
        private ISpaceProxy _spaceProxy;

        [ContainerInitialized]
        public void Init(BasicProcessingUnitContainer container)
        {
            _spaceProxy = container.GetSpaceProxy("BillBuddySpace");
        }

        public int findPaymentCountByCategory(CategoryType categoryType)
        {
            Utility.LogMessage("Start findPaymentCountByCategory service");
            int paymentCount = 0;

            // Search the space for all Merchant per selected category - prepare the query template 

            Merchant merchantTemplate = new Merchant();
            merchantTemplate.Category = categoryType;

            // Execute Merchant query to the space

            Merchant[] merchantList = _spaceProxy.ReadMultiple(merchantTemplate, int.MaxValue);

            // In case Merchant query result set has more then one Merchant

            if (merchantList.Length > 0)
            {

                Payment paymentTemplate;
                Utility.LogMessage("Number of merchants found: " + merchantList.Length + " for category: " + categoryType);

                // Loop thru the list of Merchants

                foreach (Merchant merchant in merchantList)
                {

                    // Prepare Payment query template to count number of Payments per each merchant

                    paymentTemplate = new Payment();
                    paymentTemplate.ReceivingMerchantId = merchant.MerchantAccountId;

                    // Execute query to count number of Payments per Merchant

                    paymentCount += _spaceProxy.Count(paymentTemplate);
                }

                Utility.LogMessage("Number of payments found: " + paymentCount + " for category: " + categoryType);
            }
            else
            {
                Utility.LogMessage("No Merchants found for category: " + categoryType + " !!");
            }

            Utility.LogMessage("findPaymentCountByCategory - End Execute.");
            return paymentCount;
        }
    }

}
