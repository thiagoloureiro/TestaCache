using Nancy.Hosting.Self;
using System;
using System.Configuration;

namespace CacheApi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting NancyFX...");
            using (var nancyHost = new NancyHost(new Uri("http://localhost:8889/nancy/")))
            {
                nancyHost.Start();

                Console.WriteLine("Nancy now listening - navigating to http://localhost:8889/nancy/. Press enter to stop");

                Console.ReadLine();
            }

            Console.ReadLine();
        }
    }
}