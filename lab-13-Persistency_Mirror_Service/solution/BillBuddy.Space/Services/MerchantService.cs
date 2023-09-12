using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.Remoting;
using BillBuddy.Common.Contructs;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.Core;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;

namespace BillBuddy.Space.Services
{
    [SpaceRemotingService]
    [BasicProcessingUnitComponent(Name = "MerchantService")]
    public class MerchantService : IMerchantService
    {
        private ISpaceProxy gigaSpace;

        [ContainerInitialized]
        public void Init(BasicProcessingUnitContainer container)
        {
            gigaSpace = container.GetSpaceProxy("BillBuddySpace");
        }

        public double GetMerchantProfit(Merchant merchant)
        {
            Utility.LogMessage("Staring getMerchantProfit to merchantId: {0}", merchant.MerchantAccountId);

            Payment payment = new Payment();
            payment.ReceivingMerchantId = merchant.MerchantAccountId;
            Payment[] payments = gigaSpace.ReadMultiple(payment);

            Double paymentAmount = 0d;
            for (int i = 0; i < payments.Length; i++)
            {
                paymentAmount += payments[i].PaymentAmount.Value;
            }


            ProcessingFee processingFee = new ProcessingFee();
            processingFee.PayingAccountId = merchant.MerchantAccountId;
            ProcessingFee[] processingFees = gigaSpace.ReadMultiple(processingFee);

            Double processingFeeAmount = 0d;
            for (int i = 0; i < processingFees.Length; i++)
            {
                processingFeeAmount += processingFees[i].Amount.Value;
            }

            Utility.LogMessage("merchantId profit is: {0}", (paymentAmount - processingFeeAmount));

            return paymentAmount - processingFeeAmount;
        }
    }

}
