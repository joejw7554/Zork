using System;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        public void AddItemToInventory(Item itemToAdd)
        {
            if (itemToAdd == null)
            {
                throw new ArgumentNullException(nameof(itemToAdd));
            }

            Inventory.Add(itemToAdd);
            Location.Inventory.Remove(itemToAdd);
        }

        public void RemoveItemFromInventory(Item itemToRemove)
        {
            if (itemToRemove == null)
            {
                throw new ArgumentNullException(nameof(itemToRemove));
            }

            Inventory.Remove(itemToRemove);
            Location.Inventory.Add(itemToRemove);
        }

    }
}


