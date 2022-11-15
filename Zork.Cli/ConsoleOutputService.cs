using System;
using Zork.Common;

namespace Zork.Cli
{
    internal class ConsoleOutputService : IOutputService
    {
        public void Write(object obj) => Write(obj.ToString());
        public void Write(string message) => Console.Write(message);
        public void WriteLine(object obj) => WriteLine(obj.ToString());
        public void WriteLine(string message) => Console.WriteLine(message);
    }
}