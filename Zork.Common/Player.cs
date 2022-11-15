using System;
using System.Collections.Generic;

namespace Zork.Common
{
    public class Player
    {
        public int Moves
        {
            get { return _moves; }
            set
            {
                if (_moves != value)
                {
                    _moves = value;
                }
            }
        }

        public int Score
        {
            get { return _scores; }
            set
            {
                if (_scores != value)
                {
                    _scores = value;
                }
            }
        }

        public Room CurrentRoom
        {
            get => _currentRoom;
            set => _currentRoom = value;
        }

        public IEnumerable<Item> Inventory => _inventory;

        public Player(World world, string startingLocation)
        {
            _world = world;

            if (_world.RoomsByName.TryGetValue(startingLocation, out _currentRoom) == false)
            {
                throw new Exception($"Invalid starting location: {startingLocation}");
            }

            _inventory = new List<Item>();
        }

        public bool Move(Directions direction)
        {
            bool didMove = _currentRoom.Neighbors.TryGetValue(direction, out Room neighbor);
            if (didMove)
            {
                CurrentRoom = neighbor;
            }

            return didMove;
        }

        public void AddItemToInventory(Item itemToAdd)
        {
            if (_inventory.Contains(itemToAdd))
            {
                throw new Exception($"Item {itemToAdd} already exists in inventory.");
            }

            _inventory.Add(itemToAdd);
        }

        public void RemoveItemFromInventory(Item itemToRemove)
        {
            if (_inventory.Remove(itemToRemove) == false)
            {
                throw new Exception("Could not remove item from inventory.");
            }
        }

        private readonly World _world;
        private Room _currentRoom;
        private readonly List<Item> _inventory;
        private int _moves = 1;
        private int _scores = 0;


        public override string ToString()
        {
            return Score.ToString();
        }
    }
}
