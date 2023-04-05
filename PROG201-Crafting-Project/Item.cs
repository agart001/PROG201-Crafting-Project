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

        public enum ItemSource
        {
            Found,
            Bought,
            Crafted
        }

        public ItemRarity Rarity { get; set; }

        public ItemType Type { get; set; }

        public ItemSource Source { get; set; }


        public BitmapImage Image { get; set; }


        public string Name { get; set; }

        public string Desc { get; set; }


        public double Value { get; set; }

        public int XP { get; set; }


        public double Count { get; set; }

        public string CountUnit { get; set; }


        public Item(ItemRarity rarity, ItemType type, ItemSource source, BitmapImage image, string name, string desc, double value, double count, string countunit)
        {
            Rarity = rarity;
            Type = type;
            Source = source;
            Image = image;
            Name = name;
            Desc = desc;
            Value = value;
            Count = count;
            CountUnit = countunit;

            if (Count < 1) ConvertUnitTolower();
            else ConvertUnitToHigher();
        }

        public Item(ItemRarity rarity, ItemType type, ItemSource source, BitmapImage image, string name, string desc, double value, int xp, double count, string countunit)
        {
            Rarity = rarity;
            Type = type;
            Source = source;
            Image = image;
            Name = name;
            Desc = desc;
            Value = value;
            XP = xp;
            Count = count;
            CountUnit = countunit;

            if (Count < 1) ConvertUnitTolower();
            else ConvertUnitToHigher();
        }


        public Item Clone() => (Item)this.MemberwiseClone();

        public void ConvertUnitToHigher()
        {
            switch (CountUnit.ToLower()) 
            {
                case "tsp": if(Count >= 3) { Count = 1; CountUnit = "Tbsp"; } goto case "tbsp";
                case "tbsp": if(Count >= 16) { Count = 1; CountUnit = "Cup"; } goto case "cup";
                case "cup":
                    break;
            }
        }

        public void ConvertUnitTolower()
        {
            switch (CountUnit.ToLower())
            {
                case "cup": if (Count < 1) { Count *= 16; CountUnit = "Tbsp"; } goto case "tbsp";
                case "tbsp": if (Count < 1) { Count *= 3; CountUnit = "Tsp"; } goto case "tsp";
                case "tsp":
                    break;
            }
        }
    }
}
