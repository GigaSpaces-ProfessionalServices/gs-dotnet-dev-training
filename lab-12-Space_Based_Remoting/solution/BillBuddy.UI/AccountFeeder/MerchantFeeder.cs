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
            DocumentProperties documentProperties = new DocumentProperties();

            // 1. Create the properties:
            documentProperties["transactionPrecentFee"] = Utility.NextDouble / 10;
            documentProperties["contractDate"] = DateTime.Now;
            documentProperties["merchantId"] = merchantId;

            // 2. Create the document using the type name and properties: 
            SpaceDocument document = new SpaceDocument("ContractDocument", documentProperties);

            // 3. Write the document to the space:
            _spaceProxy.Write(document);

            Utility.LogMessage( "Added MerchantContract object with id '{0}'", document["id"]);

            Utility.LogMessage("Stopping Merchant Feeder");
        }
        private void RegisterContractType()
        {
            // Create type descriptor:
            SpaceTypeDescriptorBuilder typeDescriptor = new SpaceTypeDescriptorBuilder("ContractDocument");
            typeDescriptor.SetIdProperty("id", true);
            typeDescriptor.AddFixedProperty("merchantId", typeof(int));
            typeDescriptor.SetRoutingProperty("merchantId");

            // Register type:
            _spaceProxy.TypeManager.RegisterTypeDescriptor(typeDescriptor.Create());
        }
    }

}
