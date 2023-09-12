using BillBuddy.Common.Utils;
using GigaSpaces.XAP.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.UI.Reports
{
    public class CountPaymenByCategoryReducer : IRemoteResultReducer
    {

        public object Reduce(SpaceRemotingResultsCollection results, ISpaceRemotingInvocation invocation)
        {
            int totalCountOfPayments = 0;

            // Each result is an array of events. Each result is from a single partition. 

            foreach (SpaceRemotingResult result in results)
            {
                if (result.Exception != null)
                {
                    // just log the fact that there was an exception

                    Utility.LogError(result.Exception, "Executor Remoting Exception [" + result.Exception + "]");

                    continue;
                }
                totalCountOfPayments += (int)result.Result;
            }

            return totalCountOfPayments;
        }
    }
}
