using System.Diagnostics;
using TicTacToe.Enums;

namespace TicTacToe;

public static class AI {
    public static Move GetBestMove(Board board, Player playerInTurn) {
        var moves = board.GetPossibleMoves(playerInTurn);

        var bestMove = new Move(-1, -1, playerInTurn);
        var bestValue = int.MinValue;
        foreach (var move in moves) {
            board.PlayMove(move);
            var moveValue = minimax(board, false, playerInTurn);
            board.UndoMove(move);

            if (moveValue > bestValue) {
                bestValue = moveValue;
                bestMove = move;
            }
        }

        return bestMove;
    }

    private static int minimax(Board board, bool maximizingPlayer, Player playerAskingMove) {
        if (board.GetStatus() is not BoardStatus.InProgress) return evaluate(board, playerAskingMove);

        if (maximizingPlayer) {
            var playerInTurn = playerAskingMove;

            var moves = board.GetPossibleMoves(playerInTurn);

            var bestValue = int.MinValue;
            foreach (var move in moves) {
                board.PlayMove(move);
                bestValue = Math.Max(bestValue, minimax(board, !maximizingPlayer, playerAskingMove));
                board.UndoMove(move);
            }

            return bestValue;
        }
        else {
            var playerInTurn = playerAskingMove == Player.O ? Player.X : Player.O;

            var moves = board.GetPossibleMoves(playerInTurn);

            var bestValue = int.MaxValue;
            foreach (var move in moves) {
                board.PlayMove(move);
                bestValue = Math.Min(bestValue, minimax(board, !maximizingPlayer, playerAskingMove));
                board.UndoMove(move);
            }

            return bestValue;
        }
    }

    private static int evaluate(Board board, Player playerAskingMove) {
        Debug.Assert(board.GetStatus() is not BoardStatus.InProgress);

        if (board.GetStatus() is BoardStatus.Draw) return 0;

        // Player asking for move wins
        if (board.GetStatus() is BoardStatus.XWon && playerAskingMove is Player.X) return 10;
        if (board.GetStatus() is BoardStatus.OWon && playerAskingMove is Player.O) return 10;

        // Player asking for move looses
        return -10;
    }
}