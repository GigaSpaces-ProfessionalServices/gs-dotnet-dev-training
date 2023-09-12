using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GigaSpaces.XAP.ProcessingUnit.Containers;
using System.Threading;

namespace BillBuddy.UI
{
    class Command
    {
        public string Id { get; set; }
        public bool IsInitialized { get; set; }
        public TextBoxLogger Logger { get; set; }
        public ProcessingUnitContainerHost PU { get; set; }
    }


}
