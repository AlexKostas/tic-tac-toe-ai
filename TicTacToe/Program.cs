namespace TicTacToe;

internal class Program {
    private static void Main(string[] args) {
        var board = new Board();

        if (board.TileEmpty(1, 1))
            board.ChangeTile(1, 1, PieceType.O);

        if (board.TileEmpty(2, 2))
            board.ChangeTile(2, 2, PieceType.X);

        var successors = board.GetSuccesors(Player.O);
        foreach (var successor in successors) successor.Print();
    }
}