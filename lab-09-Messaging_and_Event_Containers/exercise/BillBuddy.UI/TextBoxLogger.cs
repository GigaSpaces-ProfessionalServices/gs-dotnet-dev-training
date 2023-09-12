using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using BillBuddy.Common.Utils;

namespace BillBuddy.UI
{
    public class TextBoxLogger : ITextBoxLogger
    {
        private TextBoxBase _textBox;
        private Action<String> _safeLogMessageDelegate;

        public TextBoxLogger(TextBoxBase textBox)
        {
            this._textBox = textBox;
            this._safeLogMessageDelegate = new Action<String>(SafeLogMessage);
            Utility.Logger = this;
        }

        public void LogMainHeader(String title)
        {
            title = "*** " + title + " ***";
            String separator = new string('*', title.Length);

            LogMessage(String.Empty);
            LogMessage(separator);
            LogMessage(title);
            LogMessage(separator);
        }
        public void LogHeader(String title)
        {
            LogMessage(String.Empty);
            LogMessage("*** " + title);
        }
        public void LogError(String message, Exception ex)
        {
            LogMessage(message + ": " + ex.Message);
        }

        public void LogMessage(String text, params object[] args)
        {
            if (!String.IsNullOrEmpty(text))
            {
                String threadName = (_textBox.InvokeRequired
                    ? String.Format("[Thread #{0}] ", Thread.CurrentThread.ManagedThreadId)
                    : "");
                text = DateTime.Now.ToString("HH:mm:ss.fff ") + threadName + String.Format(text, args) + Environment.NewLine;
            }
            else
                text = Environment.NewLine;

            if (_textBox.InvokeRequired)
                _textBox.BeginInvoke(_safeLogMessageDelegate, text);
            else
                SafeLogMessage(text);
        }

        private void SafeLogMessage(String text)
        {
            _textBox.AppendText(text);
            _textBox.Refresh();
            _textBox.ScrollToCaret();
        }

        internal void Clear()
        {
            _textBox.Clear();
        }
    }
}
