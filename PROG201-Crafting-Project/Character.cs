using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PROG201_Crafting_Project.Utility;

namespace PROG201_Crafting_Project
{
    internal class Character
    {
        public string Name { get; set; }

        public int Gold { get; set; }

        public int XP { get; set; }

        public List<Item> Inventory = new List<Item>();

        public Character(string _name, int _gold, int _xp)
        {
            Name = _name;
            Gold = _gold;
            XP = _xp;
        }

        public Character(string _name, int _gold, int _xp, string _invfile, string _node)
        {
            Name = _name;
            Gold = _gold;
            XP = _xp;
            Inventory = LoadItemsXML(_invfile, _node);
        }
    }
}
