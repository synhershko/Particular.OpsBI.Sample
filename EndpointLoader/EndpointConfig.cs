
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

        public void Start()
        {
            System.Console.WriteLine("Starting");

            int orderNumber = 0;

            System.Console.WriteLine("Click Enter to start sending");
            System.Console.ReadLine();
            System.Console.WriteLine("Sending");


            Stopwatch sw = new Stopwatch();

            LoopSendMessage(orderNumber, sw);
        
        }

        private void LoopSendMessage(int orderNumber, Stopwatch sw)
        {
            while (true)
            {
                int rand = 1000;
                sw.Start();
                
                orderNumber = SendMessage(orderNumber);

                rand = new Random().Next(rand);
                Thread.Sleep(rand);

                if (orderNumber % 1000 == 0)
                {
                    sw.Stop();
                    var msgPerSec = (1000000 / sw.ElapsedMilliseconds);
                    System.Console.WriteLine("{0} msg/sec", msgPerSec);
                    sw.Reset();
                }
            }
        }

        private int SendMessage(int orderNumber)
        {
            var command = new SubmitOrder
            {
                ClientId = Thread.CurrentThread.ManagedThreadId.ToString(),
                OrderNumber = Interlocked.Increment(ref orderNumber),
                VideoIds = new string[] { "intro2", "intro1"},
                EncryptedCreditCardNumber = "GwPEFVxH9Sc53E7/IuvTlCdPVnS54f/vtFWFYdC/+Fs=@8qP13Poio7oXF5lDlDbyGQ==",
                EncryptedExpirationDate = "EVNMPEVijulYdA9Dz2sJNw==@yd1497lzzIykLg2FEmTqkQ=="
            };

            Bus.Send(command);
            return orderNumber;
        }

        public void Stop()
        {
            System.Console.WriteLine("Stopping");
        }

    }
}
