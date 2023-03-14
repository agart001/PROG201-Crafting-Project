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

        //--
        //stack overflow
        //source uri: https://stackoverflow.com/questions/47421110/check-if-a-list-contains-all-of-another-lists-items-when-comparing-on-one-proper
        bool CheckCraftability(List<Item> _inventory, Recipe _recipe)
        {
            return _recipe.Ingredients.All(r_item => _inventory.Any(i_item => i_item.Name == r_item.Name)); 
        }
        //--

        void RemoveCraftItems(List<Item> _inventory, Recipe _recipe)
        {
            foreach (Item ingredient in _recipe.Ingredients) 
            { 
                _inventory.Remove(ingredient);
            }
        }

        public void CraftItem(List<Item> _inventory, int r_index)
        {
            Recipe _recipe = Recipes[r_index];

            if(CheckCraftability(_inventory, _recipe))
            {
                RemoveCraftItems(_inventory, _recipe);
                _inventory.Add(_recipe.Result);
            }
        }
    }
}
