
namespace ChessLibrary
{
    public partial class Chess
    { 
        private bool[] movedForCastling;
        private Castling castling1, castling2;

        private bool enPassantPossible;
        private Pos enPassantPos;

        private Func<int> PromotePieceFunction;
        private int? promotingPiece = null;

        private struct Castling
        {
            public bool possible;
            public Pos kingTo, rookFrom, rookTo;
        }

        private void CheckForCastling(CColor color)
        {
            //castling black
            if (color == CColor.Black && !movedForCastling[1])
            {
                if (!movedForCastling[0])
                {
                    if (pieces[0, 3] == Piece.Empty &&
                        pieces[0, 2] == Piece.Empty &&
                        pieces[0, 1] == Piece.Empty)
                    {
                        validFields[0, 2] = true;
                        castling1.possible = true;
                        castling1.kingTo = new Pos(0, 2);
                        castling1.rookFrom = new Pos(0, 0);
                        castling1.rookTo = new Pos(0, 3);
                    }
                }
                if (!movedForCastling[2])
                {
                    if (pieces[0, 5] == Piece.Empty &&
                        pieces[0, 6] == Piece.Empty)
                    {
                        validFields[0, 6] = true;
                        castling2.possible = true;
                        castling2.kingTo = new Pos(0, 6);
                        castling2.rookFrom = new Pos(0, 7);
                        castling2.rookTo = new Pos(0, 5);
                    }
                }
            }

            //castling white
            else if (color == CColor.White && !movedForCastling[4])
            {
                if (!movedForCastling[3])
                {
                    if (pieces[7, 3] == Piece.Empty &&
                        pieces[7, 2] == Piece.Empty &&
                        pieces[7, 1] == Piece.Empty)
                    {
                        validFields[7, 2] = true;
                        castling1.possible = true;
                        castling1.kingTo = new Pos(7, 2);
                        castling1.rookFrom = new Pos(7, 0);
                        castling1.rookTo = new Pos(7, 3);
                    }
                }
                if (!movedForCastling[5])
                {
                    if (pieces[7, 5] == Piece.Empty &&
                        pieces[7, 6] == Piece.Empty)
                    {
                        validFields[7, 6] = true;
                        castling2.possible = true;
                        castling2.kingTo = new Pos(7, 6);
                        castling2.rookFrom = new Pos(7, 7);
                        castling2.rookTo = new Pos(7, 5);
                    }
                }
            }
        }

        private void TryCastling(Pos from, Pos to)
        {
            if (castling1.possible)
            {
                if (castling1.kingTo.y == to.y && castling1.kingTo.x == to.x)
                {
                    MovePiece(castling1.rookFrom, castling1.rookTo, pieces);
                    castling1.possible = false;
                }
            }
            if (castling2.possible)
            {
                if (castling2.kingTo.y == to.y && castling2.kingTo.x == to.x)
                {
                    MovePiece(castling2.rookFrom, castling2.rookTo, pieces);
                    castling2.possible = false;
                }
            }
        }

        private void CheckForEnPassant()
        {
            int step = TurnColor == CColor.White ? 1 : -1;
            Piece pawn = TurnColor == CColor.White ? Piece.WPawn : Piece.BPawn;

            if (enPassantPos.x > 0 && pieces[enPassantPos.y + step, enPassantPos.x - 1] == pawn
                && selectedPos == enPassantPos.Alter(step, -1))
            {
                validFields[enPassantPos.y, enPassantPos.x] = true;
            }
            else if (enPassantPos.x < 7 && pieces[enPassantPos.y + step, enPassantPos.x + 1] == pawn
                && selectedPos == enPassantPos.Alter(step, +1))
            {
                validFields[enPassantPos.y, enPassantPos.x] = true;
            }
        }

        private void PromotePawn(Pos pos, CColor color, int PromoteToPiece)
        {
            Piece newPiece = Piece.WQueen; //white by default

            switch (PromoteToPiece)
            {
                case 0:
                    newPiece = Piece.WQueen; break;
                case 1:
                    newPiece = Piece.WRook; break;
                case 2:
                    newPiece = Piece.WBishop; break;
                case 3:
                    newPiece = Piece.WKnight; break;
            }

            if (color == CColor.Black)
            {
                newPiece = (Piece)(6 + (int)newPiece); //change color to black
            }

            pieces[pos.y, pos.x] = newPiece;
        }
    }
}