using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using GigaSpaces.Core;
using BillBuddy.Common.Utils;
using BillBuddy.Common.Tasks;

namespace BillBuddy.UI.Reports
{
    public class ProcessServiceAmountFinder
    {
        private ISpaceProxy _spaceProxy;

        public ProcessServiceAmountFinder(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void ExecuteReport()
        {
            Utility.LogMessage("Calculate Process service amount from all processing fee");

            IAsyncResult<double> ar = _spaceProxy.BeginExecute(new ProcessServiceAmountTask(), null, null);

            double processingFeeAmount = _spaceProxy.EndExecute(ar);

            if (processingFeeAmount > 0.0)
            {
                Utility.LogMessage("Processing Fee Amount is: {0}", processingFeeAmount);
            }
            else if (processingFeeAmount == 0.0)
            {
                Utility.LogMessage("BillBuddy profit is 0 (ZERO), Please run the PaymentFeeder project.");
            }
        }
    }
}
