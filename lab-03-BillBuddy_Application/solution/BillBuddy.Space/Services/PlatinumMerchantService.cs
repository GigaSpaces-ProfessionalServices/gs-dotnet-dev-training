using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.Remoting;
using BillBuddy.Common.Contracts;
using GigaSpaces.Core;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;

namespace BillBuddy.Space.Services
{
    [SpaceRemotingService]
    [BasicProcessingUnitComponent(Name = "PlatinumMerchantService")]
    public class PlatinumMerchantService : IPlatinumMerchantService
    {

        private ISpaceProxy _spaceProxy;

        [ContainerInitialized]
        public void Init(BasicProcessingUnitContainer container)
        {
            _spaceProxy = container.GetSpaceProxy("BillBuddySpace");
        }

        public Merchant[] FindPlatinumMerchants()
        {
            Utility.LogHeader("Start Platinum Merchant service");

            SqlQuery<Merchant> platinumMerchantQuery = new SqlQuery<Merchant>(" order by FeeAmount desc");

            Merchant[] platinumMerchants = _spaceProxy.ReadMultiple<Merchant>(platinumMerchantQuery, 5);

            Utility.LogHeader("Finish Platinum Merchant service");

            return platinumMerchants;
        }
    }
}
