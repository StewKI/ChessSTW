
namespace ChessLibrary
{
    public enum Piece
    {
        Empty,
        WKing, WQueen, WBishop, WKnight, WRook, WPawn,
        BKing, BQueen, BBishop, BKnight, BRook, BPawn
    }

    /// <summary> Enum for encoding Chess Color, White is 0 and Black is 1. </summary>
    public enum CColor //Chess Color
    {
        White, Black
    }

    public partial class Chess
    {
        public Piece[,] pieces { get; private set; }


        private CColor DetermineChessColor(Piece p)
        {
            if (p == Piece.Empty)
            {
                throw new Exception("Empty piece does not have color.");
            }

            if (((int)p) <= 6) return CColor.White;
            else return CColor.Black;
        }

        public static CColor Opposite(CColor color)
        {
            if (color == CColor.White)
            {
                return CColor.Black;
            }
            else
            {
                return CColor.White;
            }
        }

        private Piece[,] DefaultLayout()
        {
            Piece[,] pcs = new Piece[8, 8];

            pcs[0, 0] = Piece.BRook;
            pcs[0, 1] = Piece.BKnight;
            pcs[0, 2] = Piece.BBishop;
            pcs[0, 3] = Piece.BQueen;
            pcs[0, 4] = Piece.BKing;
            pcs[0, 5] = Piece.BBishop;
            pcs[0, 6] = Piece.BKnight;
            pcs[0, 7] = Piece.BRook;

            pcs[7, 0] = Piece.WRook;
            pcs[7, 1] = Piece.WKnight;
            pcs[7, 2] = Piece.WBishop;
            pcs[7, 3] = Piece.WQueen;
            pcs[7, 4] = Piece.WKing;
            pcs[7, 5] = Piece.WBishop;
            pcs[7, 6] = Piece.WKnight;
            pcs[7, 7] = Piece.WRook;

            for (int i = 0; i < 8; i++)
            {
                pcs[1, i] = Piece.BPawn;
                pcs[6, i] = Piece.WPawn;
            }

            return pcs;
        }

        public static string PieceToString(Piece p)
        {
            string r;
            switch (p)
            {
                case Piece.WPawn:
                    r = "♙";
                    break;
                case Piece.WRook:
                    r = "♖";
                    break;
                case Piece.WBishop:
                    r = "♗";
                    break;
                case Piece.WQueen:
                    r = "♕";
                    break;
                case Piece.WKing:
                    r = "♔";
                    break;
                case Piece.WKnight:
                    r = "♘";
                    break;
                case Piece.BPawn:
                    r = "♟";
                    break;
                case Piece.BRook:
                    r = "♜";
                    break;
                case Piece.BBishop:
                    r = "♝";
                    break;
                case Piece.BQueen:
                    r = "♛";
                    break;
                case Piece.BKing:
                    r = "♚";
                    break;
                case Piece.BKnight:
                    r = "♞";
                    break;
                default:
                    r = "";
                    break;
            }
            return r;
        }

        /*
        private Piece[,] DebugLayout()
        {
            Piee[,] pcs = new Piece[8, 8];
            
            pcs[1, 1] = Piece.WPawn;
            pcs[6, 0] = Piece.BPawn;
            pcs[7, 7] = Piece.BKing;
            pcs[0, 7] = Piece.WKing;
            

            //pcs[0, 0] = Piece.BRook;
            //pcs[0, 1] = Piece.BKnight;
            //pcs[0, 2] = Piece.BBishop;
            pcs[0, 3] = Piece.BQueen;
            pcs[0, 4] = Piece.BKing;
            //pcs[0, 5] = Piece.BBishop;
            //pcs[0, 6] = Piece.BKnight;
            //pcs[0, 7] = Piece.BRook;

            pcs[7, 0] = Piece.WRook;
            //pcs[7, 1] = Piece.WKnight;
            //pcs[7, 2] = Piece.WBishop;
            //pcs[7, 3] = Piece.WQueen;
            pcs[7, 4] = Piece.WKing;
            //pcs[7, 5] = Piece.WBishop;
            //pcs[7, 6] = Piece.WKnight;
            pcs[7, 7] = Piece.WRook;
            
            for (int i = 0; i < 8; i++)
            {
                pcs[1, i] = Piece.BPawn;
                pcs[6, i] = Piece.WPawn;
            }
            


            return pcs;
        }*/
    }
}