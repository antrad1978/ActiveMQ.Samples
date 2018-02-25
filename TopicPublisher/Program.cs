using System;
using activemq1;

namespace TopicPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher publisher = new Publisher();
            publisher.SendMessage("Spart!");
        }
    }
}
