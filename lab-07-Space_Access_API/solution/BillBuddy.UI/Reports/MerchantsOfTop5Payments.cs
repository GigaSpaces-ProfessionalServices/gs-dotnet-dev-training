using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.Core;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using BillBuddy.Common.Entities;
using BillBuddy.Common.Utils;

namespace BillBuddy.UI.Reports
{
    public class MerchantsOfTop5Payments
    {
        private ISpaceProxy _spaceProxy;

        public MerchantsOfTop5Payments(ISpaceProxy spaceProxy)
        {
            _spaceProxy = spaceProxy;
        }

        public void Run()
        {
            Utility.LogHeader("Merchants of top 5 payments");

            SqlQuery<Payment> query = new SqlQuery<Payment>("ORDER BY PaymentAmount DESC");

            Payment[] payments = _spaceProxy.ReadMultiple<Payment>(query, 5);

            int order = 0;

            foreach (Payment p in payments)
            {
                Merchant merchant = _spaceProxy.ReadById<Merchant>(p.ReceivingMerchantId);

                if (merchant != null)
                {
                    Utility.LogMessage("{0}: Merchant {1}, payment: {2}", order++, merchant.Name, +p.PaymentAmount);
                }
            }
        }
    }
}
