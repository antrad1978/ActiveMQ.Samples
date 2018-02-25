using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using ActiveMQ.Base;

namespace TopicConsumer
{
    public class Consumer 
    {
        //public const string DESTINATION = "queue://Client2To1";

        //public const string DESTINATION = "queue://Client1To2";

        public Consumer()
        {
            
        }

        public void Initialize()
        {
            try
            {
                string URI = "activemq:tcp://localhost:61002";  
                ConnectionFactory connectionFactory = new ConnectionFactory(URI); 

                IConnection _connection = connectionFactory.CreateConnection(); 
                _connection.Start(); 
                ISession  _session = _connection.CreateSession(); 

                //standard
                //ISession _session = _connection.CreateSession();

                IDestination dest = _session.GetTopic("events");
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
