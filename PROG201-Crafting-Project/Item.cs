using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PROG201_Crafting_Project
{
    public class Item
    {
        public enum ItemRarity
        {
            Common,
            Uncommon,
            Rare,
            Epic
        }

        public enum ItemType
        {
            Material,
            Consumable,
            Equipment
        }

        public ItemRarity Rarity { get; set; }

        public ItemType Type { get; set; }

        public BitmapImage Image { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public int Value { get; set; }

        public int Count { get; set; }
    }
}
