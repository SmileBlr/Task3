using System;

namespace Task3
{
    static class Program
    {
        static void Main(string[] args)
        {
            CreateMoves(args);
            Rules.CheckMovesCount();

            if (Rules.CountOfMoves < 3)
            {
                Console.WriteLine("Cant start game. Not enough moves.");
            }
            else
            {
                StartGame();
            }

            Console.ReadLine();
        }
        
        private static void CreateMoves(string[] moves)
        {
            foreach (var move in moves)
            {
                if (!Rules.TryAddMove(move))
                {
                    Console.WriteLine($"Cant add move {move}, already exist.");
                }
            }
        }

        private static void StartGame()
        {
            string enemyMove = Rules.GetRandomMove();
            string playerMove = string.Empty;
            var moves = Rules.GetAvailableMovesList();

            Secure.GenerateKey();
            var hmac = Secure.GenerateHMAC(enemyMove);
            Console.WriteLine($"HMAC: {hmac}");

            ShowAvailableMoves();
            GetPlayerInput();
            ShowGameResult();

            #region Work with moves and result functions

            void ShowAvailableMoves()
            {
                Console.WriteLine("Available moves:");

                for (int i = 0; i < moves.Count; i++)
                {
                    Console.WriteLine($"{i} - {moves[i]}");
                }

                Console.WriteLine("? - Help");
            }

            void GetPlayerInput()
            {
                Console.Write("Enter your move: ");
                var input = Console.ReadLine()?.Replace(" ", "");

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Empty input. Write again");
                    GetPlayerInput();
                }
                else
                {
                    ProcessUserInput(input);
                }
            }

            void ProcessUserInput(string input)
            {
                if (input == "?")
                {
                    HelpMenu.BuildTable();
                    GetPlayerInput();
                }
                else if (Int32.TryParse(input, out var parsedValue))
                {
                    if (parsedValue > moves.Count) WrongInput();
                    else playerMove = moves[parsedValue];
                }
                else WrongInput();
            }

            void WrongInput()
            {
                Console.WriteLine("Wrong input. Write Again");
                GetPlayerInput();
            }

            void ShowGameResult()
            {
                Console.WriteLine($"Your move: {playerMove} vs Computer move: {enemyMove}");
                Console.WriteLine(Rules.CheckGameResult(playerMove, enemyMove));
                Console.WriteLine($"HMAC key: {Secure.Key}");
            }

            #endregion
        }
    }
}
