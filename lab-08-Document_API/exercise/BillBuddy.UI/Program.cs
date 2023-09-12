using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GigaSpaces.Core;
using BillBuddy.Common.Utils;

namespace BillBuddy.UI
{
	class Program
	{
		[STAThread]
		static void Main()
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;          
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                SpaceProxyFactory spf = new SpaceProxyFactory(Utility.BillBuddySpaceName);
                TimeSpan tp = new TimeSpan(0,0,1);
                spf.LookupTimeout = tp;
                ISpaceProxy spaceProxy = spf.Create();
                MessageBox.Show("Connection to BillBuddySpace was established successfully");
            }
            catch
            {
                MessageBox.Show("Connection to BillBuddySpace failed, please check if there is a BillBuddySpace deployed");
            }

			Application.Run(new MainForm());
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			MessageBox.Show(
				e.ExceptionObject.ToString(), 
				"Unhandled exception", 
				MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}