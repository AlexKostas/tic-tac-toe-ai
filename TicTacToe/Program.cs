namespace TicTacToe;

internal class Program {
    private static void Main(string[] args) {
        var board = new Board();
        board.Print();

        if (board.TileEmpty(1, 1))
            board.ChangeTile(1, 1, PieceType.O);

        if (board.TileEmpty(2, 2))
            board.ChangeTile(2, 2, PieceType.X);

        board.Print();
    }
}