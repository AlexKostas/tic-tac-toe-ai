using System.Diagnostics;

namespace TicTacToe;

public class Board {
    private PieceType[,] board;
    private BoardStatus status;

    public Board() {
        board = new PieceType[3, 3];

        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                board[i, j] = PieceType.Empty;

        status = BoardStatus.InProgress;
    }

    public void PlayMove(Move move) {
        var row = move.GetDestRow();
        var column = move.GetDestColumn();
        var pieceType = MatchPlayerToPieceType(move.GetPlayer());

        Debug.Assert(row is >= 0 and < 3);
        Debug.Assert(column is >= 0 and < 3);
        Debug.Assert(pieceType is not PieceType.Empty);
        Debug.Assert(board[row, column] is not PieceType.X or PieceType.O);

        board[row, column] = pieceType;
        updateStatus();
    }

    public void UndoMove(Move move) {
        var row = move.GetDestRow();
        var column = move.GetDestColumn();

        Debug.Assert(row is >= 0 and < 3);
        Debug.Assert(column is >= 0 and < 3);
        Debug.Assert(board[row, column] is PieceType.X or PieceType.O);

        board[row, column] = PieceType.Empty;
        updateStatus();
    }

    public void ChangeTile(int row, int column, PieceType newPiece) {
        Debug.Assert(row is >= 0 and < 3);
        Debug.Assert(column is >= 0 and < 3);
        Debug.Assert(newPiece is not PieceType.Empty);
        Debug.Assert(board[row, column] is not PieceType.X or PieceType.O);

        board[row, column] = newPiece;
        updateStatus();
    }

    public Board PlacePieceInNewCopy(int row, int column, PieceType newPiece) {
        var newBoard = new Board();

        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                newBoard.board[i, j] = board[i, j];

        newBoard.updateStatus();
        newBoard.ChangeTile(row, column, newPiece);

        return newBoard;
    }

    public bool TileEmpty(int row, int column) {
        Debug.Assert(row is >= 0 and < 3);
        Debug.Assert(column is >= 0 and < 3);

        return board[row, column] == PieceType.Empty;
    }

    public BoardStatus GetStatus() {
        return status;
    }

    public List<Move> GetPossibleMoves(Player playerInTurn) {
        var moves = new List<Move>();

        var playerPieceType = MatchPlayerToPieceType(playerInTurn);
        Debug.Assert(playerPieceType is not PieceType.Empty);

        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                if (TileEmpty(i, j)) {
                    var newMove = new Move(i, j, playerInTurn);
                    moves.Add(newMove);
                }

        return moves;
    }

    public List<Board> GetSuccesors(Player playerInTurn) {
        var successors = new List<Board>();

        var playerPieceType = MatchPlayerToPieceType(playerInTurn);

        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                if (TileEmpty(i, j))
                    successors.Add(PlacePieceInNewCopy(i, j, playerPieceType));

        return successors;
    }

    public void Print() {
        printHorizontalLine();
        for (var i = 0; i < 3; i++) {
            printRow(i);
            printHorizontalLine();
        }

        Console.Write("");
    }

    private void printRow(int row) {
        Console.Write('|');
        for (var i = 0; i < 3; i++)
            switch (board[row, i]) {
                case PieceType.Empty:
                    Console.Write(" |");
                    break;
                case PieceType.O:
                    Console.Write("O|");
                    break;
                case PieceType.X:
                    Console.Write("X|");
                    break;
                default:
                    throw new Exception("Unknown PieceType");
                    break;
            }

        Console.WriteLine("");
    }

    private void printHorizontalLine() {
        for (var i = 0; i < 7; i++)
            Console.Write('-');
        Console.WriteLine("");
    }

    private void updateStatus() {
        if (winningPatternExists(PieceType.O)) status = BoardStatus.OWon;
        else if (winningPatternExists(PieceType.X)) status = BoardStatus.XWon;
        else if (boardIsFull()) status = BoardStatus.Draw;
        else status = BoardStatus.InProgress;
    }

    // TODO: Update this to use the Player Enum
    private bool winningPatternExists(PieceType player) {
        Debug.Assert(player != PieceType.Empty);

        // Test rows
        for (var i = 0; i < 3; i++)
            if (rowCompleted(i, player))
                return true;

        // Test columns
        for (var i = 0; i < 3; i++)
            if (columnCompleted(i, player))
                return true;

        // Test diagonals
        if (mainDiagonalCompleted(player)) return true;
        if (secondaryDiagonalCompleted(player)) return true;

        return false;
    }

    private bool rowCompleted(int row, PieceType player) {
        Debug.Assert(player != PieceType.Empty);
        Debug.Assert(row is >= 0 and < 3);

        for (var i = 0; i < 3; i++)
            if (board[row, i] != player)
                return false;

        return true;
    }

    private bool columnCompleted(int column, PieceType player) {
        Debug.Assert(player != PieceType.Empty);
        Debug.Assert(column is >= 0 and < 3);

        for (var i = 0; i < 3; i++)
            if (board[i, column] != player)
                return false;

        return true;
    }

    private bool mainDiagonalCompleted(PieceType player) {
        Debug.Assert(player != PieceType.Empty);

        for (var i = 0; i < 3; i++)
            if (board[i, i] != player)
                return false;

        return true;
    }

    private bool secondaryDiagonalCompleted(PieceType player) {
        Debug.Assert(player != PieceType.Empty);

        for (var i = 0; i < 3; i++)
            if (board[i, 3 - i - 1] != player)
                return false;

        return true;
    }

    private bool boardIsFull() {
        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                if (board[i, j] == PieceType.Empty)
                    return false;

        return true;
    }

    public static PieceType MatchPlayerToPieceType(Player player) {
        if (player == Player.O) return PieceType.O;
        if (player == Player.X) return PieceType.X;
        throw new ArgumentException("Unknown Player Type");
    }
}