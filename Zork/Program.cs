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

             public Room CurrentRoom
        {
            get => _world.Rooms[_location.Row, _location.Column];
        }

        const string defaultRoomsFilename = "Rooms.txt";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);

            bool isRunning = true;
            Room previousRoom = null;
            string roomsFileName = "Rooms.txt";
            InitializeRoomDescriptions(roomsFileName);

            while (isRunning)
            {
                Console.WriteLine(Player.CurrentRoom);
                if (previousRoom != Player.CurrentRoom)
                {
                    Console.WriteLine(Player.CurrentRoom.Description);
                    previousRoom = Player.CurrentRoom;
                }

                Console.Write("> ");

                string inputString = Console.ReadLine().Trim();
                Commands command = ToCommand(inputString);

                string outputString = null;

                switch (command)
                {
                    case Commands.QUIT:
                        isRunning = false;
                        break;

                    case Commands.LOOK:
                        outputString = Player.CurrentRoom.Description;
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Player.Move(command))
                        {
                            outputString = $"You moved {command}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }
                Console.WriteLine(outputString);
            }


            Game game = new Game();
            Console.WriteLine("Welcome to Zork!");
            game.Run();
            Console.WriteLine("Thank you for playing!");
        }

    }
}


