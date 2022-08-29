using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            string inputString = Console.ReadLine().Trim();
            Commands command = ToCommand(inputString);
            Console.WriteLine(command);
        }

        private static Commands ToCommand(string commandString)
        {
            //if (Enum.TryParse<Commands>(commandString, true, out Commands command))
            //{
            //    return command;
            //}
            //else
            //{
            //    return Commands.UNKNOWN;
            //}

            return Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;
        }
    }
}
