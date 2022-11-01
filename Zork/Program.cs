using System;
using Zork.Common;

namespace Zork.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultRoomsFilename = @"Content/Zork.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguements.RoomsFilename] : defaultRoomsFilename);
            Game game = Game.Load(gameFilename);

            var input = new ConsoleInputService();
            var output = new ConsoleOutputService();

            game.Player.MovesChanged += Player_MovesChaged;


            Console.WriteLine("Welcome to Zork!");
            game.Run(output);
            Console.WriteLine("Thank you for Playing!");
        }

        static void Player_MovesChaged(object sender, int moves)
        {
            Console.WriteLine($"You've made {moves} moves");
        }

        enum CommandLineArguements
        {
            RoomsFilename = 0
        }
    }
}


