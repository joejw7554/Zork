using System.Dynamic;

namespace Zork
{
    internal class Room
    {
        private string m_name;
        
        public string Name { get; set; }
        
        private string mDescription;
        public string Description
        {
            get => mDescription;
            set => mDescription = value;
        }

        public Room(string name)
        {
            Name = name;
        }

        public Room(string name, string description = "")
        {
            Name = name;
            Description = description;
        }
        public Room()
        {

        }

        public override string ToString()
        {
            return Name;
        }

    }
}
