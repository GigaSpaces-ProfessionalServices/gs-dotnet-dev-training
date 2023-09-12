using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using GigaSpaces.Core;
using GigaSpaces.Core.Document;
using GigaSpaces.Core.Metadata;
using BillBuddy.Common.Utils;
using BillBuddy.Common.Entities;

namespace BillBuddy.UI.AccountFeeder
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
            Utility.LogMessage("Starting Merchant Feeder");
         
            RegisterContractType();

            int merchantAccountId = 1;
            
            foreach (string merchantName in Config.MerchantList)
            {
                Merchant templateMerchant = new Merchant();

                templateMerchant.MerchantAccountId = merchantAccountId;

                Merchant foundMerchant = _spaceProxy.Read(templateMerchant);

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

                    Utility.LogMessage( "Added Merchant object with name '{0}'", merchant.Name);

                    CreateMerchantContract(merchant.MerchantAccountId);
                }

                merchantAccountId++;
            }
            Utility.LogMessage( "Stopping Merchant Feeder");
        }

        private void CreateMerchantContract(int? merchantId)
        {
            // TODO: Create SpaceDocument with the terms between Merchant and BillBuddy 

        }
        private void RegisterContractType()
        {
            // TODO: Register ContractDocument SpaceDocument into Space
        }
    }

}
