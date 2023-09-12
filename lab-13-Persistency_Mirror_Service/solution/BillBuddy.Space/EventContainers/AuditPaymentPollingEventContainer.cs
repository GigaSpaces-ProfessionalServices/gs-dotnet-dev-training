using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.Events.Polling;
using GigaSpaces.XAP.Events;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using GigaSpaces.Core;

namespace BillBuddy.Space.EventContainers
{
    [PollingEventDriven]
    [TransactionalEvent(TransactionType = TransactionType.Distributed)]
    public class AuditPaymentPollingEventContainer
    {       
        [EventTemplate]
        public Payment FindNewPayments()
        {
            Utility.LogMessage("Starting AuditPaymentPollingEventContainer EventTemplate for Payment with NEW status.");

            Payment paymentTemplate = new Payment();
            paymentTemplate.Status = TransactionStatus.NEW;

            return paymentTemplate;
        }

        [DataEventHandler]
        public Payment ProcessPayment(Payment payment,ISpaceProxy spaceProxy,ITransaction tx)
        {

            Utility.LogMessage("Starting AuditPaymentPollingEventContainer SpaceDataEvent.");
            Utility.LogMessage("Payment ID: {0} Merchant ID: {1} User ID: {2} Payment Amount: {3}",
                payment.PaymentId, payment.ReceivingMerchantId, payment.PayingAccountId, payment.PaymentAmount);
            // Set payment status
            payment.Status = TransactionStatus.AUDITED;

            return payment;
        }
    }
}
