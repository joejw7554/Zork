using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Zork
{
    class Program
    {
        static readonly Dictionary<string, Room> RoomMap;
        static Program()
        {
            RoomMap = new Dictionary<string, Room>();

            foreach (Room room in Rooms)
            {
                RoomMap[room.Name] = room;
            }
        }
        public static Room CurrentRoom
        {
            get => Rooms[_location.Row, _location.Column];
        }

        static (int Row, int Column) _location = (1, 1);

        static void Main(string[] args)
        {
            Room previousRoom = null;

            const string defaultRoomsFilename = @"Content\Rooms.txt";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguements.RoomsFilename] : defaultRoomsFilename);
            InitializeRoomDescriptions(roomsFilename);

            Console.WriteLine("Welcome to Zork!");

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(CurrentRoom);
                if (previousRoom != CurrentRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                }

                Console.Write("> ");

                string inputString = Console.ReadLine().Trim();
                Commands command = ToCommand(inputString);

                string outputString;

                switch (command)
                {
                    case Commands.QUIT:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = CurrentRoom.Description;
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command)) //Move Character in the array
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

        }

        static bool Move(Commands command)
        {
            bool didMove = false;
            switch (command)
            {
                case Commands.NORTH when _location.Row < Rooms.GetLength(0) - 1:
                    _location.Row++;
                    didMove = true;
                    break;

                case Commands.SOUTH when _location.Row > 0:
                    _location.Row--;
                    didMove = true;
                    break;

                case Commands.EAST when _location.Column < Rooms.GetLength(1) - 1:
                    _location.Column++;
                    didMove = true;
                    break;

                case Commands.WEST when _location.Column > 0:
                    _location.Column--;
                    didMove = true;
                    break;
            }
            return didMove;
        }
        static Commands ToCommand(string commandString) =>
            Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;

        static readonly Room[,] Rooms =
        {
            {new Room("Rocky Trail"), new Room("South of House") ,new Room("Canyon View")},
            {new Room("Forest"), new Room("West of House"),new Room("Behind House")},
            {new Room("Dense Woods"), new Room("North of House"),new Room("Clearing")}
        };

        static void InitializeRoomDescriptions(string roomFileName)
        {
            var rooms = JsonConvert.DeserializeObject<Room[]>(File.ReadAllText(roomFileName));
            foreach (Room room in rooms)
            {
                RoomMap[room.Name].Description = room.Description;
            }
        }
        enum Fields
        {
            Name,
            Description
        }
        enum CommandLineArguements
        {
            RoomsFilename = 0
        }

        static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };
    }
}


