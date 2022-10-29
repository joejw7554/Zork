﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Zork
{
    public class Player
    {
        public World World { get; }

        [JsonIgnore]
        public Room Location { get; private set; }

        public List<Item> Inventory { get; }

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
                Inventory.Add(item); //This is very similar to RemoveItemFrom Inventory but it's same action different order.. is there a better way of taking care of add and remove item?
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
