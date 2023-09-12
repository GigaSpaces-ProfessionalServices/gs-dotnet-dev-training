using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core;
using GigaSpaces.Core.Executors;
using GigaSpaces.Core.Metadata;
 
using BillBuddy.Common.Utils;
using BillBuddy.Common.Entities;

namespace BillBuddy.Common.Tasks
{
    [Serializable]
    public class MerchantGetUserIdTask : ISpaceTask<HashSet<int>>
    {
        private int receivingMerchantId;

        public MerchantGetUserIdTask(int receivingMerchantId)
        {
            this.receivingMerchantId = receivingMerchantId;
        }

        public HashSet<int> Execute(ISpaceProxy spaceProxy, ITransaction tx)
        {
            Utility.LogMessage( "Search Payments for Merchant ID: {0}", receivingMerchantId);

            SqlQuery<Payment> query = new SqlQuery<Payment>("ReceivingMerchantId = ? ");

            query.SetParameter(1, receivingMerchantId);

            Payment[] payments = spaceProxy.ReadMultiple<Payment>(query, int.MaxValue);

            HashSet<int> userIds = new HashSet<int>();

            // Eliminate duplicate UserId
            if (payments != null)
            {
                for (int i = 0; i < payments.Length; i++)
                {
                    userIds.Add(payments[i].PayingAccountId.Value);
                }
            }

            return userIds;
        }

        //TODO: Add a routing Method and attribute accordingly

    }
}
