using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
            Room previousRoom = null;

            const string defaultRoomsFilename = @"Content/Rooms.json";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguements.RoomsFilename] : defaultRoomsFilename);
            InitializeRooms(roomsFilename);

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

       
        static Commands ToCommand(string commandString) =>
            Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;

        static Room[,] Rooms;

        static (int Row, int Column) _location = (1, 1);

        static void InitializeRooms(string roomFileName) => Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomFileName));
      
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


