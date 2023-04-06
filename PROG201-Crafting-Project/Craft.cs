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

        //Checks the craftability of recipe against the player's inventory
        bool CheckCraftability(List<Item> inventory, Recipe recipe)
        {
            //Boolean variables 
            bool craftable = false;
            bool[] useable = new bool[recipe.Ingredients.Count];
            int index = 0;
            foreach (Item ingredient in recipe.Ingredients)
            {
                //Find ingredient in player inventory
                Item item = inventory.Find(i_item => i_item.Name == ingredient.Name);

                if (item == null) break;

                //Convert player item and ingredient to tsp
                item.ConvertToTsp();
                ingredient.ConvertToTsp();
                
                if (item.Count >= ingredient.Count) 
                {
                    //useable item
                    useable[index] = true;
                }

                //Convert player item and ingredient back to base unit
                item.ConvertUnitToHigher();
                ingredient.ConvertUnitToHigher();

                index++;
            }

            //Craftable - if all items are useable
            craftable = useable.All(i => i == true);

            return craftable;
        }

        //Checks player inventory for craftable recipes and returns that list
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

        //Begins result generation 
        void GenerateItem(Item item)
        { 
            //Calculates result's rarity
            Item.ItemRarity base_rarity = CalcRarity(item);

            //Based on result rarity, change result's value and xp reward
            if (item.Rarity != base_rarity) 
            { 
                CalcValue(item);
                CalcXP(item);
            }

            MessageBox.Show
            (
                $"Crafted: {item.Name}\n\r" +
                "------------------\n\r" +
                $"Rarirty: {item.Rarity}\n\r" +
                $"Value: {item.Value}\n\r" +
                $"XP: {item.XP}"
            );
        }


        void AddResult(List<Item> inventory, Item result)
        {
            //Generate the recipe's result - random chance of rarer result
            GenerateItem(result);

            //Find item in player inventory based on name
            List<Item> matches = inventory.FindAll(i => i.Name == result.Name);

            //Find item in player inventory based on source and rarity
            Item i_item = matches.Find(i => CompareItems(i, result, 0) && CompareItems(i, result, 1));

            if (i_item != null)
            {
                //Combine inventory item with the result
                i_item.Combine(result, true);
            }
            else
            {
                inventory.Add(result);
            }
        }

        Item BiasItemSource(List<Item> items)
        {
            Item item;

            List<Item> sorted = items.OrderBy(i => i.Source).ToList();

            item = sorted.First();

            return item;
        }

        void RemoveIngredients(List<Item> inventory, Recipe recipe)
        {
            foreach (Item ingredient in recipe.Ingredients)
            {
                //Find ingredient in inventory
                List<Item> items = inventory.FindAll(i_item => i_item.Name == ingredient.Name);
                //Bias in order - found/bought/crafted
                Item item = BiasItemSource(items);

                //Remove ingredient count from inventory item
                item.Combine(ingredient, false);

                if (item.Count <= 0) { inventory.Remove(item); }
            }
        }

        //Begin Exchanging player inventory items for a recipe's result
        void ExchangeItems(List<Item> inventory, Recipe recipe)
        {
            Item result = recipe.Result;

            //Remove a recipe's ingredient from the player's inventory
            RemoveIngredients(inventory, recipe);
            //Add a recipe's result to the player's inventory
            AddResult(inventory, result);
        }

        //Begin Crafting process - takes character/recipe, checks craftability, begins exchanging ingredient items for the recipe result
        public void CraftItem(Character character, Recipe recipe)
        {
            List<Item> inventory = character.Inventory;
            Item result = recipe.Result;

            //Checks the craftability of a recipe against the player's inventory
            if (CheckCraftability(inventory, recipe) != true) return;

            //Exchanges player's inventory items for the recipe's result and reward the player with the result's xp
            ExchangeItems(inventory, recipe);
            character.XP += result.XP;
        }

        //Craft page is loaded - takes page data and sets/displays it
        public void CraftLoaded(UI ui, Character character, DataGrid datagrid, Grid grid, List<TextBlock> banner)
        {
            ui.SetBannerSource(character, banner);
            ui.SetGridSource(datagrid, character.GetBoundRecipes());

            grid.Visibility = Visibility.Hidden;
        }

        //Button to craft item is clicked - takes page data and begins the crafting process
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
