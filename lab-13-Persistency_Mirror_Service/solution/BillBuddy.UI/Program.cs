using System;
using System.Collections.Generic;
using System.Windows.Forms;

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