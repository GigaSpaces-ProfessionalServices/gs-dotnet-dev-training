using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BillBuddy.UI;
using System.IO;
using BillBuddy.Common.Utils;
using GigaSpaces.Core;
using BillBuddy.UI.AccountFeeder;


namespace BillBuddy.UI
{
    public partial class MainForm : Form
    {
        private BillBuddyDemo _billBuddyDemo;

        public MainForm()
        {
            InitializeComponent();

            this._billBuddyDemo = new BillBuddyDemo(new TextBoxLogger(this.OutputTextBox));
        }



        private void ClearOutputButton_Click(object sender, EventArgs e)
        {
            _billBuddyDemo.Clear();
        }

        private void UsersFeederButton_Click(object sender, EventArgs e)
        {
            try
            {
                // ISpaceProxy spaceProxy = GigaSpacesFactory.FindSpace(Utility.BillBuddyUrl + "&timeout=500");
                //SpaceProxyFactory spf = new SpaceProxyFactory(Utility.BillBuddySpaceName);
                //spf.LookupGroups = "";
                ISpaceProxy spaceProxy = new SpaceProxyFactory(Utility.BillBuddySpaceName).Create();
                new UserFeeder(spaceProxy).Run();
            }
            catch (Exception ex)
            {
                _billBuddyDemo.Logger.LogError("Failed to find space", ex);
            }
        }

        private void MerchantsFeederButton_Click(object sender, EventArgs e)
        {
            try
            {
                // ISpaceProxy spaceProxy = GigaSpacesFactory.FindSpace(Utility.BillBuddyUrl + "&timeout=500");
                ISpaceProxy spaceProxy = new SpaceProxyFactory(Utility.BillBuddySpaceName).Create();
                new MerchantFeeder(spaceProxy).Run();
            }
            catch (Exception ex)
            {
                _billBuddyDemo.Logger.LogError("Failed to find space", ex);
            }
        }
    }
}
