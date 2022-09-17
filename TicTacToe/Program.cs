using System.Diagnostics;
using TicTacToe.Enums;

namespace TicTacToe;

internal static class Program {
    private static void Main(string[] args) {
        while (true) {
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
                case BoardStatus.InProgress:
                    throw new Exception("Invalid board status type");
                default:
                    throw new Exception("Invalid board status type");
            }

            Console.WriteLine("Press 'P' to play again");
            var input = Console.ReadLine();
            input = input?.ToLower();
            input = input?.Trim();
            if (input != null && !input.Equals("p")) break;
        }
    }
}