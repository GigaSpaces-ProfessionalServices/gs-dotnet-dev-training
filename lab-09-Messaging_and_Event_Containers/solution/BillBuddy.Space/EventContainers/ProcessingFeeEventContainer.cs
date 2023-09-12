using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.Events;
using BillBuddy.Common.Utils;
using GigaSpaces.Core;
using BillBuddy.Common.Entities;
using GigaSpaces.Core.Document;
using GigaSpaces.XAP.Events.Polling;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;

namespace BillBuddy.Space.EventContainers
{
    //[EventDriven]
    [PollingEventDriven]
    [BasicProcessingUnitComponent(Name = "ProcessingFeeEventContainer")]
    public class ProcessingFeeEventContainer
    {
        private ISpaceProxy spaceProxy;

        [ContainerInitialized]
        public void init(BasicProcessingUnitContainer container)
        {
            spaceProxy = spaceProxy = container.GetSpaceProxy("BillBuddySpace");
        }

        [EventTemplate]
        SqlQuery<Payment> UnprocessedData()
        {
            Utility.LogMessage("Starting ProcessingFeeTransaction EventTemplate for Payment with NEW status.");
            Utility.LogMessage("templete will be more efficient but we use SQLQuery for course training.");

            SqlQuery<Payment> template = new SqlQuery<Payment>("Status = ?");

            template.SetParameter(1, TransactionStatus.AUDITED);

            return template;
        }


        [DataEventHandler]
        public Payment ProcessAuctions(Payment payment)
        {
            Utility.LogMessage("Starting ProcessingFeeTransaction SpaceDataEvent.");

            // Read Merchant Account
            Utility.LogMessage("Read Merchant Id: {0} account.", payment.ReceivingMerchantId);
            Merchant merchantTemplate = new Merchant();
            merchantTemplate.MerchantAccountId = payment.ReceivingMerchantId;

            Merchant merchant = spaceProxy.Read(merchantTemplate);


            // Read Contract Account
            Utility.LogMessage("Read Contract Id: {0} account.", payment.ReceivingMerchantId);

            SqlQuery<SpaceDocument> queryContract = new SqlQuery<SpaceDocument>("ContractDocument", "merchantId = ?")
                {Projections = new []{"transactionPrecentFee"}};

                   
            queryContract.SetParameter(1, payment.ReceivingMerchantId);

            SpaceDocument contract = spaceProxy.Read<SpaceDocument>(queryContract);

            // Get transactionPrecentFee 
            double transactionFeeAmount = ((double)contract["transactionPrecentFee"]) * payment.PaymentAmount.Value;

            // Withdraw payment amount from merchant account
            UpdateMerchantBalance(merchant, transactionFeeAmount);

            ProcessingFee processingFee = new ProcessingFee();

            processingFee.Description = payment.Description;
            processingFee.DependentPaymentId = payment.PaymentId;
            processingFee.PayingAccountId = merchant.MerchantAccountId;

            processingFee.Amount = transactionFeeAmount;

            processingFee.CreatedDate = DateTime.Now;

            processingFee.Status = TransactionStatus.PROCESSED;

            // Write the ProcessingFee object
            spaceProxy.Write(processingFee);

            // Set payment status
            payment.Status = TransactionStatus.PROCESSED;

            return payment;
        }

        private void UpdateMerchantBalance(Merchant merchant, Double transactionFeeAmount)
        {
            Utility.LogMessage("ProcessingFeeTransaction add {0} from merchant: {1}", transactionFeeAmount, merchant.Name);

            merchant.FeeAmount = merchant.FeeAmount + transactionFeeAmount;

            spaceProxy.Write(merchant);

            Utility.LogMessage("ProcessingFeeTransaction updates merchants transactionFeeAmount. Merchant: {0} new transactionFeeAmount is {1}"
                 , merchant.Name, merchant.FeeAmount);

        }
    }

}


