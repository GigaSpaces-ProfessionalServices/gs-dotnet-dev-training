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
            Payment[] payments = null;
            Merchant merchant = null;

            Utility.LogHeader("Merchants of top 5 payments");

            //TODO: use query to retrieve top 5 Payments from the space
            //TODO: HINT - 'order by ATTRIBUTE desc'

            int order = 0;

            foreach (Payment p in payments)
            {
                //TODO: retrieve the payment associated Merchant to display its details.
                //TODO: HINT - space.readById(class,id)

                if (merchant != null)
                {
                    Utility.LogMessage("{0}: Merchant {1}, payment: {2}", order++, merchant.Name, +p.PaymentAmount);
                }
            }
        }
    }
}
