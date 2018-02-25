using System;
using System.Threading;
using Amqp;

namespace AMQPConsumer1
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "amqp://a:a@localhost:5672";

            Connection connection = new Connection(new Address(address));
            Session session = new Session(connection);
            ReceiverLink receiverLink = new ReceiverLink(session, "test-receiver", "amqp1");
            receiverLink.Start(1,(receiver, message) => {
                System.Console.WriteLine(receiver.Name);
                var body=message.Body;
                System.Console.WriteLine(body);
            });
            Thread.Sleep(100000);

            Console.WriteLine("Message sent into queue q1");
        }
    }
}
