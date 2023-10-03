using ChessLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary
{
    public class Match
    {
        public Player WhitePlayer { get; private set; }
        public Player BlackPlayer { get; private set; }

        //private CColor turnColor = CColor.White;
        private List<Pos> clicks = new();

        private Chess game = new(PromotePiece); 

        public Match(Player WhitePlayer, Player BlackPlayer)
        {
            this.WhitePlayer = WhitePlayer;
            this.BlackPlayer = BlackPlayer;
        }

        private static int PromotePiece()
        {
            return 0;
        }

        private Player PlayerOnTurn()
        {
            if (game.TurnColor == CColor.White)
            {
                return WhitePlayer;
            }
            else
            {
                return BlackPlayer;
            }
        }
        private Player PlayerOffTurn()
        {
            if (game.TurnColor == CColor.Black)
            {
                return WhitePlayer;
            }
            else
            {
                return BlackPlayer;
            }
        }

        public Player? PlayerWithUsername(string username)
        {
            if (BlackPlayer.Username == username)
            {
                return BlackPlayer;
            }
            else if (WhitePlayer.Username == username)
            {
                return WhitePlayer;
            }
            else return null;
        }
        
        public async Task MakeMove(Move move, CColor color)
        {
            if(color == game.TurnColor)
            {
                if (await PlayerOffTurn().SendMessageAsync($"move:{move}")) //TODO: add out queue
                {
                    await PlayerOnTurn().SendMessageAsync("succes");
                    game.MakeMove(move);
                }
                else
                {
                    await PlayerOnTurn().SendMessageAsync("waiting");
                }
            }
        }
        
        public async Task End(string reason)
        {
            await WhitePlayer.SendMessageAsync($"end:{reason}");
            await BlackPlayer.SendMessageAsync($"end:{reason}");
        }
    }
}
