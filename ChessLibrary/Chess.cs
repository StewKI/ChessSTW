
namespace ChessLibrary
{
    public struct Pos
    {
        public int y;
        public int x;

        public Pos(int y, int x)
        {
            this.y = y;
            this.x = x;
        }

        public static bool operator ==(Pos a, Pos b) { return a.y == b.y && a.x == b.x; }

        public static bool operator !=(Pos a, Pos b) { return a.y != b.y || a.x != b.x; }

        public Pos Alter(int y, int x)
        {
            return new Pos(this.y + y, this.x + x);
        }

    }

    //This is a partial class, and there is just a constructor, rest of the class in the other files.
    public partial class Chess
    {
        public CColor TurnColor { get; private set; }

        public Chess(Func<int> PromotePieceFunction)
        {
            this.PromotePieceFunction = PromotePieceFunction;
            this.pieces = DefaultLayout();
            this.isSelected = false;
            this.selectedPos = new Pos(-1, -1);
            this.validFields = new bool[8, 8];
            this.TurnColor = CColor.White;
            this.movedForCastling = new bool[6]; // 0-leftBRook 1-BKing 2-rightBRook, 3-leftWRook 4-WKing 5-rightWRook
            this.castling1 = new Castling();
            this.castling1 = new Castling();
            this.enPassantPossible = false;
            this.enPassantPos = new Pos(-1, -1);
        }
    }
}

class Debug
{
    public static void PrintMatrix(bool[,] matrix)
    {

        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);

        int maxElementWidth = 1;

        Console.Write(" ".PadLeft(2));
        for (int j = 0; j < columns; j++)
        {
            Console.Write((j + 1).ToString().PadLeft(maxElementWidth + 1));
        }
        Console.WriteLine();

        for (int i = 0; i < rows; i++)
        {
            Console.Write((i + 1).ToString().PadLeft(2));
            for (int j = 0; j < columns; j++)
            {
                int valueToPrint = matrix[i, j] ? 1 : 0;
                Console.Write(valueToPrint.ToString().PadLeft(maxElementWidth + 1));
            }
            Console.WriteLine();
        }

        Console.WriteLine("++++++++++++++++++++++++++++#");
    }

}