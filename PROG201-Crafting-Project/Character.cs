using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using static PROG201_Crafting_Project.Utility;

namespace PROG201_Crafting_Project
{
    public class Character
    {
        public enum CharType
        {
            NPC,
            Player
        }

        public CharType Type { get; set; }

        public string Name { get; set; }

        public double Gold { get; set; }

        public int XP { get; set; }

        public List<Item> Inventory { get; set;}
        public List<Recipe> Recipes { get; set;}

        BindingList<Item> BoundInventory { get; set; }
        BindingList<Recipe> BoundRecipes { get; set; }

        public Character(CharType type, string _name, double _gold, int _xp, string _invfile, string _node)
        {
            Type = type;

            Name = _name;
            Gold = _gold;
            XP = _xp;

            Inventory = LoadItemsXML(_invfile, _node);
            SetBoundInventory();
        }

        public void SetBoundInventory() => BoundInventory = new BindingList<Item>(Inventory);
        public BindingList<Item> GetBoundInventory() => BoundInventory;

        public void SetBoundRecipes(Craft crafter)
        {
            Recipes = crafter.CheckRecipes(Inventory);

            BoundRecipes = new BindingList<Recipe>(Recipes);
        }
        public BindingList<Recipe> GetBoundRecipes() => BoundRecipes;


        public BindingList<Item> GetCraftedItems()
        {
            List<Item> crafteditems = Inventory.FindAll(item => item.Source == Item.ItemSource.Crafted);

            BindingList<Item> CraftedItems = new BindingList<Item>(crafteditems);

            return CraftedItems;
        }

        public void InventoryLoaded(UI ui, DataGrid datagrid, Grid grid, List<TextBlock> banner)
        {
            ui.SetBannerSource(this, banner);
            ui.SetGridSource(datagrid, GetBoundInventory());
            grid.Visibility = Visibility.Hidden;
        }


    }
}
