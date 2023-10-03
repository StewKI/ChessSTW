using NetworkLibrary.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.Connections
{
    public class SocketConnection : IConnection
    {
        private Socket socket;

        public SocketConnection(Socket socket)
        {
            this.socket = socket;
        }

        public async Task<string> ReceiveMessageAsync()
        {
            byte[] buffer = new byte[1024];
            int recieved = await socket.ReceiveAsync(buffer);

            string message = Encoding.UTF8.GetString(buffer, 0, recieved);
            
            return message;
        }

        public async Task SendMessageAsync(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await socket.SendAsync(messageBytes);
        }

        bool IConnection.ConnectionTest()
        {
            return !((socket.Poll(1000, SelectMode.SelectRead) && (socket.Available == 0)) || !socket.Connected);
        }
    }
}
