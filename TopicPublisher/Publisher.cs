using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using ActiveMQ.Base;

namespace activemq1
{
    public class Publisher : BaseConnection
    {
        public Publisher()
        {
        }

        public string SendMessage(string message)
        {
            string result = string.Empty;
            try
            {
                ITopic destination = _session.GetTopic("events");

                using (IMessageProducer producer = _session.CreateProducer(destination))
                {
                    producer.DeliveryMode = MsgDeliveryMode.Persistent;
                    producer.TimeToLive = TimeSpan.FromHours(1);

                    //var textMessage = producer.CreateTextMessage(message);

                    //producer.Send(textMessage);
                    var myMessage = producer.CreateMessage();

                    myMessage.NMSMessageId = Guid.NewGuid().ToString();
                    myMessage.NMSDeliveryMode = MsgDeliveryMode.Persistent;
                    myMessage.NMSPriority = MsgPriority.Normal;
                    myMessage.NMSTimeToLive = TimeSpan.FromMinutes(15);
                    producer.DeliveryMode = MsgDeliveryMode.Persistent;

                    producer.Send(myMessage);
                }
                result = "Message sent successfully.";
            }
            catch (Exception ex)
            {
                result = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

    }
}
