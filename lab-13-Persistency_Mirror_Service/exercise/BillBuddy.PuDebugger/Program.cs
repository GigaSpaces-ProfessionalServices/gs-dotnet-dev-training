using System;
using System.IO;

using GigaSpaces.Core;
using GigaSpaces.XAP.ProcessingUnit.Containers;
using BillBuddy.Common.Utils;


namespace BillBuddy.PuDebugger
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ProcessingUnitContainerHost primaryProcessorContainerHost = null;
            ProcessingUnitContainerHost backupProcessorContainerHost = null;
            ProcessingUnitContainerHost mirrorProcessorContainerHost = null;
            ProcessingUnitContainerHost paymentFeederContainerHost = null;

            try
            {
                GigaSpacesFactory.FindSpace(Utility.BillBuddyUrl + "&timeout=500");
            }
            catch
            {
                var spacePath = Path.GetFullPath(Path.Combine(Utility.DeploymentPath, "BillBuddy.Space"));

                ClusterInfo primaryClusterInfo = new ClusterInfo("partitioned-sync2backup", 1, null, 1, 1);
                ClusterInfo backupClusterInfo = new ClusterInfo("partitioned-sync2backup", 1, 1, 1, 1);

                primaryProcessorContainerHost = new ProcessingUnitContainerHost(spacePath, primaryClusterInfo, null);
                backupProcessorContainerHost = new ProcessingUnitContainerHost(spacePath, backupClusterInfo, null);
            }

            Console.WriteLine("Press any key to run Mirror Service.");
            Console.ReadKey();

            try
            {
                GigaSpacesFactory.FindSpace("jini://*/mirror-service_container/mirror-service?groups=$(XapNet.Groups)");
            }
            catch
            {
                var mirrorPath = Path.GetFullPath(Path.Combine(Utility.DeploymentPath, "BillBuddy.Persistency"));
                mirrorProcessorContainerHost = new ProcessingUnitContainerHost(mirrorPath, null, null);
            }

            Console.WriteLine("Press any key to run Payment Feeder.");
            Console.ReadKey();


            try
            {
                GigaSpacesFactory.FindSpace("/./BillBuddy.PaymentFeeder?groups=$(XapNet.Groups)&timeout=500");
            }
            catch
            {
                var paymentFeederPath = Path.GetFullPath(Path.Combine(Utility.DeploymentPath, "BillBuddy.PaymentFeeder"));
                paymentFeederContainerHost = new ProcessingUnitContainerHost(paymentFeederPath, null, null);
            }

            Console.ReadLine();

            if (paymentFeederContainerHost != null)
                paymentFeederContainerHost.Dispose();
            mirrorProcessorContainerHost.Dispose();

            if (backupProcessorContainerHost != null)
                backupProcessorContainerHost.Dispose();
            if (primaryProcessorContainerHost != null)
                primaryProcessorContainerHost.Dispose();
        }
    }
}