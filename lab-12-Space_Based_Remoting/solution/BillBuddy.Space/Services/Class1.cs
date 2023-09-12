using BillBuddy.Common.Contracts;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;
using GigaSpaces.Core;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using GigaSpaces.XAP.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.Space.Services
{

    public class CategoryTop5PaymentReducer : IRemoteResultReducer
    {
        public object Reduce(SpaceRemotingResultsCollection results, ISpaceRemotingInvocation invocation)
        {
            List<Payment> payments = new List<Payment>();
            foreach (SpaceRemotingResult result in results)
            {
                if (result.Exception != null)
                {
                    Utility.LogError(result.Exception, "Executor Remoting Exception ["
                                  + result.Exception + "]");
                    continue;
                }
                payments.AddRange((Payment[])result.Result);
            }
            //Continued on next slide
            return null;
        }
    }
}
