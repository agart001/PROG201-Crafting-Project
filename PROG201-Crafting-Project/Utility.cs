using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using static System.Console;

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

        public static List<Item> LoadItemsXML(string FileName, string NodePath)
        {
            string path = "../../../xml/" + FileName + ".xml";
            List<Item> Items = new List<Item>();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNode root = xml.DocumentElement;
            XmlNodeList ItemsList = root.SelectNodes("/AllItems/"+NodePath+"/items/item");
            xml.AppendChild(root);
            foreach (XmlElement _item in ItemsList)
            {
                int value = Convert.ToInt32(_item.GetAttribute("value"));
                //add the instance to the list that will be returned from method
                Items.Add(new Item
                {
                    //object initialization with public class fields
                    Name = _item.GetAttribute("name"),
                    Desc = _item.GetAttribute("desc"),
                    Value = value
                });
            }

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
                int _value = Convert.ToInt32(_recipe.GetAttribute("value"));

                XmlNodeList IngredientsList = _recipe.SelectNodes("ingredients/ingredient");
                List<Item> _ingredients = GetIngredients(IngredientsList);
                //add the instance to the list that will be returned from method
                Recipes.Add(new Recipe
                {
                    //object initialization with public class fields
                    Result = new Item
                    {
                        Name = _recipe.GetAttribute("name"),
                        Desc = _recipe.GetAttribute("desc"),
                        Value = _value
                    },
                    Ingredients = _ingredients
                });
            }

            return Recipes;
        }

        static List<Item> GetIngredients(XmlNodeList List)
        {
            List<Item> Ingredients = new List<Item>();

            foreach (XmlElement _ingredient in List)
            {
                int _value = Convert.ToInt32(_ingredient.GetAttribute("value"));

                Ingredients.Add(new Item
                {
                    Name = _ingredient.GetAttribute("name"),
                    Desc = _ingredient.GetAttribute("desc"),
                    Value = _value
                });
            }

            return Ingredients;
        }

    }
}
