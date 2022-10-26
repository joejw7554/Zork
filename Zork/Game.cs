using Newtonsoft.Json;
using System;
using System.IO;


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
                }

                Console.Write("\n> ");

                string inputString = Console.ReadLine().Trim();
                const char separator = ' ';
                string[] commandTokens = inputString.Split(separator);
                string verb = null;
                string subject = null;

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
                    subject = commandTokens[1];
                }
                Commands command = ToCommand(verb);


                switch (command)
                {
                    case Commands.QUIT:
                        IsRunning = false;
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(Player.Location.Description);
                        foreach (Item items in Player.Location.Inventory)
                        {

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
                        if (subject == null)
                        {
                            Console.WriteLine("this verb requires a subject");
                        }
                        bool itemExists = Player.Location.Inventory.Exists(x => x.Name == subject);
                        if (itemExists)
                        {
                            //Player.Inventory.Insert(Player.Location.Inventory[0]);
                        }
                        else
                        {
                            Console.WriteLine("There's no such item dummy");
                        }
                        break;

                    case Commands.DROP when commandTokens.Length >= 2:
                        if (subject == null)
                        {
                            Console.WriteLine("this verb requires a subject");
                        }
                        break;

                    case Commands.INVENTORY:
                        if (Player.Inventory.Count == 0)
                        {
                            Console.WriteLine("Empty hand.");
                        }
                        else
                        {
                            Console.WriteLine("Taken.");
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
