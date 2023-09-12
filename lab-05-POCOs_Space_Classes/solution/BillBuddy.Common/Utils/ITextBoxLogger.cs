using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.Common.Utils
{
    public interface ITextBoxLogger
    {
        void LogMainHeader(String title);
        void LogHeader(String title);
        void LogError(String message, Exception ex);
        void LogMessage(String text, params object[] args);
    }
}
