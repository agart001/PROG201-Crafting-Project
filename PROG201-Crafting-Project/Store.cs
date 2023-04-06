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
        //Item is added to Buyer's inventory
        void AddItem(List<Item> buyer_inventory, Item item, double amount)
        {
            //Find item in buyer's inventory based on name
            List<Item> matches = buyer_inventory.FindAll(i => i.Name == item.Name);
            //Find item based on source and rarity
            Item i_item = matches.Find(i => CompareItems(i, item, 0) && CompareItems(i, item, 1));

            //Clone the item being sold, convert it to tsp, set count to requested amount
            Item sold_item = item.Clone();
            sold_item.ConvertToTsp();
            sold_item.Count = amount;

            if (i_item != null)
            {
                //Combine buyer item with bought item
                i_item.Combine(sold_item, true);
            }
            else
            {
                //Convert sold item into base units 
                if(sold_item.Count > 1) { sold_item.ConvertUnitToHigher(); }
                else { sold_item.ConvertUnitToLower(); }
                //Add sold item to buyer's inventory
                buyer_inventory.Add(sold_item);
            }
        }

        //Seller's item stock runs out
        void ItemRemoved(List<Item> seller_inventory, Item item)
        {
            seller_inventory.Remove(item);
        }

        //Seller's item stock is decremented
        void ItemDecremented(Item item, double amount)
        {

            item.Count -= amount;

            Math.Round(item.Count, 2);

            item.ConvertUnitToHigher();
        }
        
        //Cost is applied to both the buyer and seller
        void ApplyCost(Character buyer, Character seller, double cost)
        {
            seller.Gold = Math.Round(seller.Gold + cost, 2);
            buyer.Gold = Math.Round(buyer.Gold - cost, 2);
        }

        //Gets buying amount in tsp based on the item being bought
        double TspAmount(Item item, double amount)
        {
            switch (item.CountUnit.ToLower())
            {
                case "cup": amount *= 48; break;
                case "tbsp": amount *= 3; break;
                case "tsp":
                    break;
            }

            return amount;
        }

        //Begin item buying process
        void BuyItem(Character buyer, Character seller, Item item, double amount)
        {
            //Convert the amount purchased and the item being sold to tsp
            amount = TspAmount(item, amount);
            item.ConvertToTsp();

            //Buyer can not afford item or amount requested exceeds item's count
            if (buyer.Gold - (item.Value * amount) < 0 || amount > item.Count) return;

            List<Item> b_inv = buyer.Inventory;
            List<Item> s_inv = seller.Inventory;

            //Get total cost before shuffling inventories
            double cost = Math.Round(item.Value * amount,2);

            //Item count compared against amount requested
            if (item.Count - amount <= 0) 
            {
                ItemRemoved(s_inv, item);
            }
            else
            {
                ItemDecremented(item, amount);
            }
            
            //Item is added to buyers inventory
            AddItem(b_inv, item, amount);
            //Buyer pays cost, seller earns cost
            ApplyCost(buyer, seller, cost);

        }

        //Calculate player profit margin
        void ProfitMargin(List<Recipe> recipes, Item item)
        {
            //Find recipe based on item
            Recipe recipe = recipes.Find(i=> i.Result.Name == item.Name);
            Item result = recipe.Result;

            double cost = 0;
            //Cost of all ingredients
            foreach(Item ingredient in recipe.Ingredients) 
            {
                //Ingredient cost per tsp
                ingredient.ConvertToTsp();

                cost += ingredient.Count * ingredient.Value;

                ingredient.ConvertUnitToHigher();
            }

            //Calc profit using item value(per tsp) and ingredient cost(per tsp)
            result.ConvertToTsp();
            double result_val = result.Value * result.Count;

            cost = Math.Round(cost,2);
            result_val = Math.Round(result_val,2);

            double profit = result_val - cost;
            double percent = Math.Round((profit / result_val) * 100, 2);

            result.ConvertUnitToHigher(); 

            MessageBox.Show
            (
                $"Sale Breakdown:\n\r{item.Name}\n\r" +
                "--------------------\n\r" +
                $"Cost: {cost}\n\r" +
                $"Price: {result_val}\n\r" +
                $"Profit: {profit}\n\r" +
                $"Percentage: {percent}%"
            );
        }

        //Get seller inventory
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

        //Store Page - take page data and sets/displays
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

        //Store Page - take page data and begins the buying/selling process
        public void StoreClick(UI ui, Character buyer, Character seller,
            DataGrid buyer_grid, DataGrid seller_grid, 
            List<TextBlock> buyer_banner, List<TextBlock> seller_banner,
            Grid item_grid, TextBox buy_input)
        {
            Item item = seller_grid.SelectedItem as Item;
            double amount = Convert.ToDouble(buy_input.Text);

            //Unit check, no unit smaller than tsp
            if (item.CountUnit.ToLower() == "tsp" && amount < 0) return;


            BuyItem(buyer, seller, item, amount);

            //Display player's profit margin
            if(seller.Type == Character.CharType.Player)
            {
                Craft craft = new Craft();
                ProfitMargin(craft.Recipes, item);
            }
            

            buyer.SetBoundInventory();
            seller.SetBoundInventory();

            BindingList<Item> buyer_inventory = buyer.GetBoundInventory();
            //Set seller inventory, player can only sell crafted items
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
