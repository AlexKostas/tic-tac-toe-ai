using TicTacToe.Enums;

namespace TicTacToe;

public class Move {
    private readonly int destinationRow;
    private readonly int destinationColumn;
    private readonly Player player;

    public Move(int destinationRow, int destinationColumn, Player player) {
        this.destinationRow = destinationRow;
        this.destinationColumn = destinationColumn;
        this.player = player;
    }

    public int GetDestinationRow() {
        return destinationRow;
    }

    public int GetDestinationColumn() {
        return destinationColumn;
    }

    public Player GetPlayer() {
        return player;
    }
}