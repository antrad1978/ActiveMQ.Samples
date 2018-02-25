using System;
using ActiveMQ.Base;
using Apache.NMS;

namespace CronMessageSample
{
    class Publisher : BaseConnection
    {
        public const string DESTINATION = "queue://ScheduledCronSampleQueue";
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
                    long delay = 30 * 1000;
                    long period = 10 * 1000;
                    int repeat = 9;
                    message.Properties["AMQ_SCHEDULED_DELAY"] = delay;
                    message.Properties["AMQ_SCHEDULED_PERIOD"] = period;
                    message.Properties["AMQ_SCHEDULED_REPEAT"] = repeat;

                    //a message every hour
                    //message.Properties["AMQ_SCHEDULED_CRON"] = "0 * * * *";

                    producer.Send(message);
                }
                result = "Message sent successfully.";
            }
            catch (Exception ex)
            {
                result = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                _session.Dispose();
            }
            return result;
        }
    }
}
