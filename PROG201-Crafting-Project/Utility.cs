using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using static System.Console;
using System.Security.Policy;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Xml.Linq;

namespace PROG201_Crafting_Project
{
    internal static class Utility
    {
        public static Random Rand = new Random();

        public static void SetRandomSeed(double seed) => Rand = new Random((int)seed);

        public static void CloseApp() => Environment.Exit(0);

        public static string ConvertToLower(string str) { return str.ToLower(); }

        public static bool IsDigit(string input)
        {
            char[] char_array = input.ToArray();

            bool digit = false;
            bool[] digits = new bool[char_array.Length];
            int index = 0;

            foreach (char c in char_array)
            {
                if (Char.IsDigit(c)) digits[index] = true;

                index++;
            }

            digit = digits.All(i => i == true);

            return digit;
        }

        public static bool IsDouble(string input)
        {
            char[] char_array = input.ToArray();

            char_array = char_array.Where(c => c != '.').ToArray();

            bool dbl = false;
            bool[] digits = new bool[char_array.Length];

            int dotindex = 0;
            int index = 0;

            foreach (char c in char_array)
            {
                if (Char.IsDigit(c)) digits[index] = true;

                if(c == '.') dotindex = index;

                index++;
            }

            dbl = digits.All(i => i == true);

            return dbl;
        }

        public static bool IsFraction(string input)
        {
            char[] char_array = input.ToArray();
            char_array = char_array.Where(c => c != '/' && c != ' ').ToArray();

            bool fraction = false;
            bool[] digits = new bool[char_array.Length];

            int slashindex = 0;
            int index = 0;

            foreach (char c in char_array)
            {
                if (Char.IsDigit(c)) digits[index] = true;

                if (c == '/') slashindex = index;

                index++;
            }

            fraction = digits.All(i => i == true);

            return fraction;
        }

        public static bool CompareItems(Item item_1, Item item_2, int type)
        {
            bool result = false;

            switch (type)
            {
                case 0: if(item_1.Rarity == item_2.Rarity) result= true; break;
                case 1: if (item_1.Source == item_2.Source) result = true; break;
            }
            return result;
        }

        //Load NPCs
        public static List<Character> LoadCharactersXML()
        {
            //NPC file path setup
            string path = "../../../xml/characters.xml";
            List<Character> Characters = new List<Character>();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNode root = xml.DocumentElement;
            XmlNodeList CharacterList = root.SelectNodes("/characters/character");
            xml.AppendChild(root);
            //Parse NPCs in file
            foreach (XmlElement character in CharacterList)
            {
                Character.CharType type = (Character.CharType)Convert.ToInt32(character.GetAttribute("type"));

                string name = character.GetAttribute("name");

                double gold = Convert.ToDouble(character.GetAttribute("gold"));
                int xp = Convert.ToInt32(character.GetAttribute("xp"));

                string i_file = character.GetAttribute("i_file");
                string i_node = character.GetAttribute("i_node");

                Characters.Add(new Character
                        (
                            type,
                            name,
                            gold,
                            xp,
                            i_file,
                            i_node
                        ));
            }

            return Characters;
        }

        //Load Items
        public static List<Item> LoadItemsXML(string FileName, string NodePath)
        {
            //Item file path setup
            string path = "../../../xml/" + FileName + ".xml";
            List<Item> Items = new List<Item>();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNode root = xml.DocumentElement;
            XmlNodeList ItemsList = root.SelectNodes("/AllItems/"+NodePath+"/items/item");
            xml.AppendChild(root);

            //Parse Items
            Items = ParseItems(ItemsList);

            return Items;
        }

        //Load Recipes
        public static List<Recipe> LoadRecipesXML()
        {
            //Recipe file path setup
            string path = "../../../xml/recipes.xml";
            List<Recipe> Recipes = new List<Recipe>();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNode root = xml.DocumentElement;
            XmlNodeList RecipeList = root.SelectNodes("/recipes/recipe");
            xml.AppendChild(root);
            //Parse recipes in file
            foreach (XmlElement _recipe in RecipeList)
            {
                Item.ItemRarity rarity = (Item.ItemRarity)Convert.ToInt32(_recipe.GetAttribute("rarity"));
                Item.ItemType type = (Item.ItemType)Convert.ToInt32(_recipe.GetAttribute("type"));
                Item.ItemSource source = (Item.ItemSource)Convert.ToInt32(_recipe.GetAttribute("source"));

                BitmapImage image = ParseItemImage(type, _recipe.GetAttribute("image"));

                double value = Convert.ToDouble(_recipe.GetAttribute("value"));
                double count = Convert.ToDouble(_recipe.GetAttribute("count"));
                int xp = Convert.ToInt32(_recipe.GetAttribute("xp"));

                XmlNodeList IngredientsList = _recipe.SelectNodes("ingredients/ingredient");
                //Parse ingredients in recipe
                List<Item> _ingredients = ParseItems(IngredientsList);

                Recipes.Add(new Recipe
                {
                    Result = new Item
                    (
                        rarity,
                        type,
                        source,

                        image,

                        _recipe.GetAttribute("name"),
                        _recipe.GetAttribute("desc"),

                        value,
                        xp,

                        count,
                        _recipe.GetAttribute("countunit")
                    ),
                    Ingredients = _ingredients
                });
            }

            return Recipes;
        }

        //Parse Item image from type folder
        static BitmapImage ParseItemImage(Item.ItemType type, string file) 
        {
            string type_folder = ConvertToLower(type.ToString());
            string path = $"/../images/item/{type_folder}/{file}";
            BitmapImage image = new BitmapImage(new Uri(path, UriKind.Relative));

            if (image == null)
            {
                image = new BitmapImage(new Uri("/../images/default.BMP", UriKind.Relative));
            }

            return image;
        }

        //Parse Items from Xml list
        static List<Item> ParseItems(XmlNodeList List)
        {
            List<Item> Items = new List<Item>();

            foreach (XmlElement _item in List)
            {
                Item.ItemRarity rarity = (Item.ItemRarity)Convert.ToInt32(_item.GetAttribute("rarity"));
                Item.ItemType type = (Item.ItemType)Convert.ToInt32(_item.GetAttribute("type"));
                Item.ItemSource source = (Item.ItemSource)Convert.ToInt32(_item.GetAttribute("source"));

                BitmapImage image = ParseItemImage(type, _item.GetAttribute("image"));

                double value = Convert.ToDouble(_item.GetAttribute("value"));
                double count = Convert.ToDouble(_item.GetAttribute("count"));

                Item item = new Item
                (
                    rarity,
                    type,
                    source,

                    image,

                    _item.GetAttribute("name"),
                    _item.GetAttribute("desc"),

                    value,

                    count,
                    _item.GetAttribute("countunit")
                );

                item.ConvertUnitToHigher();

                Items.Add(item);
            }

            return Items;
        }
    }
}
