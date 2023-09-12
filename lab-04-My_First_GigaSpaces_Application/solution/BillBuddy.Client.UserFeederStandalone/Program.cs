using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core;
using BillBuddy.Common.Entities;

namespace BillBuddy.Client.UserFeederConfigurer
{
    class Program
    {
        static void Main(string[] args)
        {

            // This was the way we create a space proxy in the pre-XAP10 release
            //ISpaceProxy spaceProxy = GigaSpacesFactory.FindSpace("jini://*/*/BillBuddySpace?groups=$(XapNet.Groups)");

            ISpaceProxy spaceProxy = new SpaceProxyFactory("BillBuddySpace").Create(); 
            
            User user = new User();
            user.UserAccountId = 151273;
            user.Balance = 120000.87;
            user.CreditLimit = 20000.00;
            user.Name = "GigaSpaces";
            user.Status = AccountStatus.BLOCKED;

            spaceProxy.Write(user);


            User result = spaceProxy.ReadById<User>(151273);

            Console.WriteLine("User ID: " + result.UserAccountId);
            Console.WriteLine("User Name: " + result.Name);
            Console.WriteLine("User Balance: " + result.Balance);
            Console.WriteLine("User Status: " + result.Status);
        }
    }
}
