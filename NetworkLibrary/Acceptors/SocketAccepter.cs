using NetworkLibrary.Connections;
using NetworkLibrary.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.Accepters
{
    public class SocketAccepter : IAccepter
    {
        private Socket? listener;

        public SocketAccepter()
        {
        }

        public async Task<bool> StartAsync()
        {
            IPHostEntry iPHostInfo = await Dns.GetHostEntryAsync(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Any;

            //Temp
            await Console.Out.WriteLineAsync($"Host name: {Dns.GetHostName()}");
            await Console.Out.WriteLineAsync($"Started on IP: {ipAddress.ToString()} on port {6969}, {ipAddress.AddressFamily}");

            IPEndPoint endPoint = new IPEndPoint(ipAddress, 6969);
            listener = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(endPoint);

            try
            {
                Console.WriteLine("Waiting for connections...");
                listener.Listen();

                //_ = ListenForConnectionsAsync(listener);

                return true;
            }
            catch (SocketException e)
            {
                listener.Dispose();
                return false;
                //throw new SocketException(e.ErrorCode);
            }

        }
        public async Task<IConnection> AcceptAsync()
        {
            Socket newSocket = await listener!.AcceptAsync();
            return new SocketConnection(newSocket);
        }
    }
}
