using System;

namespace CronMessageSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher publisher = new Publisher();
            publisher.SendMessage("Hello!");
        }
    }
}
