using System.Diagnostics;

namespace TicTacToe;

public class Board {
    private PieceType[,] board;

    public Board() {
        board = new PieceType[3, 3];

        for (var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++)
            board[i, j] = PieceType.Empty;
    }

    public void ChangeTile(int row, int column, PieceType newPiece) {
        Debug.Assert(row is >= 0 and < 3);
        Debug.Assert(column is > 0 and < 3);
        Debug.Assert(newPiece is not PieceType.Empty);
        Debug.Assert(board[row, column] is not PieceType.X or PieceType.O);

        board[row, column] = newPiece;
    }

    public bool TileEmpty(int row, int column) {
        Debug.Assert(row is >= 0 and < 3);
        Debug.Assert(column is > 0 and < 3);

        return board[row, column] == PieceType.Empty;
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
                    Console.WriteLine("An error occured in printRow: Unknown PieceType");
                    break;
            }

        Console.WriteLine("");
    }

    private void printHorizontalLine() {
        for (var i = 0; i < 7; i++)
            Console.Write('-');
        Console.WriteLine("");
    }
}