using System;
using System.Collections.Generic;
using System.IO;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultRoomsFilename = "Rooms.txt";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);

            Game game = new Game();
            Console.WriteLine("Welcome to Zork!");
            game.Run();
        }
        enum CommandLineArguments
        {
            RoomsFilename = 0
        }
    }
}


