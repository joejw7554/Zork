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

            var output = new ConsoleOutputService();

            Console.WriteLine("Welcome to Zork!");
            game.Run(output);
            Console.WriteLine("Thank you for Playing!");
        }
        enum CommandLineArguements
        {
            RoomsFilename = 0
        }
    }
}


