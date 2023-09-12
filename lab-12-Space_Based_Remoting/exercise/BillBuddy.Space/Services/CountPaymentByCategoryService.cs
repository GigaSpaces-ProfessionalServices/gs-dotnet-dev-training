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

            //TODO: query for the Payment count, how many payments were made under a specific category
            //HINT: find relevant Merchants in a specific category and then find a count of all their individual payments. 
            // Summarize them all into one single count.

            Utility.LogMessage("findPaymentCountByCategory - End Execute.");
            return paymentCount;
        }
    }

}
