using ChessLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NetworkLibrary.Connections;
using NetworkLibrary.Intefaces;
using NetworkLibrary.Accepters;
using NetworkLibrary;
using NetworkLibrary.Acceptors;

namespace ServerLibrary
{
    public class ServerLogic
    {
        IAccepter socketAccepter, webSocketAccepter;
        ThreadSafeList<Player> unmatchedPlayers = new();
        List<Match> matches = new();
        ConcurrentQueue<Message> messageQueue = new();
        AutoResetEvent queueEvent = new(false);

        public ServerLogic()
        {
            socketAccepter = new SocketAccepter();
            webSocketAccepter = new WebSocketAccepter();
        }

        public async Task StartAsync()
        {
            if (await socketAccepter.StartAsync() == true)
            {
                _ = ListenForPlayersAsync(socketAccepter);
            }
            await webSocketAccepter.StartAsync();
            _ = ListenForPlayersAsync(webSocketAccepter);

            while (true)
            {
                await ProccesMessagesAsync(10);
                await CheckForDisconnectionsAsync();
                queueEvent.WaitOne();
            }
        }

        async Task CheckForDisconnectionsAsync()
        {
            List<Player> disconneted = new();
            foreach (Player p in unmatchedPlayers)
            {
                if (!await p.CheckConnectionAsync())
                {
                    disconneted.Add(p);
                }
            }

            foreach (Player p in disconneted)
            {
                unmatchedPlayers.Remove(p);
                Console.WriteLine($"[{p.Username}] disconnected");
            }
        }

        private async Task ListenForPlayersAsync(IAccepter accepter)
        {
            while (true)
            {
                IConnection? newConn = await accepter.AcceptAsync();
                if (newConn is not null)
                {
                    Player newPlayer = new Player(newConn);
                    _ = ListenForMessagesAsync(newPlayer);
                }
            }
        }

        async Task ListenForMessagesAsync(Player player)
        {
            while (true)
            {
                string message = await player.ReceiveMessageAsync();
                messageQueue.Enqueue(new Message(message, player));
                queueEvent.Set();
            }
        }

        CColor? CColorFromString(string s)
        {
            switch (s)
            {
                case "White": return CColor.White;
                case "Black": return CColor.Black;
                case "Random": default: return null;
            }
        }

        void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        bool DetermineColors(Player a, Player b, out Player WhitePlayer, out Player BlackPlayer, bool forceRandomColors = false)
        {
            //Default
            WhitePlayer = a;
            BlackPlayer = b;

            if (a.MyColor == null && b.MyColor == null || forceRandomColors)
            {
                Random random = new();
                if (random.Next(0, 2) == 0)
                {
                    Swap(ref WhitePlayer, ref BlackPlayer);
                }
                return true;
            }
            else if (a.MyColor == b.MyColor)
            {
                return false;
            }

            if (a.MyColor == CColor.Black || b.MyColor == CColor.White)
            {
                Swap(ref WhitePlayer, ref BlackPlayer);
            }
            return true;
        }

