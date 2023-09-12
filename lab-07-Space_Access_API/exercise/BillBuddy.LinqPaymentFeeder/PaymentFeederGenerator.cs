using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaSpaces.XAP.ProcessingUnit.Containers.BasicContainer;
using System.Threading;
using BillBuddy.Common.Utils;

namespace BillBuddy.LinqPaymentFeeder
{

    [BasicProcessingUnitComponent(Name = "PaymentFeederGenerator")]
    public class PaymentFeederGenerator : IDisposable
    {
        private int defaultDelay = 1000;
        private Thread _feedingThread;
        private bool _isRunning = true;
        private PaymentFeeder _paymentFeeder;

        [ContainerInitialized]
        public void Initialize(BasicProcessingUnitContainer container)
        {
            Utility.LogMessage("Starting PaymentFeeder");

            _paymentFeeder = new PaymentFeeder();

            _feedingThread = new Thread(Feed);

            _feedingThread.Start();
        }

        private void Feed()
        {
            while (_isRunning)
            {
                Thread.Sleep(defaultDelay);

                _paymentFeeder.CreatePayment();
            }
        }

        public void Dispose()
        {
            _isRunning = false;

            if (_feedingThread != null)
            {
                _feedingThread.Join();
            }

        }
    }
}
