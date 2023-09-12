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

        public Payment[] FindTop10Payments()
        {
            Utility.LogHeader("Find Top 10 payment merchant over the space");

            SqlQuery<Payment> query = new SqlQuery<Payment>(" order by paymentAmount desc");

            Payment[] payments = _spaceProxy.ReadMultiple<Payment>(query, 10);

            Utility.LogHeader("Found: {0} payments.", payments.Length);

            return payments;
        }
    }
}
