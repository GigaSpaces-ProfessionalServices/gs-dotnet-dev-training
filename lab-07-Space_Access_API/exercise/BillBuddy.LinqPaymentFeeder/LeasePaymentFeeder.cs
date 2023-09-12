using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core;
using GigaSpaces.Core.Linq;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;

using System.Timers;
using BillBuddy.Common.Utils;
using BillBuddy.Common.Entities;


namespace BillBuddy.LinqPaymentFeeder
{

    [BasicProcessingUnitComponent(Name = "LeasePaymentFeeder")]
    public class LeasePaymentFeeder : IDisposable
    {
        private double defaultDelay = 5000;

        // TODO: write number of payments object

        private ISpaceProxy gigaSpace;

        private PaymentFeederTask paymentFeederTask;

        [ContainerInitialized]
        public void Construct(BasicProcessingUnitContainer container)
        {
            Utility.LogMessage("Starting PaymentWriter");

            gigaSpace = container.GetSpaceProxy("BillBuddySpace");


            User[] users = (from u in gigaSpace.Query<User>()
                            select u).ToArray();

            if ((users == null) || (users.Length == 0))
            {
                Utility.LogMessage("Could not find users, did you write any?");
                throw new Exception("Could not find users, did you write any?");
            }


            Merchant[] merchants = (from m in gigaSpace.Query<Merchant>()
                                    select m).ToArray();

            if ((merchants == null) || (merchants.Length == 0))
            {
                Utility.LogMessage("Could not find merchants, did you write any?");
                throw new Exception("Could not find merchants, did you write any?");
            }

            paymentFeederTask = new PaymentFeederTask(gigaSpace, users, merchants, defaultDelay);

            paymentFeederTask.Start();
        }


        public void Dispose()
        {
            Utility.LogMessage("Stopping PaymentWriter");

            if (paymentFeederTask != null)
            {
                paymentFeederTask.Stop();
            }
        }

        public class PaymentFeederTask : Timer
        {
            private User[] users;
            private Merchant[] merchants;
            private ISpaceProxy _spaceProxy;

            public PaymentFeederTask(ISpaceProxy proxy, User[] users, Merchant[] merchants, double interval)
                : base(interval)
            {
                _spaceProxy = proxy;
                this.users = users;
                this.merchants = merchants;

                this.AutoReset = true;
                Elapsed += new ElapsedEventHandler(Run);
            }

            public void Run(object sender, ElapsedEventArgs e)
            {
                try
                {
                    //TODO: write payments array

                    Random randomizer = new Random();

                    //TODO: create loop to go over payments array.

                        // Pick a random user
                        User user = ((users != null) && (users.Length > 0))
                            ? users[(int)((users.Length - 1) * randomizer.NextDouble())] : null;

                        // Pick a random merchant
                        Merchant merchant = ((merchants != null) && (merchants.Length > 0))
                            ? merchants[(int)((merchants.Length - 1) * randomizer.NextDouble())] : null;


                        //Calendar calendar = Calendar.getInstance();
                        //calendar.setTimeInMillis(System.currentTimeMillis());
                        DateTime date = DateTime.Now;//calendar.getTime();

                        Double paymentAmount = randomizer.NextDouble() * 100;

                        // Check If user valid and have credit limit
                        if (user.Status != AccountStatus.ACTIVE)
                        {
                            Utility.LogMessage("User: {0} status is {1}", user.Name, user.Status);
                        }
                        else if (user.Balance - paymentAmount < user.CreditLimit)
                        {
                            Utility.LogMessage("User: {0} doesn't have credit.", user.Name);

                            double addUserBalance = randomizer.NextDouble() * 1000;

                            Utility.LogMessage("Add {0} to user balance", addUserBalance);

                            user.Balance = user.Balance + addUserBalance;
                            _spaceProxy.Write(user);

                        }
                        else
                        {

                            // Withdraw payment amount from user account
                            UpdateUserBalance(user, paymentAmount);

                            // Deposit payment amount to merchant account                    
                            UpdateMerchantReceipts(merchant, paymentAmount);


                            Payment payment = new Payment();

                            payment.PayingAccountId = user.UserAccountId;
                            payment.ReceivingMerchantId = merchant.MerchantAccountId;
                            payment.Description = merchant.Category.ToString();
                            payment.CreatedDate = date;
                            payment.PaymentAmount = randomizer.NextDouble() * 1000;
                            payment.Status = TransactionStatus.NEW;

                           //TODO: insert payment to array

                            Utility.LogMessage("TransactionWriterTask wrote new transaction between user: {0} and merchant: {1}", user.Name, merchant.Name);

                        }
                    

                    // TODO: Write the payment object

                }
                catch (Exception ex)
                {
                    Utility.LogMessage(ex.ToString());
                }


            }

            private void UpdateUserBalance(User user, Double paymentAmount)
            {
                Utility.LogMessage("TransactionWriterTask withdraw {0} from user: {1}", paymentAmount, user.Name);

                user.Balance = user.Balance - paymentAmount;

                IdQuery<User> idQuery = new IdQuery<User>(user.UserAccountId, user.UserAccountId);

                _spaceProxy.Change<User>(idQuery, new ChangeSet().Decrement("Balance", user.Balance.Value));

                Utility.LogMessage("TransactionWriterTask updates user balance. User: {0} new balance is {1}", user.Name, user.Balance);

            }

            private void UpdateMerchantReceipts(Merchant merchant, Double paymentAmount)
            {
                Utility.LogMessage("TransactionWriterTask deposit {0} to merchant: {1}", paymentAmount, merchant.Name);

                merchant.Receipts = merchant.Receipts + paymentAmount;

                IdQuery<Merchant> idQuery = new IdQuery<Merchant>(merchant.MerchantAccountId, merchant.MerchantAccountId);

                _spaceProxy.Change<Merchant>(idQuery, new ChangeSet().Increment("Receipts", merchant.Receipts.Value));

                Utility.LogMessage("TransactionWriterTask updates merchant receipt. Merchant: {0} new receipt is {1}", merchant.Name, merchant.Receipts);

            }
        }
    }

}
