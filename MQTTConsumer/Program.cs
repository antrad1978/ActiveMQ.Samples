using System;
using System.Net;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace mqtt
{
    class Program
    {
        static void Main(string[] args)
        {
            MqttClient client;

            try
            {
                string clientId;
                // create client instance
                string BrokerAddress = "127.0.0.1";

                client = new MqttClient(BrokerAddress);

                // register a callback-function (we have to implement, see below) which is called by the library when a message was received
                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;;

                // use a unique id as client id, each time we start the application
                clientId = Guid.NewGuid().ToString();

                client.Connect(clientId);

                // subscribe to the topic "/home/temperature" with QoS 2
                client.Subscribe(new string[] { "/home/temperature" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.StackTrace);
            }
        }

        static void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            System.Console.WriteLine(e.Topic);
            System.Text.Encoding.ASCII.GetString(e.Message);
        }
    }
}
