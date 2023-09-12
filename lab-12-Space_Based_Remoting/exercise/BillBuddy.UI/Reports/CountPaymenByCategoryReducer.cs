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

            //TODO: Sum all results to a single score that sums them all into totalCountOfPayments

            return totalCountOfPayments;
        }
    }
}
