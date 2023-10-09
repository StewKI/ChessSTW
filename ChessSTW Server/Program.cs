using ChessLibrary;
using System.Collections.Concurrent;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using ServerLibrary;


namespace ChessSTW_Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var server = new ServerLogic();
            await server.StartAsync();
        }
    }
    
}