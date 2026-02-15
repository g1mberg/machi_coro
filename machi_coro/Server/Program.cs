using Shared;
using ProtocolFramework;


namespace Server
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "XServer";
            Console.ForegroundColor = ConsoleColor.White;
            ProtocolFramework.Utils.Utils.RegisterAllTypes();


            var server = new XServer();
            server.Start();
            server.AcceptClients();
        }
    }
}
