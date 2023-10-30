using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary;
using NetworkLibrary.Intefaces;

namespace ServerLibrary
{
    public class Player
    {
        private IConnection connection;
        
        public Match? Match { get; private set; }

        public bool isIntroduced { get; private set; }
        public bool isConnected { get; private set; }
        public AutoResetEvent ConnectEvenet { get; private set; }

        public string? Username { get; private set; }
        public CColor? MyColor { get; private set; }
        public string OpponentUsername { get; private set; }

        public Player(IConnection connection)
        {
            this.connection = connection;
            isIntroduced = false;
            isConnected = true;
            ConnectEvenet = new(false);
            OpponentUsername = "";
        }

        public void Connect(IConnection connection)
        {
            this.connection = connection;
            isConnected = true;
            ConnectEvenet.Set();
        }

        public void Introduce(string username, CColor? myColor, string opponentUsername)
        {
            this.Username = username;
            this.MyColor = myColor;
            this.OpponentUsername = opponentUsername;

            isIntroduced = true;
        }

        public void StartMatch(Match newMatch)
        {
            this.Match = newMatch;
            _ = SendMessageAsync($"welcome:{MyColor}:{OpponentUsername}");
        }

        public async Task<string> ReceiveMessageAsync()
        {
            string message = await this.connection.ReceiveMessageAsync();
            await Console.Out.WriteLineAsync($"Message received from [{Username}]: {message}");
            return message;
        }

        public async Task<bool> SendMessageAsync(string message)
        {
            if (isConnected)
            {
                try
                {
                    await connection.SendMessageAsync(message);
                    await Console.Out.WriteLineAsync($"Message sent to [{Username}]: {message}");
                    return true;
                }
                catch
                {
                    isConnected = false;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckConnectionAsync()
        {
            bool additionalTest = connection.ConnectionTest();
            try
            {
                await SendMessageAsync("check");
                return true && additionalTest;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Checked sending failed: {e.Message}");
                return false;
            }
        }
    }

    
}
