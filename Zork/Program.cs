using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        public static Room CurrentRoom
        {
            get => Rooms[_location.Row, _location.Column];
        }

        static void Main(string[] args)
        {
            bool isRunning = true;

            Console.WriteLine("Welcome to Zork!");
            while (isRunning)
            {
                Console.Write($"> {Rooms[_location.Row, _location.Column]}\n");
                //Console.WriteLine(CurrentRoom);
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
                        Console.WriteLine(CurrentRoom.Description);
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
        static void InitializeRoomDescriptions()
        {
            Rooms[0, 0].Description = "You are on a rock-strewn trail.";
            Rooms[0, 1].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";
            Rooms[0, 2].Description = "You are at the top of the Great Canyon on its south wall.";

            Rooms[1, 0].Description = "This is a forest, with trees in all direction around.";
            Rooms[1, 1].Description = "This is an open field west of a while house, with a boarded front door";
            Rooms[1, 2].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar.";

            Rooms[2, 0].Description = "This is dimly lift forest, with large trees all aroubd. To the east, there appears to be sunlight.";
            Rooms[2, 1].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";
            Rooms[2, 2].Description = "You are in a clearing, with a forest surrounding you on the west and south.";
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


