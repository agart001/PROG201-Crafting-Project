using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PROG201_Crafting_Project.Utility;

namespace PROG201_Crafting_Project
{
    public class Craft
    {
        public List<Recipe> Recipes = new List<Recipe>();

        public Craft()
        {
            Recipes = LoadRecipesXML();
        }

        bool CheckCraftability(List<Item> _inventory, Recipe _recipe)
        {
            bool craftable = false;
            bool[] useable = new bool[_recipe.Ingredients.Count];
            int index = 0;
            foreach (Item ingredient in _recipe.Ingredients)
            {
                Item item = _inventory.Find(i_item => i_item.Name == ingredient.Name);
                if (item == null) break;

                if (item.Count >= ingredient.Count) 
                {
                    useable[index] = true;
                }

                index++;
            }

            craftable = useable.All(i => i == true);

            return craftable;
        }

        void CalcRarity(Item item)
        {

        }

        void RemoveItems(List<Item> _inventory, Recipe _recipe)
        {
            foreach (Item ingredient in _recipe.Ingredients)
            {
                Item item = _inventory.Find(i_item => i_item.Name == ingredient.Name);

                if (item.Count - ingredient.Count <= 0) { _inventory.Remove(item); }
                else { item.Count -= ingredient.Count; }
            }
        }

        void ExchangeItems(List<Item> _inventory, Recipe _recipe)
        {
            //_inventory.RemoveAll(i_item => _recipe.Ingredients.Any(r_item => i_item.Name == r_item.Name));
            RemoveItems(_inventory,_recipe);
            _inventory.Add(_recipe.Result);
        }

        public void CraftItem(List<Item> _inventory, int r_index)
        {
            Recipe _recipe = Recipes[r_index];

            if(CheckCraftability(_inventory, _recipe))
            {
                ExchangeItems(_inventory, _recipe);
            }
        }

        public List<Recipe> CheckRecipes(List<Item> _inventory)
        {
            List<Recipe> _recipes = new List<Recipe>();

            foreach(Recipe _recipe in Recipes) 
            {
                if(CheckCraftability(_inventory, _recipe))
                {
                    _recipes.Add(_recipe);
                }
            }

            return _recipes;
        }

    }
}
