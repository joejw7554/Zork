using System;
using System.Linq;
using Newtonsoft.Json;

namespace Zork.Common
{
    public class Game
    {
        public World World { get; }

        [JsonIgnore]
        public Player Player { get; }

        [JsonIgnore]
        public IInputService Input { get; private set; }

        [JsonIgnore]
        public IOutputService Output { get; private set; }

        [JsonIgnore]
        public bool IsRunning { get; private set; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }

        public void Run(IInputService input, IOutputService output)
        {
            Input = input ?? throw new ArgumentNullException(nameof(input));
            Output = output ?? throw new ArgumentNullException(nameof(output));

            IsRunning = true;
            Input.InputReceived += OnInputReceived;
            Output.WriteLine("Welcome to Zork!");
            Look();

        }

        public void OnInputReceived(object sender, string inputString)
        {
            char separator = ' ';
            string[] commandTokens = inputString.Split(separator);

            string verb;
            string subject = null;
            string preposition = null;
            string noun = null;
            if (commandTokens.Length == 0)
            {
                return;
            }
            else if (commandTokens.Length == 1)
            {
                verb = commandTokens[0];
            }
            else if (commandTokens.Length == 2)
            {
                verb = commandTokens[0];
                subject = commandTokens[1];
            }
            else
            {
                verb = commandTokens[0];
                subject = commandTokens[1];
                preposition = commandTokens[commandTokens.Length - 2];
                noun = commandTokens[commandTokens.Length - 1];
            }

            Room previousRoom = Player.CurrentRoom;
            Commands command = ToCommand(verb);
            switch (command)
            {
                case Commands.Quit:
                    IsRunning = false;
                    Output.WriteLine("Thank you for playing!");
                    break;

                case Commands.Look:
                    Look();
                    break;

                case Commands.North:
                case Commands.South:
                case Commands.East:
                case Commands.West:
                    Directions direction = (Directions)command;
                    Output.WriteLine(Player.Move(direction) ? $"You moved {direction}." : "The way is shut!");
                    break;

                case Commands.Take:
                    if (string.IsNullOrEmpty(subject))
                    {
                        Output.WriteLine("This command requires a subject.");
                    }
                    else
                    {
                        Take(subject);
                    }
                    break;

                case Commands.Drop:
                    if (string.IsNullOrEmpty(subject))
                    {
                        Output.WriteLine("This command requires a subject.");
                    }
                    else
                    {
                        Drop(subject);
                    }
                    break;

                case Commands.Inventory:
                    if (Player.Inventory.Count() == 0)
                    {
                        Output.WriteLine("You are empty handed.");
                    }
                    else
                    {
                        Output.WriteLine("You are carrying:");
                        foreach (Item item in Player.Inventory)
                        {
                            Output.WriteLine(item.InventoryDescription);
                        }
                    }
                    break;

                case Commands.Reward:
                    Player.Score += 1;
                    break;

                case Commands.Score:
                    Output.WriteLine($"Your socre is {Player.Score} , your move is {Player.Moves}");
                    break;

                case Commands.Attack:
                    if (string.IsNullOrEmpty(subject))
                    {
                        Output.WriteLine("This command requires a target");
                    }
                    else if (string.IsNullOrEmpty(noun))
                    {
                        Output.WriteLine("with what?");
                    }
                    else
                    {
                        Attack(subject, preposition, noun);
                    }
                    break;

                default:
                    Output.WriteLine("Unknown command.");
                    break;
            }


            if (command != Commands.Unknown && command != Commands.Reward && command != Commands.Score)
            {
                Player.Moves++;
            }


            if (ReferenceEquals(previousRoom, Player.CurrentRoom) == false)
            {
                Look();
            }

        }

        private void Look()
        {
            Output.WriteLine($"{Player.CurrentRoom}");
            Output.WriteLine(Player.CurrentRoom.Description);
            foreach (Item item in Player.CurrentRoom.Inventory)
            {
                Output.WriteLine(item.LookDescription);
            }
            foreach (Enemy enemy in Player.CurrentRoom.Enemy)
            {
                Output.WriteLine(enemy.Description);
            }

        }

        private void Take(string itemName)
        {
            Item itemToTake = Player.CurrentRoom.Inventory.FirstOrDefault(item => string.Compare(item.Name, itemName, ignoreCase: true) == 0);
            if (itemToTake == null)
            {
                Output.WriteLine("You can't see any such thing.");
            }
            else
            {
                Player.AddItemToInventory(itemToTake);
                Player.CurrentRoom.RemoveItemFromInventory(itemToTake);
                Output.WriteLine("Taken.");
            }
        }

        private void Drop(string itemName)
        {
            Item itemToDrop = Player.Inventory.FirstOrDefault(item => string.Compare(item.Name, itemName, ignoreCase: true) == 0);
            if (itemToDrop == null)
            {
                Output.WriteLine("You can't see any such thing.");
            }
            else
            {
                Player.CurrentRoom.AddItemToInventory(itemToDrop);
                Player.RemoveItemFromInventory(itemToDrop);
                Output.WriteLine("Dropped.");
            }
        }

        void Attack(string enemyName, string preposition, string noun)
        {
            Enemy target = Player.CurrentRoom.Enemy.FirstOrDefault(target => string.Compare(target.Name, enemyName, true) == 0); //Find Valid Enemy
            Item item = Player.Inventory.FirstOrDefault(item => string.Compare(item.Name, noun, true) == 0);
            if (string.Compare(preposition, "with", true) == 0)
            {

                if (target == null)
                {
                    Output.WriteLine("You can't see any such thing.");
                    return;
                }


                else if (target != null && item == null)
                {

                    Output.WriteLine($"You don't have the {noun}.");
                    return;

                }

                else if (target != null && item != null && item.Weapon == false)
                {
                    Output.WriteLine($"You can't attack the {enemyName} with {item.Name}.");
                    return;
                }

                else
                {
                    target.TakeDamage(item.Damage);

                    if (target.HitPoints <= 0)
                    {
                        Output.WriteLine($"The {target.Name} is finally dead");
                        Player.CurrentRoom.RemoveEnemyFromRoom(target);
                    }
                    else
                    {
                        Output.WriteLine($"The {target.Name} is severly injured. The {target.Name} has {target.HitPoints} HP left");
                    }
                }
            }

            else
            {
                Output.WriteLine("Attack command tip: attack [Subject] with [Noun]");
            }
        }
        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.Unknown;
    }
}