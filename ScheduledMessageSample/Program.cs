﻿using System;
namespace ScheduledMessageSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Publisher publisher = new Publisher();
            publisher.SendMessage("Spart!");
            Console.WriteLine("Hello World!");
        }
    }
}
