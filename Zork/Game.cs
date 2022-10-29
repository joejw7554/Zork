using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Zork
{
    public class Game
    {
        public World World { get; private set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        [JsonIgnore]
        private bool IsRunning { get; set; }

        public Game(World world, Player player)
        {
            World = world;
            Player = player;
        }

        public void Run()
        {
            IsRunning = true;
            Room previousRoom = null;
            while (IsRunning)
            {
                Console.WriteLine(Player.Location);
                if (previousRoom != Player.Location)
                {
                    Console.WriteLine(Player.Location.Description);
                    previousRoom = Player.Location;
                    foreach(Item itemsInRooms in previousRoom.Inventory)
                    {
                        Console.WriteLine(itemsInRooms.Description);
                    }
                }

                Console.Write("\n> ");

                string inputString = Console.ReadLine().Trim();
                const char separator = ' ';
                string[] commandTokens = inputString.Split(separator);
                string verb = null;
                string objectWord = null;

                if (commandTokens.Length == 0)
                {
                    continue;
                }
                else if (commandTokens.Length == 1)
                {
                    verb = commandTokens[0];
                }
                else
                {
                    verb = commandTokens[0];
                    objectWord = commandTokens[1];
                }
                Commands command = ToCommand(verb);


                switch (command)
                {
                    case Commands.QUIT:
                        IsRunning = false;
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(Player.Location.Description);
                        foreach (Item itemsInRoom in Player.Location.Inventory)
                        {
                            Console.WriteLine(itemsInRoom.Description);
                        }
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        Directions direction = Enum.Parse<Directions>(command.ToString(), true);
                        if (Player.Move(direction) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    case Commands.TAKE when commandTokens.Length >= 2:
                        if (objectWord == null)
                        {
                            Console.WriteLine("What do you want to take?");
                        }

                        Item item = Player.Location.Inventory.FirstOrDefault(roomItems => roomItems.Name == objectWord);

                        if (item != null)
                        {
                            Player.AddItemToInventory(item);
                        }
                        else
                        {
                            Console.WriteLine("You can't see any such thing.");
                        }
                        //item != null ? Player.AddItemToInventory(item) : Console.WriteLine("You can't see any such thing.")
                        break;

                    case Commands.DROP when commandTokens.Length >= 2:
                        if (objectWord == null)
                        {
                            Console.WriteLine("What do you want to drop?");
                        }

                        item = Player.Inventory.FirstOrDefault(playerItems => playerItems.Name == objectWord);

                        if (item != null)
                        {
                            Player.RemoveItemFromInventory(item);
                        }
                        else
                        {
                            Console.WriteLine("You can't see any such thing.");
                        }
                        break;

                    case Commands.INVENTORY:
                        if (Player.Inventory.Count == 0)
                        {
                            Console.WriteLine("You are empty-handed.");
                        }
                        else
                        {
                            Console.WriteLine($"You are carrying:");
                            foreach (Item inventoryItems in Player.Inventory)
                            {
                                Console.WriteLine(inventoryItems);
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }

        }

        public static Game Load(string filename)
        {
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filename));
            game.Player = game.World.SpawnPlayer();

            return game;
        }

        public string FindVerb(string word)
        {
            return word;
        }
        static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;
    }

}
