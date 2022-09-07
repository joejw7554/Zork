using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            Console.WriteLine("Welcome to Zork!");
            while (isRunning)
            {
                Console.Write($"> {_rooms[_currentRoom]}\n");
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
                        outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
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
                case Commands.NORTH:
                case Commands.SOUTH:
                    break;

                case Commands.EAST when _currentRoom < _rooms.Length - 1:
                    _currentRoom++;
                    didMove = true;
                    break;

                case Commands.WEST when _currentRoom > 0:
                    _currentRoom--;
                    didMove = true;
                    break;
            }
            return didMove;
        }
        private static readonly string[] _rooms = { "Forest", "West of House", "Behind House", "Clearing", "Canyon View" };
        private static int _currentRoom = 1;

    }
}


