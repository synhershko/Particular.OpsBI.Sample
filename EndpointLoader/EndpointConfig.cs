
using System.Globalization;

namespace EndpointLoader
{
    using NServiceBus;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using VideoStore.Messages.Commands;


	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Client, IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        private int orderNumber;
        private volatile bool shouldStop;
        readonly Stopwatch sw = new Stopwatch();
        private Thread thread;

        public void Start()
        {
            Console.WriteLine("Starting");
            Console.WriteLine("Click Enter to start sending");
            Console.ReadLine();
            Console.WriteLine("Sending");

            shouldStop = false;
            thread = new Thread(LoopSendMessage);
            thread.Start();
        }

        private void LoopSendMessage()
        {
            while (!shouldStop)
            {                
                sw.Start();
                
                var num = SendMessage();

                // On peak we sleep less and send more
                var rand = SampleRandomization.IsPeak() ? 100 : 1000;
                Thread.Sleep(SampleRandomization.Random.Next(rand));

                if (num % 1000 == 0)
                {
                    sw.Stop();
                    var msgPerSec = (1000000 / sw.ElapsedMilliseconds);
                    Console.WriteLine("{0} msg/sec", msgPerSec);
                    sw.Reset();
                }
            }
        }

        private int SendMessage()
        {
            var command = new SubmitOrder
            {
                ClientId = Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture),
                OrderNumber = Interlocked.Increment(ref orderNumber),
                VideoIds = new[] { "intro2", "intro1"},
                EncryptedCreditCardNumber = "GwPEFVxH9Sc53E7/IuvTlCdPVnS54f/vtFWFYdC/+Fs=@8qP13Poio7oXF5lDlDbyGQ==",
                EncryptedExpirationDate = "EVNMPEVijulYdA9Dz2sJNw==@yd1497lzzIykLg2FEmTqkQ=="
            };

            Bus.Send(command);
            return orderNumber;
        }

        public void Stop()
        {
            shouldStop = true;
            Console.WriteLine("Stopping");
            thread.Join();
        }
    }
}
