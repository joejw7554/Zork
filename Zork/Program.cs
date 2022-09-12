using System;

namespace Zork
{
    class Program
    {
        //static string CurrentRoom
        //{
        //    get
        //    {
        //        return _rooms[_location.Row, _location.Column];
        //    }
        //}

        static void Main(string[] args)
        {
            Room westOfHouse = new Room();

            bool isRunning = true;

            Console.WriteLine("Welcome to Zork!");
            while (isRunning)
            {
                Console.Write($"> {_rooms[_location.Row, _location.Column]}\n");
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
                        if (Move(command))
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

        static Commands ToCommand(string commandString)
        {
            return Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;
        }

        static bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH when _location.Row > _rooms.GetLength(0) - 1:
                    _location.Row++;
                    didMove = true;
                    break;

                case Commands.SOUTH when _location.Row > 0:
                    _location.Row--;
                    didMove = true;
                    break;

                case Commands.EAST when _location.Column < _rooms.GetLength(1) - 1:
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

        private static void InitializeRoomDescriptions()
        {
            _rooms[0, 0].Description = "You are on";
        }

        private static readonly Room[,] _rooms= {{new Room( "Rocky Trail"), new Room("South of House"), new Room("Canyon View")},
                                                      { new Room("Forest"), new Room("West of House"), new Room("Behind House")},
                                                      {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }};
        //private static int _location.Row = 1;
        //private static int _currentLocationColumn = 1;

        //private static (int Row, int Colomn) _location=(1,1);

        private static Location _location = new Location() { Row = 1, Column = 1 };
    }

    internal class Location
    {
        public int Row;
        public int Column;
    }
}


