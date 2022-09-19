using System;
using System.Data.Common;
using System.Dynamic;

namespace Zork
{
    internal class Room
    {
        Program location = new Program();
        public string Name { get; }
        public string Description { get; set; }

        public Room(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        public override string ToString() => Name;
    }
}
