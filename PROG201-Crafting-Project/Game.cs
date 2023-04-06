using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static PROG201_Crafting_Project.Utility;

namespace PROG201_Crafting_Project
{
    public class Game
    {
        public Craft Crafter;

        public Store Vendor;

        public Character Player;

        public List<Character> NPCs;

        //Used for Data context purposes
        //--
        public BindingList<Item> PlayerInventory;
        public BindingList<Recipe> PlayerRecipes;
        //--


        public Game() 
        {
            Crafter = new Craft();

            Vendor = new Store();

            Player = new Character(Character.CharType.Player,"Alex", 40, 0, "items", "player");

            NPCs = LoadCharactersXML();
        }

    }
}
