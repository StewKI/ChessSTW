using NetworkLibrary.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.Connections
{
    public class WebSocketConnection : IConnection
    {
        private WebSocket webSocket;

        public WebSocketConnection(WebSocket webSocket)
        {
            this.webSocket = webSocket;
        }

        public async Task<string> ReceiveMessageAsync()
        {
            byte[] buffer = new byte[1024];
            string receivedMessage = "";
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Text)
            {
                receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            }
            return receivedMessage;
        }

        public async Task SendMessageAsync(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            //await Task.Delay(TimeSpan.FromSeconds(2));
        }

        public bool ConnectionTest()
        {
            return true;
        }
    }
}
