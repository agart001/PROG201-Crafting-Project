using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG201_Crafting_Project
{
    public class Store
    {

        void AddItem(List<Item> buyer_inventory, Item item, int amount)
        {
            Item i_item = buyer_inventory.Find(i => i.Name == item.Name);

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
        

        public void BuyItem(Character buyer, Character seller, Item item, int amount)
        {
            if (amount > item.Count) return;

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

            seller.Gold += cost;
            buyer.Gold -= cost;
        }
    }
}
