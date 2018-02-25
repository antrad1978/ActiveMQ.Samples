using System;
using ActiveMQ.Base;
using Apache.NMS;

namespace ScheduledMessageSample
{
    class Publisher : BaseConnection
    {
        public const string DESTINATION = "queue://ScheduledQueueSampleQueue";
        public Publisher()
        {
        }

        public string SendMessage(string textMessage)
        {
            string result = string.Empty;
            try
            {
                IDestination destination = _session.GetDestination(DESTINATION);
                using (IMessageProducer producer = _session.CreateProducer(destination))
                {
                    producer.DeliveryMode = MsgDeliveryMode.Persistent;

                    ITextMessage message = _session.CreateTextMessage(textMessage);
                    long time = 60 * 1000;
                    message.Properties["AMQ_SCHEDULED_DELAY"] = time;

                    producer.Send(message);
                }
                result = "Message sent successfully.";
            }
            catch (Exception ex)
            {
                result = ex.Message;
                Console.WriteLine(ex.ToString());
            }finally{
                _session.Dispose();
            }
            return result;
        }
    }
}
