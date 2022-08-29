using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            string inputString = Console.ReadLine().Trim().ToUpper();
            Commands command = ToCommand(inputString);
            Console.WriteLine(command);

        }

        private static Commands ToCommand(string commandString)
        {
            Commands command;

            if (commandString == "QUIT")
            {
                command = Commands.QUIT;
            }
            else if (commandString == "LOOK")
            {
                command = Commands.LOOK;
            }
            else if (commandString == "NORTH")
            {
                command = Commands.NORTH;
            }
            else if (commandString == "SOUTH")
            {
                command = Commands.SOUTH;
            }
            else if (commandString == "EAST")
            {
                command = Commands.EAST;
            }
            else if (commandString == "WEST")
            {
                command = Commands.WEST;
            }
            else
            {
                command = Commands.UNKNOWN;
            }
            return command;
        }
    }
}
