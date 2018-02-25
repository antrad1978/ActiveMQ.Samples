using System;
using Amqp;
using System.Threading;

namespace AMQPPublisher1
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "amqp://test:test@localhost:5672";

            Connection connection = new Connection(new Address(address));
            Session session = new Session(connection);
            SenderLink producerLink = new SenderLink(session, "test-sender", "amqp1");

            producerLink.AddClosedCallback((sender, error) => {
                if(sender.IsClosed){
                    System.Console.WriteLine(error.Description);
                }
            });

            Message message1 = new Message("Hello AMQP!");
            message1.MessageAnnotations = new Amqp.Framing.MessageAnnotations();
            message1.MessageAnnotations.Map.Add("metadata", "meatadata");
            message1.Header = new Amqp.Framing.Header();
            message1.Header.DeliveryCount = 0;

            //senderLink.Send(message1);
            //expiring message sending
            producerLink.Send(message1,TimeSpan.FromMinutes(5));

            Console.WriteLine("Hello World!");

            producerLink.Close();

            Thread.Sleep(5000);
        }
    }
}
