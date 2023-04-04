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

        void AddItem(List<Item> buyer_inventory, Item item, int amount)
        {
            List<Item> matches = buyer_inventory.FindAll(i => i.Name == item.Name);

            Item i_item = matches.Find(i => CompareItems(i, item, 0) && CompareItems(i, item, 1));
            Item sold_item = item.Clone();
            sold_item.Count = amount;

            if (i_item != null)
            {
                i_item.Count += amount;
            }
            else
            {
                buyer_inventory.Add(sold_item);
            }
        }

        void ItemRemoved(List<Item> buyer_inventory, List<Item> seller_inventory, Item item, int amount)
        {
            seller_inventory.Remove(item);

            AddItem(buyer_inventory, item, amount);
        }

        void ItemDecremented(List<Item> buyer_inventory, Item item, int amount)
        {
            item.Count -= amount;

            AddItem(buyer_inventory, item, amount);
        }
        
        void ApplyCost(Character buyer, Character seller, int cost)
        {
            seller.Gold += cost;
            buyer.Gold -= cost;
        }

        void BuyItem(Character buyer, Character seller, Item item, int amount)
        {
            if (buyer.Gold - (item.Value * amount) < 0 || amount > item.Count) return;

            List<Item> b_inv = buyer.Inventory;
            List<Item> s_inv = seller.Inventory;

            int cost = item.Value * amount;

            if(item.Count - amount <= 0) 
            {
                ItemRemoved(b_inv, s_inv, item, amount);
            }
            else
            {
                ItemDecremented(b_inv, item, amount);
            }

            ApplyCost(buyer, seller, cost);

            MessageBox.Show
            (
                $"{buyer.Name} Bought: {item.Name}\n\r" +
                "------------------\n\r" +
                $"Buy Price: {item.Value}\n\r" +
                $"{buyer.Name}'s Gold: {buyer.Gold}"
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
            int amount = Convert.ToInt32(buy_input.Text);
            BuyItem(buyer, seller, item, amount);

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
