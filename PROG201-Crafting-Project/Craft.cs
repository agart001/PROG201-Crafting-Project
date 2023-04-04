using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        Item BiasItemSource(List<Item> items)
        {
            Item item = null;

            List<Item> sorted = items.OrderBy(i => i.Source).ToList();

            item = sorted.First();

            return item;
        }

        bool CheckCraftability(List<Item> inventory, Recipe recipe)
        {
            bool craftable = false;
            bool[] useable = new bool[recipe.Ingredients.Count];
            int index = 0;
            foreach (Item ingredient in recipe.Ingredients)
            {
                Item item = inventory.Find(i_item => i_item.Name == ingredient.Name);

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

        public List<Recipe> CheckRecipes(List<Item> inventory)
        {
            List<Recipe> recipes = new List<Recipe>();

            foreach (Recipe recipe in Recipes)
            {
                if (CheckCraftability(inventory, recipe))
                {
                    recipes.Add(recipe);
                }
            }

            return recipes;
        }

        Item.ItemRarity CalcRarity(Item item)
        {
            Item.ItemRarity base_rarity = item.Rarity;
            int i_rarity = (int)base_rarity;
            int len = Enum.GetNames(typeof(Item.ItemRarity)).Length;

            if(i_rarity+ 2 > len) { return base_rarity; }

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

            return base_rarity;
        }

        void CalcValue(Item item)
        {
            double factor = ((int)item.Rarity) + 1;
            double applied = ((factor * 10) + 100) / 100;
            double calc_value = item.Value * applied;
            item.Value = (int)calc_value;
        }

        void CalcXP(Item item)
        {
            double factor = ((int)item.Rarity) + 1;
            double applied = ((factor * 10) + 100) / 100;
            double calc_value = item.XP * applied;
            item.XP = (int)calc_value;
        }

        void GenerateItem(Item item)
        { 
            Item.ItemRarity base_rarity = CalcRarity(item);
            if (item.Rarity != base_rarity) { CalcValue(item); }
            if (item.Rarity != base_rarity) { CalcXP(item); }
        }

        void RemoveIngredients(List<Item> inventory, Recipe recipe)
        {
            foreach (Item ingredient in recipe.Ingredients)
            {
                List<Item> items = inventory.FindAll(i_item => i_item.Name == ingredient.Name);
                Item item = BiasItemSource(items);

                if (item.Count - ingredient.Count <= 0) { inventory.Remove(item); }
                else { item.Count -= ingredient.Count; }
            }
        }

        void AddResult(List<Item> inventory, Item result)
        {
            GenerateItem(result);

            List<Item> matches = inventory.FindAll(i => i.Name == result.Name);

            Item i_item = matches.Find(i => CompareItems(i, result, 0) && CompareItems(i, result, 1));

            if (i_item != null)
            {
                i_item.Count += result.Count;
            }
            else
            {
                inventory.Add(result);
            }

        }

        void ExchangeItems(List<Item> inventory, Recipe recipe)
        {
            Item result = recipe.Result;

            RemoveIngredients(inventory, recipe);
            AddResult(inventory, result);
        }

        public void CraftItem(Character character, Recipe recipe)
        {
            List<Item> inventory = character.Inventory;
            Item result = recipe.Result;

            if (CheckCraftability(inventory, recipe) != true) return;

            ExchangeItems(inventory, recipe);
            character.XP += result.XP;
        }

        public void CraftLoaded(UI ui, Character character, DataGrid datagrid, Grid grid, List<TextBlock> banner)
        {
            ui.SetBannerSource(character, banner);
            ui.SetGridSource(datagrid, character.GetBoundRecipes());

            grid.Visibility = Visibility.Hidden;
        }

        public void CraftClick(UI ui, Character character, DataGrid datagrid, Grid grid, List<TextBlock> banner)
        {
            Recipe recipe = datagrid.SelectedItem as Recipe;

            if (recipe == null) return;

            CraftItem(character, recipe);

            ui.SetBannerSource(character, banner);

            character.SetBoundRecipes(this);
            ui.SetGridSource(datagrid, character.GetBoundRecipes());
            datagrid.SelectedIndex = -1;

            grid.Visibility = Visibility.Hidden;
        }

    }
}
