using System.Diagnostics;

namespace TicTacToe;

internal class Program {
    private static void Main(string[] args) {
        var gameManager = new GameManager();
        var gameResult = gameManager.PlayGame();
        Debug.Assert(gameResult is not BoardStatus.InProgress);

        switch (gameResult) {
            case BoardStatus.Draw:
                Console.WriteLine("It's a draw!");
                break;
            case BoardStatus.OWon:
                Console.WriteLine("Player O won!");
                break;
            case BoardStatus.XWon:
                Console.WriteLine("Player X Won");
                break;
            default:
                throw new Exception("Invalid board status type");
        }
    }
}