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
  
    //1. TODO: Add attribute your Event Container
    [BasicProcessingUnitComponent(Name = "ProcessingFeeEventContainer")]
    public class ProcessingFeeEventContainer
    {
        private ISpaceProxy spaceProxy;

        [ContainerInitialized]
        public void init(BasicProcessingUnitContainer container)
        {
            spaceProxy = spaceProxy = container.GetSpaceProxy("BillBuddySpace");
        }

        // 2. TODO: EventTemplate = all Payments with status AUDITED. see TransactionStatus Enum 
        
        // 3. TODO: @SpaceDataEvent

        public Payment ProcessAuctions(Payment payment)
        {
            // 3.1 TODO: Read Merchant that got the payment
            // 3.2 TODO: Read the Merchant contract transactionPrecentFee property. Use projection API for reading only that property
            // 3.3 TODO: Calculate the ProcessingFeeAmount. see the contract variable make sure you initialize it with the Contract document
            // 3.5 TODO: Update the Merchant: add to the FeeAmount property the processing fee amount.
            // 3.6 TODO: Write Merchant 
            // 3.7 TODO: Write Processing Fee
            // 3.8 TODO: Set the status of Payment to 'PROCESSED'
            // 3.8 TODO: Make sure you remember to write the Payment (Hint: return) 
        }
    }

}


