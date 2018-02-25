using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using ActiveMQ.Base;

namespace Consumer
{
    public class Consumer : BaseConnection
    {
        //public const string DESTINATION = "queue://Client2To1";
        public const string DESTINATION = "queue://Client1To2";

        public Consumer()
        {
            
        }

        public void Initialize()
        {
            try
            {
                IConnectionFactory connectionFactory = new ConnectionFactory(URI);

                IConnection _connection = connectionFactory.CreateConnection();
                _connection.Start();

                //standard
                ISession _session = _connection.CreateSession();

                IDestination dest = _session.GetDestination(DESTINATION);
                using (IMessageConsumer consumer = _session.CreateConsumer(dest))
                {
                    Console.WriteLine("Listener started.");
                    Console.WriteLine("Listener created.rn");
                    IMessage message;
                    while (true)
                    {
                        message = consumer.Receive();
                        if (message != null)
                        {
                            ITextMessage textMessage = message as ITextMessage;
                            if (!string.IsNullOrEmpty(textMessage.Text))
                            {
                                Console.WriteLine(textMessage.Text);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Press <ENTER> to exit.");
                Console.Read();
            }
        }
    }
}
