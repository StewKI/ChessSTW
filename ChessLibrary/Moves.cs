using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Move
    {
        public Pos from { get; private set; }
        public Pos to { get; private set; }

        public Move(Pos from, Pos to)
        {
            this.from = from;
            this.to = to;
        }

        public override string ToString()
        {
            return $"0;{from.y};{from.x};{to.y};{to.x}";
        }

        public static Move? FromString(string String)
        {
            Move? move = null;

            string[] sInfo = String.Split(";");

            if (sInfo.Length > 0)
            {
                switch (sInfo[0])
                {
                    case "0":
                        if (sInfo.Length >= 5)
                        {
                            int fy = int.Parse(sInfo[1]), fx = int.Parse(sInfo[2]),
                                ty = int.Parse(sInfo[3]), tx = int.Parse(sInfo[4]);

                            move = new Move(new Pos(fy, fx), new Pos(ty, tx));
                        }
                        break;

                    case "1":
                        if (sInfo.Length >= 6)
                        {
                            int fy = int.Parse(sInfo[1]), fx = int.Parse(sInfo[2]),
                                ty = int.Parse(sInfo[3]), tx = int.Parse(sInfo[4]),
                                p = int.Parse(sInfo[5]);

                            move = new MoveWithPromotion(new Pos(fy, fx), new Pos(ty, tx), p);
                        }
                        break;
                }
            }

            return move;
        }
    }

    public class MoveWithPromotion : Move
    {
        public int PromotingPiece = 0; // 0 - queen; 1 - rook; 2 - bishop; 3 - knight

        public MoveWithPromotion(Pos from, Pos to, int piece) : base(from, to)
        {
            PromotingPiece = piece;
        }

        public MoveWithPromotion(Move move, int piece) : base(move.from, move.to)
        {
            PromotingPiece = piece;
        }

        public MoveWithPromotion(Pos from, Pos to) : base(from, to) { }

        public override string ToString()
        {
            return $"1;{from.y};{from.x};{to.y};{to.x};{PromotingPiece}";
        }
    }

    public partial class Chess
    {
        public List<Move> Moves { get; private set; } = new();

        public Move LastMove()
        {
            return Moves.Last();
        }
    }
}
