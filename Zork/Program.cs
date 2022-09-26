using System;
using System.Collections.Generic;
using System.IO;

namespace Zork
{


    class Program
    {
        enum CommandLineArguments
        {
            RoomsFilename = 0
        }

        static Program()
        {
            var _roomMap = new Dictionary<string, Room>();
            foreach (Room room in Rooms)
            {
                _roomMap[room.Name] = room;
            }
        }
        static void Main(string[] args)
        {
            const string defaultRoomsFilename = "Rooms.txt";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);

            Game game = new Game();
            Console.WriteLine("Welcome to Zork!");
            game.Run();
            Console.WriteLine("Thank you for playing!");
        }

    }
}


