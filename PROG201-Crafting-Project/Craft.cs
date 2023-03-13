using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PROG201_Crafting_Project.Utility;

namespace PROG201_Crafting_Project
{
    internal class Craft
    {
        List<Recipe> Recipes = new List<Recipe>();

        public Craft()
        {
            Recipes = LoadRecipesXML();
        }

        public bool CheckCraftability(List<Item> _inventory, int index)
        {
            Recipe _recipe = Recipes[index];
            return !_recipe.Ingredients.Except(_inventory).Any();
        }
    }
}
