using Shared;
using ProtocolFramework;
using Game;

namespace Server
{
    internal class Program
    {
        private static void Main()
        {
            var game = new Game.Game();
            Console.Title = "XServer";
            Console.ForegroundColor = ConsoleColor.White;
            XPacketTypeManager.RegisterType(XPacketType.Handshake, 1, 0);
            XPacketTypeManager.RegisterType(XPacketType.PlayerConnected, 1, 1);
            XPacketTypeManager.RegisterType(XPacketType.Welcome, 1, 2);
            XPacketTypeManager.RegisterType(XPacketType.PlayerJoined, 1, 3);
            XPacketTypeManager.RegisterType(XPacketType.PlayerReady, 1, 5);
            XPacketTypeManager.RegisterType(XPacketType.LobbyState, 1, 6);
            XPacketTypeManager.RegisterType(XPacketType.GameStart, 1, 7);
            XPacketTypeManager.RegisterType(XPacketType.Error, 1, 8);



            var server = new XServer();
            server.Start();
            server.AcceptClients();
        }
    }
}
