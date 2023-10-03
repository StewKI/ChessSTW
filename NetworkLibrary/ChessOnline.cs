using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary;

namespace NetworkLibrary
{
    public class ChessOnline : ChessLibrary.Chess
    {
        private Socket? client;
        public CColor? myColor { get; private set; }

        public string username { get; private set; }
        public string? opponUsername { get; private set; }

        public AutoResetEvent serverResponedEvent { get; private set; }
        public string serverResponse { get; private set; }

        private Action<ChessLibrary.Chess> UpdateFunction;
        private Action<string> EndFunc;

        private Task? Listening;

        public ChessOnline(Func<int> PromotePieceFunction, Action<ChessLibrary.Chess> UpdateFunction, Action<string> EndFunc, CColor? myColor, string username, string? opponUsername = null) : base(PromotePieceFunction)
        {
            this.client = null;
            this.UpdateFunction = UpdateFunction;
            this.myColor = myColor;
            this.username = username;
            this.opponUsername = opponUsername;
            this.serverResponse = "";
            this.serverResponedEvent = new(false);
            this.EndFunc = EndFunc;
        }

        ~ChessOnline()
        {
            if(Listening is not null)
            {
                Listening.Dispose();
            }
        }

        private async Task SendMessageAsync(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await client!.SendAsync(messageBytes);
        }

        private string CColorToString(CColor? color)
        {
            if (color is null) return "Random";
            else return color.ToString()!;
        }

        public async Task<bool> TryConnectAsync(string ipAdress, int port)
        {
            try
            {
                IPAddress iPAddress = IPAddress.Parse(ipAdress);
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);

                client = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                await client.ConnectAsync(iPEndPoint);

                //Introducing message
                _ = SendMessageAsync($"introduce:{username}:{CColorToString(myColor)}:{opponUsername}");

                Listening = ListenForMessagesAsync(client!); //TODO implement destructors

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task Disconnect()
        {
            await SendMessageAsync("disconnect");
            Listening!.Dispose();
        }

        private async Task ListenForMessagesAsync(Socket client)
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                int received = await client.ReceiveAsync(buffer);

                string message = Encoding.UTF8.GetString(buffer, 0, received);

                HandleMessage(message);
            }
        }

        private void HandleMessage(string message)
        {
            string[] messageInfo = message.Split(':');
            if (messageInfo.Length > 0)
            {
                switch (messageInfo[0])
                {
                    case "move": //format: "move:<Move.ToString()>"

                        Move? move = Move.FromString(messageInfo[1]);
                        if(move is not null)
                        {
                            MakeMove(move);
                            UpdateFunction(this);
                        }
                        else
                        {
                            //TODO: add handling for this situation
                        }

                        break;

                    case "welcome": //format: "welcome:<color>:<opponUsername>"

                        myColor = messageInfo[1] == "White" ? CColor.White : CColor.Black;
                        opponUsername = messageInfo[2];
                        serverResponedEvent.Set();
                        serverResponse = "welcome";

                        break;

                    case "waiting":

                        serverResponedEvent.Set();
                        serverResponse = "waiting";

                        break;

                    case "username":

                        serverResponedEvent.Set();
                        serverResponse = "username";

                        break;

                    case "end": //format: "end:<reason>"

                        EndFunc(messageInfo[1]);

                        break;
                }
            }
        }

        public override bool Click(int y, int x)
        {
            if (TurnColor == myColor)
            {
                bool succes = base.Click(y, x);

                if (succes)
                {
                    _ = SendMessageAsync($"move:{LastMove()}");
                }

                return succes;
            }
            return false;
        }
    }
}
