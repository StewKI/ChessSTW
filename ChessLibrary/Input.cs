
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ChessLibrary
{
    public partial class Chess
    {
        public Pos selectedPos { get; private set; }
        protected bool isSelected;


        public virtual bool Click(int y, int x)
        {

            if (isSelected)
            {
                if (selectedPos == new Pos(y, x))
                {
                    Deselect();
                }
                else if (validFields[y, x])
                {
                    MakeMove(new Move(selectedPos, new Pos(y, x)));
                    Deselect();

                    return true; // piece moved
                }
                else
                {
                    Deselect();

                    if (pieces[y, x] != Piece.Empty &&
                        DetermineChessColor(pieces[y, x]) == TurnColor)
                    {
                        Select(new Pos(y, x));
                    }
                }
            }
            else
            {
                if (pieces[y, x] != Piece.Empty &&
                    DetermineChessColor(pieces[y, x]) == TurnColor)
                {
                    Select(new Pos(y, x));
                }
            }
            return false;
        }

        private void Select(Pos pos)
        {
            selectedPos = pos;  
            isSelected = true;
            CalculateValidFields(pos, validFields, pieces);

            CheckForCastling(TurnColor);
            if (enPassantPossible)
            {
                CheckForEnPassant();
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (validFields[i, j])
                    {
                        if (IsKingInCheck(TurnColor, pos, new Pos(i, j)))
                        {
                            validFields[i, j] = false;
                        }
                    }
                }
            }

            //castling
            int firstRow = TurnColor == CColor.White ? 7 : 0;
            if (castling1.possible && (IsKingInCheck(TurnColor) || !validFields[firstRow, 3]))
            {
                castling1.possible = false;
                validFields[firstRow, 2] = false;
            }
            if (castling2.possible && (IsKingInCheck(TurnColor) || !validFields[firstRow, 5]))
            {
                castling2.possible = false;
                validFields[firstRow, 6] = false;
            }

        }

        private void Deselect()
        {
            selectedPos = new Pos(-1, -1);
            isSelected = false;
            validFields = new bool[8, 8];
        }

        public void MakeMove(Move move)
        {
            if(move is MoveWithPromotion)
            {
                promotingPiece = ((MoveWithPromotion)move).PromotingPiece;
            }

            MovePiece(move.from, move.to);
            TurnColor = Opposite(TurnColor);

            if (move is not MoveWithPromotion &&
                promotingPiece is not null)
            {
                move = new MoveWithPromotion(move, (int)promotingPiece);
            }

            promotingPiece = null;
            Moves.Add(move);
        }

        private void MovePiece(Pos from, Pos to) // TODO: unscramble code, break into smaller methods
        {

            //for castling
            TryCastling(from, to);

            if (from.y == 0)
            {
                if (from.x == 0) movedForCastling[0] = true;
                if (from.x == 4) movedForCastling[1] = true;
                if (from.x == 7) movedForCastling[2] = true;
            }
            else if (from.y == 7)
            {
                if (from.x == 0) movedForCastling[3] = true;
                if (from.x == 4) movedForCastling[4] = true;
                if (from.x == 7) movedForCastling[5] = true;
            }

            //actual moving
            pieces[to.y, to.x] = pieces[from.y, from.x];
            pieces[from.y, from.x] = Piece.Empty;


            //pawn promotion
            if (pieces[to.y, to.x] == Piece.BPawn || pieces[to.y, to.x] == Piece.WPawn)
            {
                CColor color = DetermineChessColor(pieces[to.y, to.x]);
                int lastRowForPawn = (color == CColor.White ? 0 : 7);
                if (to.y == lastRowForPawn)
                {
                    if (promotingPiece is null)
                    {
                        promotingPiece = PromotePieceFunction();
                    }

                    PromotePawn(to, color, (int)promotingPiece);

                }
            }

            //en passant
            int epStartRow = (TurnColor == CColor.White ? 6 : 1),
                epEndRow = (TurnColor == CColor.White ? 4 : 3);
            Piece pawn = TurnColor == CColor.White ? Piece.WPawn : Piece.BPawn;
            if (pieces[to.y, to.x] == pawn
                && from.y == epStartRow && to.y == epEndRow)
            {
                enPassantPossible = true;
                enPassantPos = from.Alter(TurnColor == CColor.White ? -1 : 1, 0);
            }
            else
            {
                if (enPassantPossible && to == enPassantPos && pieces[to.y, to.x] == pawn)
                {
                    int step = TurnColor == CColor.White ? -1 : 1;
                    pieces[to.y - step, to.x] = Piece.Empty;
                }
                enPassantPossible = false;
                enPassantPos = new Pos(-1, -1);
            }
        }

        private void MovePiece(Pos from, Pos to, Piece[,] pieces)
        {
            pieces[to.y, to.x] = pieces[from.y, from.x];
            pieces[from.y, from.x] = Piece.Empty;
        }
    }
}