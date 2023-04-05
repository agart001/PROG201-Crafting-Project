using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static PROG201_Crafting_Project.Utility;

namespace PROG201_Crafting_Project
{
    public class Store
    {

        void AddItem(List<Item> buyer_inventory, Item item, double amount)
        {
            List<Item> matches = buyer_inventory.FindAll(i => i.Name == item.Name);

            Item i_item = matches.Find(i => CompareItems(i, item, 0) && CompareItems(i, item, 1));
            Item sold_item = item.Clone();
            sold_item.Count = amount;

            if (i_item != null)
            {
                i_item.Count += amount;
                i_item.ConvertUnitToHigher();
            }
            else
            {
                buyer_inventory.Add(sold_item);
            }
        }

        void ItemRemoved(List<Item> buyer_inventory, List<Item> seller_inventory, Item item, double amount)
        {
            seller_inventory.Remove(item);

            AddItem(buyer_inventory, item, amount);
        }

        void ItemDecremented(List<Item> buyer_inventory, Item item, double amount)
        {
            item.Count -= amount;
            item.ConvertUnitTolower();

            AddItem(buyer_inventory, item, amount);
        }
        
        void ApplyCost(Character buyer, Character seller, double cost)
        {
            seller.Gold += cost;
            buyer.Gold -= cost;
        }

        void BuyItem(Character buyer, Character seller, Item item, double amount)
        {
            if (buyer.Gold - (item.Value * amount) < 0 || amount > item.Count) return;

            List<Item> b_inv = buyer.Inventory;
            List<Item> s_inv = seller.Inventory;

            double cost = Math.Round(item.Value * amount,2);

            if(item.Count - amount <= 0) 
            {
                ItemRemoved(b_inv, s_inv, item, amount);
            }
            else
            {
                ItemDecremented(b_inv, item, amount);
            }

            ApplyCost(buyer, seller, cost);

        }

        void ProfitMargin(List<Recipe> recipes, Item item)
        {
            Recipe recipe = recipes.Find(i=> i.Result.Name == item.Name);
            Item result = recipe.Result;

            double cost = 0;

            foreach(Item ingredient in recipe.Ingredients) 
            {
                cost += ingredient.Count * ingredient.Value;
            }

            double profit = result.Value - cost;
            double percent = Math.Round((profit / result.Value) * 100, 2);

            MessageBox.Show
            (
                $"Sale Breakdown:\n\r{item.Name}\n\r" +
                "--------------------\n\r" +
                $"Cost: {cost}\n\r" +
                $"Price: {item.Value}\n\r" +
                $"Profit: {profit}\n\r" +
                $"Percentage: {percent}%"
            );
        }

        BindingList<Item> SellerType(Character seller)
        {
            BindingList<Item> items;

            if (seller.Type == Character.CharType.Player)
            {
                items = seller.GetCraftedItems();
            }
            else { items = seller.GetBoundInventory(); }

            return items;
        }

        public void StoreLoaded(UI ui, Character buyer, Character seller,
            DataGrid buyer_grid, DataGrid seller_grid,
            List<TextBlock> buyer_banner, List<TextBlock> seller_banner,
            Grid item_grid)
        {
            ui.SetBannerSource(buyer, buyer_banner);
            ui.SetBannerSource(seller, seller_banner);

            ui.SetGridSource(buyer_grid, buyer.GetBoundInventory());
            ui.SetGridSource(seller_grid, SellerType(seller));

            item_grid.Visibility = Visibility.Hidden;
        }

        public void StoreClick(UI ui, Character buyer, Character seller,
            DataGrid buyer_grid, DataGrid seller_grid, 
            List<TextBlock> buyer_banner, List<TextBlock> seller_banner,
            Grid item_grid, TextBox buy_input)
        {
            Item item = seller_grid.SelectedItem as Item;
            double amount = Convert.ToDouble(buy_input.Text);

            BuyItem(buyer, seller, item, amount);

            
            if(seller.Type == Character.CharType.Player)
            {
                Craft craft = new Craft();
                ProfitMargin(craft.Recipes, item);
            }
            

            buyer.SetBoundInventory();
            seller.SetBoundInventory();

            BindingList<Item> buyer_inventory = buyer.GetBoundInventory();
            BindingList<Item> seller_inventory = SellerType(seller);

            ui.SetGridSource(buyer_grid, buyer_inventory);
            ui.SetGridSource(seller_grid,seller_inventory);

            ui.SetBannerSource(buyer, buyer_banner);
            ui.SetBannerSource(seller, seller_banner);

            seller_grid.SelectedIndex = -1;

            item_grid.Visibility = Visibility.Hidden;
        }
    }
}
