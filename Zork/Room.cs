using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Zork
{
    public class Room : IEquatable<Room>
    {
        [JsonProperty(Order = 1)] public string Name { get; private set; }
        [JsonProperty(Order = 2)] public string Description { get; set; }
        [JsonProperty(PropertyName = "Neighbors", Order = 3)] private Dictionary<Directions, string> NeighborNames { get; set; }
        [JsonIgnore]
        public List<Item> Inventory { get; private set; }
        [JsonProperty("Inventory")]
        private string[] InventoryNames { get; set; }

        public Room(string name, string description, Dictionary<Directions, string> neighbornames, List<Item> inventory, string[] inventoryNames)
        {
            Name = name;
            Description = description;
            NeighborNames = neighbornames ?? new Dictionary<Directions, string>();
            InventoryNames = inventoryNames ?? new string[0];
        }

        [JsonIgnore]
        public IReadOnlyDictionary<Directions, Room> Neighbors { get; private set; }

        public static bool operator ==(Room lhs, Room rhs)
        {
            if (ReferenceEquals(lhs, rhs)) { return true; }

            if (lhs is null || rhs is null) { return false; }

            return lhs.Name == rhs.Name;
        }

        public static bool operator !=(Room lhs, Room rhs) => !(lhs == rhs);
        public override bool Equals(object obj) => obj is Room room ? this == room : false;

        public bool Equals(Room other) => this == other;

        public override string ToString() => Name;

        public override int GetHashCode() => Name.GetHashCode();


        public void UpdateInventory(World world)
        {
            Inventory = new List<Item>();
            foreach (var inventoryName in InventoryNames)
            {
                Inventory.Add(world.ItemsByName[inventoryName]);
            }
            InventoryNames = null;
        }

        public void UpdateNeighbors(World world)
        {
            Neighbors = (from entry in NeighborNames
                         let room = world.RoomsByName.GetValueOrDefault(entry.Value)
                         where room != null
                         select (Direction: entry.Key, Room: room)).ToDictionary(pair => pair.Direction, pair => pair.Room);

            //Neighbors= new Dictionary<Directions, Room>();
            //foreach(KeyValuePair<directions,string> neighborName in NeighborNames)
            //{
            //Neighbors.Add(neighborName.Key,world.RoomsByname)
            //}
            //NeighborNames=null;
        }


    }
}
