using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using BillBuddy.Common.Tasks;

namespace BillBuddy.UI.Reports
{    
    public class MerchantGetUserIdFinder
    {
        private ISpaceProxy _spaceProxy;

        public MerchantGetUserIdFinder(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void ExecuteReport()
        {
            int merchantCount = _spaceProxy.Count(new Merchant());

            if (merchantCount == 0)
            {
                Utility.LogMessage("Could not find users, did you write any?");
            }
            else
            {
                int? merchantId = (int?)(merchantCount * Utility.NextDouble);

                Merchant merchant = _spaceProxy.ReadById<Merchant>(merchantId);

                if (merchant == null)
                {

                }
                else
                {
                    IAsyncResult<HashSet<int>> ar = _spaceProxy.BeginExecute(new MerchantGetUserIdTask(merchant.MerchantAccountId.Value), merchant.MerchantAccountId, null, null);

                    HashSet<int> userIds = _spaceProxy.EndExecute(ar);

                    if (userIds.Count == 0)
                    {
                        Utility.LogMessage("No user IDs were found for Merchant ID: '{0}', Please re-run the MerchantGetUserId project.", merchantId);
                    }
                    else if (userIds.Count > 0)
                    {
                        Utility.LogMessage("Merchant {0} :", merchant.Name);
                        foreach (int userId in userIds)
                        {
                            Utility.LogMessage("User Id is: {0}", userId);
                        }
                    }
                }
            }
        }
    }
}
