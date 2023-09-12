using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;

using GigaSpaces.Core;
using BillBuddy.Common.Utils;
using BillBuddy.Common.Entities;


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
            Utility.LogMessage("Starting User Feeder");

            int userAccountId = 1;

            foreach (string u in Config.UserList)
            {
                User templateUser = new User();

                templateUser.UserAccountId = userAccountId;

                User foundUser = _spaceProxy.Read(templateUser);

                if (foundUser == null)
                {
                    User user = new User();
                    user.Name = u;
                    user.Balance = Utility.NextDouble * 10000;
                    double creditLimit = Utility.NextDouble * 10000;
                    creditLimit = creditLimit - (creditLimit % 1000);
                    user.CreditLimit = -creditLimit;

                    user.Status = AccountStatus.ACTIVE;
                    user.UserAccountId = userAccountId;

                    Address tempAddress = new Address();

                    tempAddress.Country = Utility.FetchRandomEnumValue<CountryNames>();

                    tempAddress.City = "123Completed.com";
                    tempAddress.State = "GIGASPACES";
                    tempAddress.Street = "Here and There";
                    tempAddress.ZipCode = new Random().Next();

                    user.Address = tempAddress;

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
