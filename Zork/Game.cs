using System;
using System.Collections.Generic;
using System.IO;

namespace Zork
{
    internal class Game
    {

        enum Fields
        {
            Name,
            Description
        }

        

        public World World { get; set; }
        public Player Player { get; set; }

        public void Run()
        {
            Room previousRoom = null;
            bool isRunning = true;



            InitializeRoomDescriptions(roomsFilename);


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

                string outputString;

                switch (command)
                {
                    case Commands.QUIT:
                        isRunning = false;
                        outputString = "Thank you for playing!";
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

            void InitializeRoomDescriptions(string roomFileName)
            {
                const string fieldDelmiter = "##";
                const int expectedFieldCount = 2;

                string[] lines = File.ReadAllLines(roomFileName);

                foreach (string line in lines)
                {
                    string[] fields = line.Split(fieldDelmiter);
                    if (fields.Length != expectedFieldCount)
                    {
                        throw new InvalidDataException("Invalid Recrod");
                    }
                    string name = fields[(int)Fields.Name];
                    string description = fields[(int)Fields.Description];

                    RoomMap[name].Description = description;
                }

            }

            

            static Commands ToCommand(string commandString) =>
                Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;


        }
        static (int Row, int Column) _location = (1, 1);
        static void InitializeRoomDescriptions(string roomFileName)
        {
            const string fieldDelmiter = "##";
            const int expectedFieldCount = 2;

            string[] lines = File.ReadAllLines(roomFileName);

            foreach (string line in lines)
            {
                string[] fields = line.Split(fieldDelmiter);
                if (fields.Length != expectedFieldCount)
                {
                    throw new InvalidDataException("Invalid Recrod");
                }
                string name = fields[(int)Fields.Name];
                string description = fields[(int)Fields.Description];

                RoomMap[name].Description = description;
            }

        }

        static readonly Dictionary<string, Room> RoomMap;
        static Program()
        {
            RoomMap = new Dictionary<string, Room>();

            foreach (Room room in Rooms)
            {
                RoomMap[room.Name] = room;
            }
        }

         //static bool IsDirection(Commands command) => Directions.Contains(command);





        //static readonly List<Commands> Directions = new List<Commands>
        //{
        //    Commands.NORTH,
        //    Commands.SOUTH,
        //    Commands.EAST,
        //    Commands.WEST
        //};


    }
}