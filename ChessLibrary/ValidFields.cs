
namespace ChessLibrary
{
    public partial class Chess
    {
        public bool[,] validFields { get; private set; }

        private void CalculateValidFields(Pos pos, bool[,] validFields, Piece[,] pieces)
        {
            CColor myColor = DetermineChessColor(pieces[pos.y, pos.x]);
            int i;

            switch (pieces[pos.y, pos.x])
            {
                // PAWNS
                case Piece.BPawn:
                case Piece.WPawn:

                    CheckField(validFields, pieces, pos.Alter((myColor == CColor.Black ? 1 : -1), -1), myColor, false, true);
                    CheckField(validFields, pieces, pos.Alter((myColor == CColor.Black ? 1 : -1), +1), myColor, false, true);

                    if (myColor == CColor.Black) pos.y += 1;
                    else pos.y -= 1;

                    if (CheckField(validFields, pieces, pos, myColor, true) && pos.y == (myColor == CColor.Black ? 2 : 5))
                    {
                        if (myColor == CColor.Black) pos.y += 1;
                        else pos.y -= 1;

                        CheckField(validFields, pieces, pos, myColor, true);

                    }

                    break;

                // ROOKS
                case Piece.WRook:
                case Piece.BRook:

                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(i, 0), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(-i, 0), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(0, i), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(0, -i), myColor)) i++;

                    break;

                // BISHOPS
                case Piece.WBishop:
                case Piece.BBishop:

                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(i, i), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(-i, i), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(-i, -i), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(i, -i), myColor)) i++;

                    break;

                // KINGS
                case Piece.BKing:
                case Piece.WKing:

                    CheckField(validFields, pieces, pos.Alter(0, 1), myColor);
                    CheckField(validFields, pieces, pos.Alter(0, -1), myColor);
                    CheckField(validFields, pieces, pos.Alter(1, 1), myColor);
                    CheckField(validFields, pieces, pos.Alter(1, 0), myColor);
                    CheckField(validFields, pieces, pos.Alter(1, -1), myColor);
                    CheckField(validFields, pieces, pos.Alter(-1, 1), myColor);
                    CheckField(validFields, pieces, pos.Alter(-1, 0), myColor);
                    CheckField(validFields, pieces, pos.Alter(-1, -1), myColor);

                    break;

                // QUEENS
                case Piece.WQueen:
                case Piece.BQueen:

                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(i, 0), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(-i, 0), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(0, i), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(0, -i), myColor)) i++;

                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(i, i), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(-i, i), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(-i, -i), myColor)) i++;
                    i = 1;
                    while (CheckField(validFields, pieces, pos.Alter(i, -i), myColor)) i++;

                    break;

                // KNIGHTS
                case Piece.WKnight:
                case Piece.BKnight:

                    CheckField(validFields, pieces, pos.Alter(1, 2), myColor);
                    CheckField(validFields, pieces, pos.Alter(2, 1), myColor);
                    CheckField(validFields, pieces, pos.Alter(1, -2), myColor);
                    CheckField(validFields, pieces, pos.Alter(2, -1), myColor);
                    CheckField(validFields, pieces, pos.Alter(-1, 2), myColor);
                    CheckField(validFields, pieces, pos.Alter(-2, 1), myColor);
                    CheckField(validFields, pieces, pos.Alter(-1, -2), myColor);
                    CheckField(validFields, pieces, pos.Alter(-2, -1), myColor);

                    break;
            }
        }

        private bool CheckField(bool[,] validFields, Piece[,] pieces, Pos pos, CColor myColor, bool onlyEmpty = false, bool onlyOccupied = false)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x > 7 || pos.y > 7) //out of boundary checks
            {
                return false;
            }
            else if (pieces[pos.y, pos.x] == Piece.Empty)
            {
                if (!onlyOccupied)
                {
                    validFields[pos.y, pos.x] = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (DetermineChessColor(pieces[pos.y, pos.x]) != myColor && !onlyEmpty)
            {
                validFields[pos.y, pos.x] = true;
            }
            return false;
        }
    }
}