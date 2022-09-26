﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    public class Player
    {
        public Room CurrentRoom
        {
            get => _world.Rooms[_location.Row, _location.Column];
        }
        public int Score { get; }
        public int Moves { get; }

        public Player(World world)
        {
            _world = world;
        }
        World _world;
        static (int Row, int Column) _location = (1, 1);


        public bool Move(Commands command)
        {
            bool didMove = false;
            switch (command)
            {
                case Commands.NORTH when _location.Row < _world.Rooms.GetLength(0) - 1:
                    _location.Row++;
                    didMove = true;
                    break;

                case Commands.SOUTH when _location.Row > 0:
                    _location.Row--;
                    didMove = true;
                    break;

                case Commands.EAST when _location.Column < _world.Rooms.GetLength(1) - 1:
                    _location.Column++;
                    didMove = true;
                    break;

                case Commands.WEST when _location.Column > 0:
                    _location.Column--;
                    didMove = true;
                    break;
            }
            return didMove;
        }

        static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        static bool IsDirection(Commands command) => Directions.Contains(command);
    }
}
