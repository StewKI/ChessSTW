using NetworkLibrary.Intefaces;
using NetworkLibrary.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace NetworkLibrary.Acceptors
{
    public class WebSocketAccepter : IAccepter
    {
        HttpListener httpListener = new HttpListener();
        public Task<bool> StartAsync()
        {
            string uri = "http://localhost:6970/";
            httpListener.Prefixes.Add(uri);
            httpListener.Start();
            Console.WriteLine($"Started on URI: {uri}");
            return Task.FromResult(true);
        }

        public async Task<IConnection?> AcceptAsync()
        {
            HttpListenerContext context = await httpListener.GetContextAsync();
            IConnection? conn;
            if (context.Request.IsWebSocketRequest)
            {
                HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);

                WebSocket webSocket = webSocketContext.WebSocket;
                conn = new WebSocketConnection(webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Close();
                conn = null;
            }
            return conn;
        }

        /*

        private static async Task HandleWebSocketConnection(WebSocket webSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (webSocket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        Console.WriteLine($"Received: {message}");
                        // Echo the message back to the client
                        await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    }
                }
            }
            catch (WebSocketException)
            {
                // Handle errors
            }
        }
        */
    }
}
