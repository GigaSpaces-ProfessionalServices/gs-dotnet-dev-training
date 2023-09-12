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

            Utility.LogMessage("Starting PaymentWriter");

            userCount = _spaceProxy.Count(new User());

            if (userCount == 0)
            {
                Utility.LogMessage("Could not find users, did you write any?");
                throw new Exception("Could not find users, did you write any?");
            }


            merchantCount = _spaceProxy.Count(new Merchant());

            if (merchantCount == 0)
            {
                Utility.LogMessage("Could not find merchants, did you write any?");
                throw new Exception("Could not find merchants, did you write any?");
            }
        }

        public void CreatePayment(ITransactionManager txMgr)
        {
            


            using (ITransaction tx = txMgr.Create())
            {
                try
                {
                    int userId = (int)(userCount * Utility.NextDouble);

                    user = _spaceProxy.ReadById<User>(new IdQuery<User>(userId, userId), tx);

                    int merchantId = (int)(merchantCount * Utility.NextDouble);

                    merchant = _spaceProxy.ReadById<Merchant>(new IdQuery<Merchant>(merchantId, merchantId), tx);

                    if (user != null && merchant != null)
                    {
                        DateTime date = DateTime.Now;

                        Double paymentAmount = Math.Round(Utility.NextDouble * 100, 2);

                        // Check If user valid and have credit limit
                        if (user.Status != AccountStatus.ACTIVE)
                        {
                            Utility.LogMessage("User: {0} status is {1}.", user.Name, user.Status);
                        }
                        else if (user.Balance - paymentAmount < user.CreditLimit)
                        {
                            Utility.LogMessage("User: {0} doesn't have credit.", user.Name);

                            double addUserBalance = Math.Round(Utility.NextDouble * 1000, 2);

                            Utility.LogMessage("Add {0} to user balance", addUserBalance);

                            user.Balance = user.Balance + addUserBalance;

                            _spaceProxy.Write(user, tx);

                        }
                        else
                        {
                            // Withdraw payment amount from user account
                            UpdateUserBalance(user, paymentAmount, tx);

                            // Deposit payment amount to merchant account                    
                            UpdateMerchantReceipts(merchant, paymentAmount, tx);

                            // Create a Payment POCO and set it up.  
                            Payment payment = new Payment();

                            payment.PayingAccountId = user.UserAccountId;
                            payment.ReceivingMerchantId = merchant.MerchantAccountId;
                            payment.Description = merchant.Category.ToString();
                            payment.CreatedDate = date;
                            payment.PaymentAmount = Math.Round(Utility.NextDouble * 100, 2);
                            payment.Status = TransactionStatus.NEW;

                            // Write the payment object
                            _spaceProxy.Write(payment, tx);

                            Utility.LogMessage("TransactionWriterTask wrote new transaction between user: {0} and merchant: {1}", user.Name, merchant.Name);
                        }

                        tx.Commit();
                    }
                }
                catch (Exception e)
                {
                    tx.Abort();
                    Utility.LogMessage("Transaction Aborted:"+ e);
                                            
                }
            }
        }

        private void UpdateUserBalance(User user, Double paymentAmount, ITransaction tx)
        {
            Utility.LogMessage("TransactionWriterTask withdraw {0} from user: {1}", paymentAmount, user.Name);

            user.Balance = user.Balance - paymentAmount;

            IdQuery<User> idQuery = new IdQuery<User>(user.UserAccountId, user.UserAccountId);

            _spaceProxy.Change<User>(idQuery, new ChangeSet().Decrement("Balance", user.Balance.Value), tx);

            Utility.LogMessage("TransactionWriterTask updates user balance. User: {0} new balance is {1}", user.Name, user.Balance);

        }

        private void UpdateMerchantReceipts(Merchant merchant, Double paymentAmount, ITransaction tx)
        {
            Utility.LogMessage("TransactionWriterTask deposit {0} to merchant: {1}", paymentAmount, merchant.Name);

            merchant.Receipts = merchant.Receipts + paymentAmount;

            IdQuery<Merchant> idQuery = new IdQuery<Merchant>(merchant.MerchantAccountId, merchant.MerchantAccountId);

            _spaceProxy.Change<Merchant>(idQuery, new ChangeSet().Increment("Receipts", merchant.Receipts.Value), tx);

            Utility.LogMessage("TransactionWriterTask updates merchant receipt. Merchant: {0} new receipt is {1}", merchant.Name, merchant.Receipts);

        }
    }


}
