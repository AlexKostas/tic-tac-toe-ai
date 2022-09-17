namespace TicTacToe;

public class Move {
    private int destRow;
    private int destColumn;
    private Player player;

    public Move(int destRow, int destColumn, Player player) {
        this.destRow = destRow;
        this.destColumn = destColumn;
        this.player = player;
    }

    public int GetDestRow() {
        return destRow;
    }

    public int GetDestColumn() {
        return destColumn;
    }

    public Player GetPlayer() {
        return player;
    }
}