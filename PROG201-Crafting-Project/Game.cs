﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PROG201_Crafting_Project
{
    public class Game
    {
        public Character Player;

        public Game() 
        {
            Player = new Character("Alex", 200, 0, "items", "player");
        }

    }
}
