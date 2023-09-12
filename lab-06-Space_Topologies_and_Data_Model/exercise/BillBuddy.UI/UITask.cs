using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace BillBuddy.UI
{
    class UITask
    {
        private Thread owner;
        private Process process;
        private TextBoxLogger _logger;       

        private string deployPath;

        public bool IsRunning { get; private set; }
        public bool IsActive { get; private set; }
        public static string ExecutePath { get; set; }
        public string ArgumentPath { get; set; }

        public UITask(TextBoxLogger logger, string deployPath)
        {
            _logger = logger;
            this.deployPath = "\"" + deployPath+ "\"";
            Initialize();
        }

        public UITask(TextBoxLogger logger, string deployPath, params object[] args)
            : this(logger, deployPath)
        {
            foreach (var a in args)
            {
                process.StartInfo.Arguments += " " + a;
            }
        }

        private void Initialize()
        {
            process = new Process();

            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = Path.Combine(ExecutePath, "BillBuddy.PuDebugExecuter.exe");
            process.StartInfo.Arguments = deployPath;
            process.StartInfo.WorkingDirectory = ExecutePath;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.EnableRaisingEvents = true;

            process.ErrorDataReceived += new DataReceivedEventHandler(OutputDataReceived);
            process.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceived);
            owner = new Thread(new ThreadStart(StartProcess));
            owner.IsBackground = true;

        }

        public void Start()
        {
            if (owner.ThreadState == System.Threading.ThreadState.Stopped)
                Initialize();

            if (owner != null && owner.IsAlive == false)
                owner.Start();
        }

        public void Stop()
        {
            if (IsRunning)
            {
                process.Kill();
            }

            IsRunning = false;

        }

        private void StartProcess()
        {
            IsRunning = true;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();           
            process.WaitForExit();
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null && e.Data.Length > 0)
            {
                _logger.LogMessage(e.Data);
            }
        }

        internal void BringToFrom()
        {
            _logger.TextBox.BringToFront();
            IsActive = true;
        }

        internal void SendToBack()
        {
            IsActive = false;
            _logger.TextBox.SendToBack();
        }

        internal void Clear()
        {
            _logger.TextBox.Clear();
        }
    }
}
