using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Zork
{
    public class Game
    {
      
        public World World { get; set; }
        public Player Player { get; set; }

        public void Run()
        {
            var Rooms = new Room();

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

                string outputString=null;

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

        }

        static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;

        enum Fields
        {
            Name,
            Description
        }

        static void InitializeRoomDescriptions(string roomsFileName)
        {
            Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFileName));
        }
       
        enum CommandLineArguments
        {
            RoomsFilename = 0
        }
        static readonly Dictionary<string, Room> RoomMap;
       

    }

}
