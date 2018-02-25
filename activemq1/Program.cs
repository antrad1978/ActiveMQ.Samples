using System;

namespace activemq1
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher publisher = new Publisher();
            publisher.SendMessage("Hello World!");
        }
    }
}
