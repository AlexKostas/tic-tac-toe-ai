namespace TicTacToe;

public class GameManager {
    private Board board;
    private Player playerInTurn;
    private const Player AIPlayer = Player.O;

    public GameManager() {
        board = new Board();
        playerInTurn = Player.X;
    }

    public BoardStatus PlayGame() {
        board.Print();

        while (board.GetStatus() == BoardStatus.InProgress) {
            Move moveToPlay;
            if (playerInTurn == AIPlayer) {
                // AI Plays
                moveToPlay = AI.GetBestMove(board, playerInTurn);
            }
            else {
                // Player Plays
                Console.WriteLine("Player " + getPlayerName(playerInTurn) + ", it's your turn!\n");
                var position = getValidPosition();

                moveToPlay = new Move(position.row, position.column, playerInTurn);
                //board.ChangeTile(position.row, position.column, Board.MatchPlayerToPieceType(playerInTurn));
            }

            board.PlayMove(moveToPlay);
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