        bool FindMatch(Player player)
        {
            bool founded = false;
            if (player.isIntroduced)
            {
                Player? WhitePlayer = null, BlackPlayer = null, foundedPlayer = null;
                Match? newMatch = null;

                if (player.OpponentUsername == "")
                {
                    foreach (Player up in unmatchedPlayers)
                    {
                        if (up.OpponentUsername == "" &&
                            DetermineColors(player, up, out WhitePlayer, out BlackPlayer))
                        {
                            newMatch = new(WhitePlayer, BlackPlayer);
                            foundedPlayer = up;
                            founded = true;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (Player up in unmatchedPlayers)
                    {
                        if (player.OpponentUsername == up.Username)
                        {
                            if (up.OpponentUsername == player.Username)
                            {
                                DetermineColors(player, up, out WhitePlayer, out BlackPlayer, true);
                                newMatch = new(WhitePlayer, BlackPlayer);
                                foundedPlayer = up;
                                founded = true;
                            }
                            break;
                        }
                    }
                }

                if (founded)
                {
                    newMatch!.WhitePlayer.Introduce(WhitePlayer!.Username!, CColor.White, BlackPlayer!.Username!);
                    newMatch.WhitePlayer.StartMatch(newMatch!);

                    newMatch.BlackPlayer.Introduce(BlackPlayer!.Username!, CColor.Black, WhitePlayer!.Username!);
                    newMatch.BlackPlayer.StartMatch(newMatch!);

                    matches.Add(newMatch);

                    Console.WriteLine($"[{player.Username!}] matched with [{foundedPlayer!.Username!}]");

                    unmatchedPlayers.Remove(foundedPlayer!);
                }
                else
                {
                    unmatchedPlayers.Add(player); // will be adding unnececery playeres (i think)
                }
            }

            return founded;
        }

        async Task<bool> TryFindPlayerAsync(Player newPlayer)
        {
            bool found = false;
            bool shouldCheckMatches = true;

            Player? playerToRemove = null;
            foreach (Player p in unmatchedPlayers)
            {
                if (p.Username == newPlayer.Username)
                {
                    shouldCheckMatches = false;
                    if (await p.CheckConnectionAsync())
                    {
                        found = true;
                    }
                    else
                    {
                        playerToRemove = p;
                    }
                    break;
                }
            }
            if (playerToRemove is not null)
            {
                unmatchedPlayers.Remove(playerToRemove);
            }

            if (shouldCheckMatches)
            {
                Match? endedMatch = null;
                foreach (Match match in matches)
                {
                    Player? player = match.PlayerWithUsername(newPlayer.Username!);
                    if (player is not null)
                    {
                        if (await player.CheckConnectionAsync())
                        {
                            found = true;
                        }
                        else
                        {
                            await match.End("opponent disconected");
                            endedMatch = match;
                        }
                        break;
                    }
                }
                if (endedMatch is not null) matches.Remove(endedMatch);
            }

            return found;
        }

        async Task HandleMessageAsync(Message message)
        {
            Player player = message.player;
            string[] messageInfo = message.text.Split(':');
            if (messageInfo.Length > 0)
            {
                switch (messageInfo[0])
                {
                    case "introduce": //format: "introduce:<username>:<myColor>:<opponentUsername>"

                        player.Introduce(
                            messageInfo[1],
                            CColorFromString(messageInfo[2]),
                            messageInfo[3]
                            );


                        await Console.Out.WriteLineAsync($"[{messageInfo[1]}] Connected!");
                        if (await TryFindPlayerAsync(player))
                        {
                            await player.SendMessageAsync("username");

                        }
                        else if (!FindMatch(player))
                        {
                            await Console.Out.WriteLineAsync($"[{messageInfo[1]}] Waiting for match.");
                            await player.SendMessageAsync($"waiting");
                        }

                        break;

                    case "move": //format: "move:<Move.ToString()>"

                        CColor color = player.MyColor == CColor.Black ? CColor.Black : CColor.White;

                        Move? move = Move.FromString(messageInfo[1]);
                        if (move is not null)
                        {
                            await player.Match!.MakeMove(move, color);
                        }
                        else
                        {
                            //TODO: add handling for this situation
                        }
                        break;

                    case "disconnect":

                        if (message.player.Match is not null)
                        {
                            await message.player.Match.End("opponent disconnected");
                            matches.Remove(message.player.Match);
                        }
                        else
                        {
                            unmatchedPlayers.Remove(player);
                        }
                        await Console.Out.WriteLineAsync($"[{player.Username}] Disconnected");

                        break;
                }
            }
        }

        async Task ProccesMessagesAsync(int max = int.MaxValue)
        {
            int numOfProccesed = 0;
            while (messageQueue.Count > 0 && numOfProccesed < max)
            {
                Message? message;
                if (messageQueue.TryDequeue(out message))
                {
                    await HandleMessageAsync(message!);
                }
                numOfProccesed++;
            }
        }
    }
}
