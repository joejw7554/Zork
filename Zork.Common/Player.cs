using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Zork.Common
{
    public class Player
    {
        public event EventHandler<int> MovesChanged;

        public World World { get; }

        [JsonIgnore]
        public Room Location { get; private set; }

        public List<Item> Inventory { get; }
        int _moves;
        public int Moves
        {
            get
            {
                return _moves;
            }
            set
            {
                if (_moves != value)
                {
                    _moves = value;
                    MovesChanged?.Invoke(this, _moves);
                }
            }
        }

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
            Inventory = new List<Item>();
        }

        public bool Move(Directions direction)
        {
            bool isValidMove = Location.Neighbors.TryGetValue(direction, out Room destination);
            if (isValidMove)
            {
                Location = destination;
            }
            return isValidMove;
        }

        public void AddItemToInventory(Item item)
        {
            foreach (KeyValuePair<string, Item> itemsInWorld in World.ItemsByName)
            {
                if (itemsInWorld.Key == item.Name)
                {
                    item = itemsInWorld.Value;
                    break;
                }
            }

            if (item != null)
            {
                Inventory.Add(item);
                Location.Inventory.Remove(item);
                Console.WriteLine("Taken.");
            }
        }

        public void RemoveItemFromInventory(Item item)
        {
            foreach (KeyValuePair<string, Item> itemsInWorld in World.ItemsByName)
            {
                if (itemsInWorld.Key == item.Name)
                {
                    item = itemsInWorld.Value;
                    break;
                }
            }

            if (item != null)
            {
                Inventory.Remove(item);
                Location.Inventory.Add(item);
                Console.WriteLine("Dropped.");
            }
        }

    }

}
