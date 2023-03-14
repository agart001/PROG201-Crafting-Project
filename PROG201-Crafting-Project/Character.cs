using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG201_Crafting_Project
{
    internal class Character
    {
        public string Name { get; set; }

        public int Gold { get; set; }

        public List<Item> Inventory { get; set; }
    }
}
