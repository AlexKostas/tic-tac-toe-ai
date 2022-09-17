namespace TicTacToe;

public class AI {
    public static Move GetBestMove(Board board, Player playerInTurn) {
        return new Move(0, 0, playerInTurn);
    }
}