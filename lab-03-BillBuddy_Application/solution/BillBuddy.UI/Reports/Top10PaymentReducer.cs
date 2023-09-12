using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.XAP.Remoting;

namespace BillBuddy.UI.Reports
{
    public class Top10PaymentReducer : IRemoteResultReducer
    {
        public object Reduce(SpaceRemotingResultsCollection results, ISpaceRemotingInvocation invocation)
        {
            Utility.LogMessage("Starting Top10PaymentFinder");

            List<Payment> payments = new List<Payment>();

            // Each result is an array of events. Each result is from a single
            // partition.
            foreach (SpaceRemotingResult result in results)
            {
                if (result.Exception != null)
                {
                    // just log the fact that there was an exception
                    Utility.LogMessage("Executor Remoting Exception [{0}]", result.Exception);

                    continue;
                }

                payments.AddRange((Payment[])result.Result);
            }

            payments.Sort((p1, p2) =>
            {
                return p2.PaymentAmount.Value.CompareTo(p2.PaymentAmount.Value);
            });

            // If the number of results needed is less than the number of events
            // that were reduced, then
            // return a sublist. Otherwise, return the entire list of events.
            Payment[] top10Payments;

            if (payments.Count < 10)
            {
                top10Payments = new Payment[payments.Count];
                payments.CopyTo(top10Payments);
            }
            else
            {
                top10Payments = payments.Take(10).ToArray();
            }
            return top10Payments;
        }
    }

}
