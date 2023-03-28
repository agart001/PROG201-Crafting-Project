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

        public static void CloseApp() => Environment.Exit(0);

        public static string ConvertToLower(string str) { return str.ToLower(); }


        public static void Print(string str) => WriteLine(str);


        public static string InputStr()
        {
            string input = ReadLine();
            char[] char_array = input.ToArray();

            bool digit = false;

            foreach (char c in char_array)
            {
                if (Char.IsDigit(c))
                {
                    digit = true;
                }
            }

            if (digit)
            {
                Print("-ERROR- non string -- re enter");
                return InputStr();
            }
            else
            {
                return input;
            }
        }

        public static int InputInt()
        {

            string input = ReadLine();
            int int_input;
            char[] char_array = input.ToArray();

            bool digit = true;

            foreach (char c in char_array)
            {
                if (!Char.IsDigit(c))
                {
                    digit = false;
                }
            }

            if (digit)
            {
                int_input = Convert.ToInt32(input);
                return int_input;
            }
            else
            {
                Print("-ERROR- non int -- re enter");
                return InputInt();
            }
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

        public static List<Character> LoadCharactersXML()
        {
            string path = "../../../xml/characters.xml";
            List<Character> Characters = new List<Character>();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNode root = xml.DocumentElement;
            XmlNodeList CharacterList = root.SelectNodes("/characters/character");
            xml.AppendChild(root);
            foreach (XmlElement character in CharacterList)
            {
                string name = character.GetAttribute("name");
                int gold = Convert.ToInt32(character.GetAttribute("gold"));
                int xp = Convert.ToInt32(character.GetAttribute("xp"));
                string i_file = character.GetAttribute("i_file");
                string i_node = character.GetAttribute("i_node");

                Characters.Add
                    (new Character
                        (
                            name,
                            gold,
                            xp,
                            i_file,
                            i_node
                        )
                    );
            }

            return Characters;
        }

        public static List<Item> LoadItemsXML(string FileName, string NodePath)
        {
            string path = "../../../xml/" + FileName + ".xml";
            List<Item> Items = new List<Item>();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNode root = xml.DocumentElement;
            XmlNodeList ItemsList = root.SelectNodes("/AllItems/"+NodePath+"/items/item");
            xml.AppendChild(root);

            Items = ParseItems(ItemsList);

            return Items;
        }

        public static List<Recipe> LoadRecipesXML()
        {
            string path = "../../../xml/recipes.xml";
            List<Recipe> Recipes = new List<Recipe>();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNode root = xml.DocumentElement;
            XmlNodeList RecipeList = root.SelectNodes("/recipes/recipe");
            xml.AppendChild(root);
            foreach (XmlElement _recipe in RecipeList)
            {
                Item.ItemRarity rarity = (Item.ItemRarity)Convert.ToInt32(_recipe.GetAttribute("rarity"));
                Item.ItemType type = (Item.ItemType)Convert.ToInt32(_recipe.GetAttribute("type"));
                Item.ItemSource source = (Item.ItemSource)Convert.ToInt32(_recipe.GetAttribute("source"));

                BitmapImage image = ParseItemImage(type, rarity, _recipe.GetAttribute("image"));

                int value = Convert.ToInt32(_recipe.GetAttribute("value"));
                int count = Convert.ToInt32(_recipe.GetAttribute("count"));

                XmlNodeList IngredientsList = _recipe.SelectNodes("ingredients/ingredient");
                List<Item> _ingredients = ParseItems(IngredientsList);
                //add the instance to the list that will be returned from method
                Recipes.Add(new Recipe
                {
                    //object initialization with public class fields
                    Result = new Item
                    {
                        Rarity = rarity,
                        Type = type,
                        Source = source,

                        Image = image,

                        Name = _recipe.GetAttribute("name"),
                        Desc = _recipe.GetAttribute("desc"),

                        Value = value,
                        Count= count
                    },
                    Ingredients = _ingredients
                });
            }

            return Recipes;
        }

        static BitmapImage ParseItemImage(Item.ItemType type, Item.ItemRarity rarity, string file) 
        {
            string type_folder = ConvertToLower(type.ToString());
            string rarity_folder = ConvertToLower(rarity.ToString());
            string path = $"/../images/item/{type_folder}/{file}.BMP";
            BitmapImage image = new BitmapImage(new Uri(path, UriKind.Relative));

            if (image == null)
            {
                image = new BitmapImage(new Uri("/../images/default.BMP", UriKind.Relative));
            }

            return image;
        }

        static List<Item> ParseItems(XmlNodeList List)
        {
            List<Item> Items = new List<Item>();

            foreach (XmlElement _item in List)
            {
                Item.ItemRarity rarity = (Item.ItemRarity)Convert.ToInt32(_item.GetAttribute("rarity"));
                Item.ItemType type = (Item.ItemType)Convert.ToInt32(_item.GetAttribute("type"));
                Item.ItemSource source = (Item.ItemSource)Convert.ToInt32(_item.GetAttribute("source"));

                BitmapImage image = ParseItemImage(type, rarity, _item.GetAttribute("image"));

                int value = Convert.ToInt32(_item.GetAttribute("value"));
                int count = Convert.ToInt32(_item.GetAttribute("count"));

                Items.Add(new Item
                {
                    Rarity = rarity,
                    Type = type,
                    Source = source,

                    Image = image,

                    Name = _item.GetAttribute("name"),
                    Desc = _item.GetAttribute("desc"),

                    Value = value,
                    Count = count
                });
            }

            return Items;
        }

    }
}
