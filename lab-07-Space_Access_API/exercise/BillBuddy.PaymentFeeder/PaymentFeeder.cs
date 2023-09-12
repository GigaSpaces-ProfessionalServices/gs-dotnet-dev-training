using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using GigaSpaces.Core;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;


namespace BillBuddy.PaymentFeeder
{
    public class PaymentFeeder
    {
        private ISpaceProxy _spaceProxy;

        private int userCount = 0;
        private int merchantCount = 0;
        private User user;
        private Merchant merchant;

        public PaymentFeeder()
        {
            // _spaceProxy = GigaSpacesFactory.FindSpace(Utility.BillBuddyUrl+"&timeout=500");
            _spaceProxy = new SpaceProxyFactory(Utility.BillBuddySpaceName).Create();

            if (_spaceProxy == null)
                throw new ArgumentNullException("spaceProxy can not be null");

            Utility.LogMessage( "Starting PaymentWriter");

           

            //TODO: get the number of users from the space and keep that value in the userCount property
            //      You will require this value for Randomly choosing a User later (use userCount property)
           


            //TODO: get the number of merchants from the space and keep that value in the merchantCount property
            //      You will require this value for Randomly choosing a User later (use merchantCount property)

        }

        public void CreatePayment()
        {
            int userId = (int)(userCount * Utility.NextDouble);

            //TODO: read a random user

            int merchantId = (int)(merchantCount * Utility.NextDouble);

            //TODO: read a random merchant

            if (user != null && merchant != null)
            {
                DateTime date = DateTime.Now;

                Double paymentAmount = Math.Round(Utility.NextDouble * 100, 2);

                // Check If user valid and have credit limit
                if (user.Status != AccountStatus.ACTIVE)
                {
                    Utility.LogMessage( "User: {0} status is {1}.", user.Name, user.Status);
                }
                else if (user.Balance - paymentAmount < user.CreditLimit)
                {
                    Utility.LogMessage( "User: {0} doesn't have credit.", user.Name);

                    // TODO: Add a random amount to the user balance and make sure to update the user.

                }
                else
                {
                    // Withdraw payment amount from user account
                    UpdateUserBalance(user, paymentAmount);

                    // Deposit payment amount to merchant account                    
                    UpdateMerchantReceipts(merchant, paymentAmount);

                    // Create a Payment POCO and set it up.  
                    Payment payment = new Payment();

                    payment.PayingAccountId = user.UserAccountId;
                    payment.ReceivingMerchantId = merchant.MerchantAccountId;
                    payment.Description = merchant.Category.ToString();
                    payment.CreatedDate = date;
                    payment.PaymentAmount = Math.Round(Utility.NextDouble * 100, 2);
                    payment.Status = TransactionStatus.NEW;

                    // TODO: Write the payment object

                    Utility.LogMessage( "TransactionWriterTask wrote new transaction between user: {0} and merchant: {1}", user.Name, merchant.Name);
                }
            }
        }


        private void UpdateUserBalance(User user, Double paymentAmount)
        {
            Utility.LogMessage("TransactionWriterTask withdraw {0} from user: {1}", paymentAmount, user.Name);

            user.Balance = user.Balance - paymentAmount;

            //TODO: update user balance and write user to the space. use change API

            Utility.LogMessage("TransactionWriterTask updates user balance. User: {0} new balance is {1}", user.Name, user.Balance);

        }

        private void UpdateMerchantReceipts(Merchant merchant, Double paymentAmount)
        {
            Utility.LogMessage("TransactionWriterTask deposit {0} to merchant: {1}", paymentAmount, merchant.Name);

            merchant.Receipts = merchant.Receipts + paymentAmount;

            //TODO: update Merchant receipts and write merchant to the space. use change API

            Utility.LogMessage("TransactionWriterTask updates merchant receipt. Merchant: {0} new receipt is {1}", merchant.Name, merchant.Receipts);

        }
    }


}
