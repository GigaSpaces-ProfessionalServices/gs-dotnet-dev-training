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
            Double processingFeeAmount = 0;
            // TODO: execute the task

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
