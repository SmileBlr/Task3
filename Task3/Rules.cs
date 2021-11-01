using System;
using System.Collections.Generic;

namespace Task3
{
    static class Rules
    {
        private static List<string> moves = new List<string>();
        private static int halfIndexMoves => moves.Count / 2;

        public static int CountOfMoves => moves.Count;
        public static bool TryAddMove(string name)
        {
            if (!moves.Contains(name))
            {
                moves.Add(name);
                return true;
            }

            return false;
        }

        public static ResultGame CheckGameResult(string movePlayer, string moveEnemy)
        {
            if (moveEnemy == movePlayer) return ResultGame.Draw;

            var index = moves.IndexOf(moveEnemy);

            for (int i = 0; i < halfIndexMoves; i++)
            {
                if (++index >= moves.Count) index = 0;
                if (moves[index] == movePlayer) return ResultGame.Lose;
            }

            return ResultGame.Win;
        }

        public static void CheckMovesCount()
        {
            if (moves.Count % 2 == 0 && moves.Count > 0)
            {
                Console.WriteLine("Even number of moves. The last move was deleted.");
                moves.Remove(moves[^1]);
            }
        }

        public static string GetRandomMove()
        {
            var rnd = new Random();
            var move = moves[rnd.Next(moves.Count)];

            return move;
        }

        public static List<string> GetAvailableMovesList() => moves;
    }

    enum ResultGame
    {
        Win,
        Lose,
        Draw
    }
}
