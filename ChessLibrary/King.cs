
namespace ChessLibrary
{
    public partial class Chess
    {
        private bool IsKingInCheck(CColor color, Pos? moveFrom = null, Pos? moveTo = null)
        {
            bool[,] tempValids = new bool[8, 8];

            Piece[,] tempPieces = new Piece[8, 8];
            Array.Copy(pieces, tempPieces, pieces.Length);

            if (moveFrom != null && moveTo != null)
            {
                MovePiece((Pos)moveFrom, (Pos)moveTo, tempPieces);
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (pieces[i, j] != Piece.Empty)
                    {
                        if (DetermineChessColor(pieces[i, j]) == Opposite(color))
                        {
                            CalculateValidFields(new Pos(i, j), tempValids, tempPieces);
                        }
                    }
                }
            }

            Pos kingPos = FindKing(color, tempPieces);

            if (tempValids[kingPos.y, kingPos.x]) return true;
            else return false;
        }


        private Pos FindKing(CColor color, Piece[,] pieces)
        {
            Piece king = (color == CColor.White ? Piece.WKing : Piece.BKing);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (pieces[i, j] == king)
                    {
                        return new Pos(i, j);
                    }
                }
            }
            throw new Exception("No king found on the table!");
        }
    }
}