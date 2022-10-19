using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace Zork
{
    public class World
    {
        public HashSet<Room> Rooms { get; set; }
        public Item[] Items { get; }

        [JsonIgnore]
        public IReadOnlyDictionary<string, Room> RoomsByName => mRoomsByName;

        public Player SpawnPlayer() => new Player(this, StartingLocation);

        [OnDeserialized]
        private void OnDeserialized(StreamingContext Context)
        {
            mRoomsByName = Rooms.ToDictionary(room => room.Name, room => room);

            foreach (Room room in Rooms)
            {
                room.UpdateNeighbors(this);
            }
        }

        public World(Item[] items)
        {
            Items = items;
            ItemByName = new Dictionary<string, Item>();
            foreach (Item item in Items)
            {
                ItemByName.Add(item.Name, item);
            }
        }

        [JsonProperty]
        private string StartingLocation { get; set; }

        private Dictionary<string, Room> mRoomsByName;
    }
}
