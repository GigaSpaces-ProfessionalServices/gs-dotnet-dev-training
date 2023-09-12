using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.XAP.Remoting;

namespace BillBuddy.UI.Reports
{
    public class PlatinumMerchantReducer : IRemoteResultReducer
    {
        public object Reduce(SpaceRemotingResultsCollection results, ISpaceRemotingInvocation invocation)
        {
            Utility.LogHeader("Starting PlatinumMerchantReducer");

            List<Merchant> merchants = new List<Merchant>();

            // Each result is an array of events. Each result is from a single partition.        
            foreach (SpaceRemotingResult result in results)
            {
                if (result.Exception != null)
                {
                    // just log the fact that there was an exception
                    Utility.LogMessage("Executor Remoting Exception [" + result.Exception + "]");

                    continue;
                }
                merchants.AddRange((Merchant[])result.Result);
            }


            merchants.Sort((p1, p2) =>
                {
                    return p2.FeeAmount.Value.CompareTo(p1.FeeAmount.Value);
                });

            // If the number of results needed is less than the number of events that were reduced, then
            // return a sublist. Otherwise, return the entire list of events.
            Merchant[] platinumMerchant;

            if (merchants.Count < 5)
            {
                platinumMerchant = new Merchant[merchants.Count];
                merchants.CopyTo(platinumMerchant);
            }
            else
            {
                platinumMerchant = merchants.Take(5).ToArray();
            }
            return platinumMerchant;
        }
    }

}
