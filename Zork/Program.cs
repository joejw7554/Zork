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

<<<<<<< HEAD
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
=======

            InitializeRoomDescriptions();
            Console.WriteLine("Welcome to Zork!");
>>>>>>> parent of c194d33 (Zork 4.1 (still working))

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

<<<<<<< HEAD
=======
        static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invnalid direction.");

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

        static bool IsDirection(Commands command) => Directions.Contains(command);

        static readonly Room[,] Rooms =
        {
            {new Room("Rocky Trail"), new Room("South of House") ,new Room("Canyon View")},
            {new Room("Forest"), new Room("West of House"),new Room("Behind House")},
            {new Room("Dense Woods"), new Room("North of House"),new Room("Clearing")}
        };

        static (int Row, int Column) _location = (1, 1);
        static void InitializeRoomDescriptions(string roomFileName)
        {
            const string fieldDelmiter = "##";
            const int expectedFieldCount = 2;

            string[] lines = File.ReadAllLines(roomFileName);

            foreach(string line in lines)
            {
                string[] fields=line.Split(fieldDelmiter);
                if (fields.Length != expectedFieldCount)
                {
                    throw new InvalidDataException("Invalid Recrod");
                }
                string name = fields[(int)Fields.Name];
                string description = fields[(int)Fields.Description];

                RoomMap[name].Description = description;
            }

        }

        static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };


>>>>>>> parent of c194d33 (Zork 4.1 (still working))
    }
}


