using System.Text;

namespace Zork
{
    public class World
    {
        public Room[,] Rooms
        {
            get
            {
                return _rooms;
            }
        }

        static readonly Room[,] _rooms =
        {
            {new Room("Rocky Trail"), new Room("South of House") ,new Room("Canyon View")},
            {new Room("Forest"), new Room("West of House"),new Room("Behind House")},
            {new Room("Dense Woods"), new Room("North of House"),new Room("Clearing")}
        };
    }
}
