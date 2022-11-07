using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Zork.Common
{
    public class Game
    {
        public World World { get; private set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        public IInputService Input { get; private set; }
        public IOutputService Output { get; private set; } // Q:why would I need properties for interface why?

        [JsonIgnore]
        public bool IsRunning { get; private set; }

        public Game(World world, Player player)
        {
            World = world;
            Player = player;
        }


        public static Game Load(string filename)
        {
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filename));
            game.Player = game.World.SpawnPlayer();

            return game;
        }

        public void Run(IInputService input, IOutputService output)
        {
            Input = input ?? throw new ArgumentException(nameof(input));
            Output = output ?? throw new ArgumentException(nameof(output));

            Input.InputReceived += OnInputReceived;
            IsRunning = true;

            Output.WriteLine(Player.Location);
            Output.WriteLine(Player.Location.Description);
            foreach (Item itemsInRoom in Player.Location.Inventory)
            {
                Output.WriteLine(itemsInRoom.Description);
            }

        }

        void OnInputReceived(object sender, string inputString)
        {
            Room previousRoom = Player.Location;

            const char separator = ' ';
            string[] commandTokens = inputString.Split(separator);
            string verb = null;
            string itemName = null;

            if (commandTokens.Length == 1)
            {
                verb = commandTokens[0];
            }
            else
            {
                verb = commandTokens[0];
                itemName = commandTokens[1];
            }

            Commands command = ToCommand(verb);

            string outputString=null;

            switch (command)
            {
                case Commands.QUIT:
                    IsRunning = false;
                    break;

                case Commands.LOOK:
                    Output.WriteLine(Player.Location.Description);
                    InventoryCommands.DisplayCurrentLocationInventory(this);
                    break;

                case Commands.NORTH:
                case Commands.SOUTH:
                case Commands.EAST:
                case Commands.WEST:
                    Directions direction = Enum.Parse<Directions>(command.ToString(), true);
                    if (Player.Move(direction) == false)
                    {
                        Output.WriteLine("The way is shut!");
                    }
                    break;

                case Commands.TAKE:
                    InventoryCommands.Take(this, itemName);
                    break;

                case Commands.DROP:
                    InventoryCommands.Drop(this, itemName);
                    break;
                   

                case Commands.INVENTORY:
                    InventoryCommands.DisplayPlayerInventory(this);
                    break;

                default:
                    Output.WriteLine("Unknown command.");
                    break;
            }

            if (previousRoom != Player.Location)
            {
                Output.WriteLine(Player.Location);
                Output.WriteLine(Player.Location.Description);
                foreach (Item itemsInRoom in Player.Location.Inventory)
                {
                    Output.WriteLine(itemsInRoom.Description);
                }
            }

            if (command != Commands.UNKNOWN)
            {
                Player.Moves++;
            }
        }

        static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;
    }

}
