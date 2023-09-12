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

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

 
            Console.ReadLine();

            if (backupProcessorContainerHost != null)
                backupProcessorContainerHost.Dispose();
            if (primaryProcessorContainerHost != null)
                primaryProcessorContainerHost.Dispose();
        }
    }
}