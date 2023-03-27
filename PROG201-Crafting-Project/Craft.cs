using System;
using System.Collections.Generic;
using System.Configuration;
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
            Item.ItemRarity base_rarity = item.Rarity;
            int i_rarity = (int)base_rarity;
            int len = Enum.GetNames(typeof(Item.ItemRarity)).Length;

            if(i_rarity+ 2 > len) { return; }

            int seed = Rand.Next(0, 10);

            seed = 6;

            if(seed <= 7)
            {
                item.Rarity = base_rarity;
            }

            if(seed > 7 && seed <= 9)
            {
                item.Rarity = base_rarity + 1;
            }
            
            if(seed > 9)
            {
                item.Rarity = base_rarity + 2;
            }


            if(item.Rarity != base_rarity) { CalcValue(item); }
        }

        void CalcValue(Item item)
        {
            double factor = ((int)item.Rarity) + 1;
            double applied = ((factor * 10) + 100) / 100;
            double calc_value = item.Value * applied;
            item.Value = (int)calc_value;
        }

        void GenerateItem(Item item)
        {
            CalcRarity(item);
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

        void AddItem(List<Item> _inventory, Item result)
        {
            Item item = _inventory.Find(i_item => i_item.Name == result.Name);

            GenerateItem(result);

            if (item == null)
            {
                _inventory.Add(result);
            }
            else
            {
                if (item.Rarity == result.Rarity && item.Source == result.Source)
                {
                    item.Count += result.Count;
                }
                else
                {
                    _inventory.Add(result);
                }
            }

        }

        void ExchangeItems(List<Item> _inventory, Recipe _recipe)
        {
            RemoveItems(_inventory, _recipe);
            AddItem(_inventory, _recipe.Result);
        }

        public void CraftItem(List<Item> _inventory, Recipe _recipe)
        {

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
