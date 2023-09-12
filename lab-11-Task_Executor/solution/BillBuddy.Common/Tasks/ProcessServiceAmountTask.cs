using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using GigaSpaces.Core.Executors;
using GigaSpaces.Core;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using BillBuddy.Common.Utils;
using BillBuddy.Common.Entities;

namespace BillBuddy.Common.Tasks
{
    [Serializable]
    public class ProcessServiceAmountTask : IDistributedSpaceTask<double, double>
    {
        public double Execute(ISpaceProxy spaceProxy, ITransaction tx)
        {

            Utility.LogMessage( "ProcessServiceAmountTask- Start Execute.");
            
            double processingFeeAmount = 0;

            ProcessingFee[] processingFees = spaceProxy.ReadMultiple(new ProcessingFee(), int.MaxValue);

            Utility.LogMessage( "Number of Processing Fees found: {0}", processingFees.Length);

            Utility.LogMessage( "MerchantByCategoryTask- End Execute.");
            
            foreach (ProcessingFee processingFee in processingFees)
            {
                processingFeeAmount += processingFee.Amount.Value;
            }

            return processingFeeAmount;
        }

        public double Reduce(SpaceTaskResultsCollection<double> results)
        {
            Utility.LogMessage( "MerchantByCategoryTask- Start reduce.");

            double processingFeeAmount = 0;

            foreach (SpaceTaskResult<double> result in results)
            {
                if (result.Exception != null)
                {
                    throw result.Exception;
                }

                processingFeeAmount += result.Result;
                           
            }

            Utility.LogMessage( "Processing Fee Amount is {0}", processingFeeAmount);

            Utility.LogMessage( "ProcessServiceAmountTask- End reduce.");

            return processingFeeAmount;
        }
    }

}
