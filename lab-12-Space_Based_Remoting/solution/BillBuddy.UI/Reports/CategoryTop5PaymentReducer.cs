using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.XAP.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.UI.Reports
{
    public class CategoryTop5PaymentReducer : IRemoteResultReducer
    {
        public object Reduce(SpaceRemotingResultsCollection results, ISpaceRemotingInvocation invocation)
        {
            Payment[] top5Payments;
            Utility.LogMessage("Starting CategoryTop5PaymentFinder");

            List<Payment> payments = new List<Payment>();

            // Each result is an array of events. Each result is from a single
            // partition.ˇ

            foreach (SpaceRemotingResult result in results)
            {
                if (result.Exception != null)
                {

                    // just log the fact that there was an exception

                    Utility.LogError(result.Exception, "Executor Remoting Exception ["
                            + result.Exception + "]");
                    continue;
                }
                payments.AddRange((Payment[])result.Result);
            }

            // Sort the entire payments results by payment amount from largest to smalles

            payments.Sort(new PaymentComparer());

            // If the number of results needed is less than the number of events
            // that were reduced, then
            // return a sublist. Otherwise, return the entire list of events.

            if (payments.Count > 5)
            {
                top5Payments = payments.Take(5).ToArray();
            }
            else
            {
                top5Payments = payments.Take(payments.Count).ToArray(); ;
            }
            
            return top5Payments;
        }
    }
}
