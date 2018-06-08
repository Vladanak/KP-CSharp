using System;
using System.ServiceModel;
using Server.Context;
using System.Data.Entity;

namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UserContext>());
            ServiceHost host = new ServiceHost(typeof(ServerWorks), new Uri("http://localhost:8000/Server"));
            host.AddServiceEndpoint(typeof(IServerWorks), new BasicHttpBinding(), "");
            host.Open();
            Console.WriteLine("Сервер запущен");
            while (Console.ReadLine()!="exit")
            host.Close();
        }
    }
}