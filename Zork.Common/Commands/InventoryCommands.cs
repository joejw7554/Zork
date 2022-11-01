using System;
using System.Linq;

namespace Zork.Common
{
    public static class InventoryCommands
    {
        public static void DisplayPlayerInventory(Game game)
        {
            if (game.Player.Inventory.Count == 0)
            {
                game.Output.WriteLine("You are empty-handed.");
            }
            else
            {
                game.Output.WriteLine($"You are carrying:");
                foreach (Item inventoryItems in game.Player.Inventory)
                {
                    game.Output.WriteLine(inventoryItems);
                }
            }
        }

        public static void DisplayCurrentLocationInventory(Game game)
        {
            foreach (Item itemsInRoom in game.Player.Location.Inventory)
            {
                game.Output.WriteLine(itemsInRoom.Description);
            }
        }

        public static void Take(Game game, string itemName)
        {
            string outputString = null;
            if (string.IsNullOrWhiteSpace(itemName))
            {
                outputString = "What do you want to take?";
            }
            else
            {
                bool alreadyInPlayersInventory = false;
                foreach (Item itemInInventory in game.Player.Inventory)
                {
                    if (string.Compare(itemInInventory.Name, itemName, true) == 0)
                    {
                        alreadyInPlayersInventory = true;
                        outputString = "You are already holding that item.";
                        break;
                    }
                }

                if (alreadyInPlayersInventory == false)
                {
                    Item item = game.Player.Location.Inventory.FirstOrDefault(roomItems => string.Compare(roomItems.Name, itemName, true) == 0);

                    if (item == null)
                    {
                        outputString = "You can't see any such thing.";
                    }
                    else
                    {
                        game.Player.AddItemToInventory(item);
                        outputString = "Taken";
                    }
                }
            }

            if (outputString != null)
            {
                game.Output.WriteLine(outputString);
            }
        }
    }
}
