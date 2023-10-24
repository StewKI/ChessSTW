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
using NetworkLibrary.Intefaces;

namespace NetworkLibrary
{
    public class ChessOnline : ChessLibrary.Chess
    {
        private IConnection? connection;
        public CColor? myColor { get; private set; }

        public string username { get; private set; }
        public string? opponUsername { get; private set; }

        //public AutoResetEvent serverResponedEvent { get; private set; }
        //public string serverResponse { get; private set; }

        //private Action<ChessLibrary.Chess> UpdateFunction;
        //private Action<string> EndFunc;

        public event EventHandler<string>? ServerResponseEvent;

        public event EventHandler<Chess>? UpdateEvent;
        public event EventHandler<string>? EndGameEvent;

        private Task? Listening;

        public ChessOnline(Func<int> PromotePieceFunction, CColor? myColor, string username, string? opponUsername = null) : base(PromotePieceFunction)
        {
            this.connection = null;
            //this.UpdateFunction = UpdateFunction;
            this.myColor = myColor;
            this.username = username;
            this.opponUsername = opponUsername;
            //this.serverResponse = "";
            //this.serverResponedEvent = new(false);
            //this.EndFunc = EndFunc;
        }
        /*
        ~ChessOnline()
        {
            if(Listening is not null)
            {
                Listening.Dispose();
            }
        }
        
        private async Task SendMessageAsync(string message)
        {
            await connection!.SendMessageAsync(message);
        }
        */
        private string CColorToString(CColor? color)
        {
            if (color is null) return "Random";
            else return color.ToString()!;
        }

        public void Connect(IConnection conn)
        {
            connection = conn;
            //Introducing message
            connection.SendMessageAsync($"introduce:{username}:{CColorToString(myColor)}:{opponUsername}");

            Listening = ListenForMessagesAsync(); //TODO implement stopping this task

        }

        public async Task Disconnect()
        {
            await connection!.SendMessageAsync("disconnect");
            Listening!.Dispose();
        }

        private async Task ListenForMessagesAsync()
        {
            await Console.Out.WriteLineAsync("Uso ikad"); //TODO: debug
            while (true)
            {
                string message;
                try
                {
                    message = await connection!.ReceiveMessageAsync();
                }
                catch (Exception e)
                {
                    await Console.Out.WriteLineAsync(e.Message);
                    throw new Exception(e.Message);
                }

                await Console.Out.WriteLineAsync($"risivovao poruku: {message}"); //TODO: debug

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
                            UpdateEvent?.Invoke(this, this);
                        }
                        else
                        {
                            //TODO: add handling for this situation
                        }

                        break;

                    case "welcome": //format: "welcome:<color>:<opponUsername>"

                        myColor = messageInfo[1] == "White" ? CColor.White : CColor.Black;
                        opponUsername = messageInfo[2];
                        ServerResponseEvent?.Invoke(this, "welcome");
                        //serverResponse = "welcome";
                        //serverResponedEvent.Set();

                        break;

                    case "waiting":
                    case "username":

                        ServerResponseEvent?.Invoke(this, messageInfo[0]);
                        //serverResponse = "waiting";
                        //serverResponedEvent.Set();

                        break;

                    case "end": //format: "end:<reason>"

                        EndGameEvent?.Invoke(this, messageInfo[1]);
                        
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
                    _ = connection!.SendMessageAsync($"move:{LastMove()}");
                }

                return succes;
            }
            return false;
        }
    }
}
