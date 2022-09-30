using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Common;

namespace Zork
{
    public class Player
    {
        public World World { get; }

        [JsonIgnore]
        public Room Location { get; private set; }

        [JsonIgnore]
        public string LocationName
        {
            get
            {
                return Location?.Name;
            }
            set
            {
                Location = World?.RoomsByName.GetValueOrDefault(value);
            }
        }

        public Player(World world, string startingLocation)
        {
            World = world;
            LocationName = startingLocation;
        }

        static bool Move(Commands command)
        {
            bool isValidMove = CatalogLocation.Neighbors.TryGetValue(direction, out Room destination);
            if (isValidMove)
            {
                CatalogLocation = destination;
            }

            return isValidMove;
        }


    }
}
