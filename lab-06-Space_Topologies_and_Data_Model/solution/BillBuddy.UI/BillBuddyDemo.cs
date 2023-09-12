using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

using GigaSpaces.Core;
using GigaSpaces.Core.Exceptions;
using GigaSpaces.Core.Events;
using GigaSpaces.XAP.ProcessingUnit.Containers;
using System.IO.Pipes;
using System.Collections.Generic;
using System.Text;
using BillBuddy.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using BillBuddy.Common.Utils;

namespace BillBuddy.UI
{
    public class BillBuddyDemo
    {
        public TextBoxLogger Logger;

        public BillBuddyDemo(TextBoxLogger logger)
        {
            this.Logger = logger;
        }

        public bool IsConnected
        {
            get;
            set;
        }

        public string SafePath(string path)
        {
            if (path.StartsWith("\"") && path.EndsWith("\""))
                return path;

            return "\"" + path + "\"";
        }

        public Process RunGridServicePu(string path, EventHandler handler)
        {
            Process process = new Process();

            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = Path.Combine(Utility.BinPath, "PuInstance.exe");
            process.StartInfo.Arguments = SafePath(path);
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.CreateNoWindow = false;

            if (handler != null)
            {
                process.EnableRaisingEvents = true;
                process.Exited += new EventHandler(handler);
            }

            process.Start();

            return process;
        }

        internal void Clear()
        {
            Logger.Clear();
        }
    }
}
