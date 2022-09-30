using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Zork
{
    public class World
    {
        public HashSet<Room> Rooms { get; set; }

        public Player SpawnPlayer()
        {
            return new Player(this, StartingLocation);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext Context)
        {
            mRoomsByName = Rooms.ToDictionary(room => room.Name, room => room);

            foreach(Room room in Rooms)
            {
                room.UpdateNeighbors(this);
            }
        }

        [JsonProperty]
        private string StartingLocation { get; set; }

        private Dictionary<string, Room> mRoomsByName;
    }
}
