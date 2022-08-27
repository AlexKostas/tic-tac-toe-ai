namespace TicTacToe;

public class GameManager {
    private Board board;
    private Player playerInTurn;

    public GameManager() {
        board = new Board();
        playerInTurn = Player.X;
    }

    public BoardStatus PlayGame() {
        board.Print();

        while (board.GetStatus() == BoardStatus.InProgress) {
            Console.WriteLine("Player " + getPlayerName(playerInTurn) + ", it's your turn!\n");
            var position = getValidPosition();

            board.ChangeTile(position.row, position.column, Board.MatchPlayerToPieceType(playerInTurn));

            board.Print();
            alternateTurns();
        }

        return board.GetStatus();
    }

    private BoardPosition getValidPosition() {
        var newPosition = new BoardPosition();

        do {
            newPosition.row = getValidIndex("Give row number (1-3)", 1, 3) - 1;
            newPosition.column = getValidIndex("Give column number (1-3)", 1, 3) - 1;
        } while (!board.TileEmpty(newPosition.row, newPosition.column));

        return newPosition;
    }

    private void alternateTurns() {
        playerInTurn = playerInTurn == Player.O ? Player.X : Player.O;
    }

    private char getPlayerName(Player player) {
        return player == Player.O ? 'O' : 'X';
    }

    private int getValidIndex(string message, int lowerLimit, int upperLimit) {
        int index;
        do {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            while (!int.TryParse(input, out index)) {
            }
        } while (index < lowerLimit || index > upperLimit);

        return index;
    }
}

public struct BoardPosition {
    public int row;
    public int column;
}