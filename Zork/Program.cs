using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        static string CurrentRoom
        {
            get=>_rooms[_location.Row, _location.Column];
        }

        static void Main(string[] args)
        {
            //Room westOfHouse = new Room();

            bool isRunning = true;

            Console.WriteLine("Welcome to Zork!");
            while (isRunning)
            {
                //Console.Write($"> {_rooms[_location.Row, _location.Column]}\n");
                Console.WriteLine(CurrentRoom);
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
                        outputString = "A rubber mat saying 'Welcome to Zork!' lies by the door.";
                        //Console.WriteLine($"{outputString}");
                        //outputString = CurrentRoom.Description;
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
        static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invnalid direction.");

            bool didMove = false;
            switch (command)
            {
                case Commands.NORTH when _location.Row < _rooms.GetLength(0) - 1:
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
        static Commands ToCommand(string commandString) =>
            Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;

        static bool IsDirection(Commands command) => Directions.Contains(command);

        //private static void InitializeRoomDescriptions()
        //{
        //    _rooms[0, 0].Description = "You are on";
        //}

        static readonly string[,] _rooms =
        {
            {"Rocky Trail", "South of House" ,"Canyon View"},
            {"Forest", "West of House","Behind House"},
            {"Dense Woods", "North of House","Clearing" }
        };

        static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        static (int Row, int Column) _location = (1, 1);
    }
}


