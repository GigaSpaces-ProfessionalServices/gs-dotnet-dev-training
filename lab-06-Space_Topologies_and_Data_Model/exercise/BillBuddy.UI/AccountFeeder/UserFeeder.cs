using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using GigaSpaces.Core;
using BillBuddy.Common.Utils;

namespace BillBuddy.UI.AccountFeeder
{
    public class UserFeeder
    {
        private ISpaceProxy _spaceProxy;

        public UserFeeder(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void Run()
        {
            Utility.LogHeader("Starting User Feeder");

            int userAccountId = 1;

            foreach (String u in Config.UserList)
            {
                User templateUser = new User();

                templateUser.UserAccountId = userAccountId;

                User foundUser = _spaceProxy.Read(templateUser);

                if (foundUser == null)
                {
                    User user = new User();
                    user.Name = u;

                    user.Balance = Utility.NextDouble * 10000;
                    Double creditLimit = Utility.NextDouble * 10000;
                    creditLimit = creditLimit - (creditLimit % 1000);
                    user.CreditLimit = -1 * creditLimit;
                    user.Status = AccountStatus.ACTIVE;
                    user.UserAccountId = userAccountId;

                    //TODO: set the Address of the User (Choose any values you wish for all address attributes)

                    // User is not found, let's add it.
                    _spaceProxy.Write(user);
                    Utility.LogMessage("Added User object with name '{0}'", user.Name);
                }
                userAccountId++;
            }

            Utility.LogMessage("Stopping User Feeder");
        }
    }
}
