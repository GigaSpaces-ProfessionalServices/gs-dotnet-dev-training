using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.Remoting;
using BillBuddy.Common.Contructs;
using GigaSpaces.Core;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;

namespace BillBuddy.Space.Services
{
    [SpaceRemotingService]
    [BasicProcessingUnitComponent(Name = "ServiceFinder")]
    public class ServiceFinder : IServiceFinder
    {
        private ISpaceProxy gigaSpace;

        [ContainerInitialized]
        public void Init(BasicProcessingUnitContainer container)
        {
            gigaSpace = container.GetSpaceProxy("BillBuddySpace");
        }

        public Payment[] FindTop10Payments()
        {
            Utility.LogMessage("Find Top 10 payment merchant over the space");

            SqlQuery<Payment> query = new SqlQuery<Payment>(" order by paymentAmount desc");

            Payment[] payments = gigaSpace.ReadMultiple<Payment>(query, 10);

            Utility.LogMessage("Found: {0} payments.", payments.Length);

            return payments;
        }

        public Merchant[] FindTop5MerchantFeeAmount()
        {
            Utility.LogMessage("Find Top 5 merchant fee amount over the space");

            SqlQuery<Merchant> query = new SqlQuery<Merchant>(" order by feeAmount desc");

            Merchant[] merchants = gigaSpace.ReadMultiple<Merchant>(query, 5);

            Utility.LogMessage("Found: {0} merchants.", merchants.Length);

            return merchants;

        }
    }

}
