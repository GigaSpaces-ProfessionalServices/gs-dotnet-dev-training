using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using GigaSpaces.Core;

using System.IO;
using System.IO.Pipes;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;


namespace BillBuddy.Space.AccountFeeder
{
    public class MerchantFeeder
    {
        private ISpaceProxy _spaceProxy;

        public MerchantFeeder(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void Run()
        {
            Utility.LogHeader( "Starting Merchant Feeder");           

            // merchantAccountId will serve as the Unique Identifier value
            int merchantAccountId = 1;
           
            Random randomer = new Random();
            // for each merchant in the merchantList do:
            foreach (string merchantName in Config.MerchantList)
            {
                // Check the merchant does not exist in the space already by trying to read it
                Merchant templateMerchant = new Merchant();

                templateMerchant.MerchantAccountId = merchantAccountId;

                Merchant foundMerchant = _spaceProxy.Read(templateMerchant);

                // If Merchant was not found then create the Merchant and write it to the space
                if (foundMerchant == null)
                {
                    Merchant merchant = new Merchant();

                    merchant.Name = merchantName;
                    merchant.Receipts = 0d;
                    merchant.FeeAmount = 0d;
                    // Select Random Category
                    merchant.Category = Utility.FetchRandomEnumValue<CategoryType>();
                    merchant.Status = AccountStatus.ACTIVE;
                    merchant.MerchantAccountId = merchantAccountId;
                    // Merchant is not found, let's add it.
                    _spaceProxy.Write(merchant);
                    //log.info(String.format("Added Merchant object with name '%s'", merchant.getName()));
                    Utility.LogMessage( "Added Merchant object with name '{0}'", merchant.Name);
                }

                merchantAccountId++;
            }

            Utility.LogMessage("Stopping Merchant Feeder");
        }
    }
}
