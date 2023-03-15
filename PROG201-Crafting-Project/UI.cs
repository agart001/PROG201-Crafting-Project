using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PROG201_Crafting_Project
{
    public class UI
    {
        Frame main;

        public UI(Frame frame)
        {
            main = frame;
        }

        //update game page
        public void UpdatePage(string _page)
        {
            string page = "pages/" + _page + "_page.xaml";
            Uri uri = new Uri(page, UriKind.Relative);
            main.Source = uri;
        }

        public void ToggleVis(UIElement element)
        {
            Visibility show = Visibility.Visible;
            Visibility hide = Visibility.Hidden;

            Visibility vis = element.Visibility;

            if (vis == show)
            {
                element.Visibility = hide;
            }
            else if (vis == hide)
            {
                element.Visibility = show;
            }
        }

        public void InventoryInfo(Character actor, TextBlock textblock)
        {
            textblock.Text = string.Empty;

            int i = 0;
            textblock.Text = actor.Name + "      Gold: " + actor.Gold + "\n\r";
            foreach (Item item in actor.Inventory)
            {
                i++;
                textblock.Text += "Item #" + i + ":" + "\n\r" +
                    "Name: {" + item.Name +
                    "} Value: {" + item.Value + "} \n\r" +
                    "Desc: (" + item.Desc + ") \n\r";
            }
        }

        public void RecipeInfo(List<Recipe> _recipes, TextBlock textblock)
        {
            textblock.Text = string.Empty;

            int i = 0;
            foreach (Recipe _recipe in _recipes)
            {
                i++;
                Item _result = _recipe.Result;
                textblock.Text += "Item #" + i + ":" + "\n\r" +
                    "Name: {" + _result.Name +
                    "} Value: {" + _result.Value + "} \n\r" +
                    "Desc: (" + _result.Desc + ") \n\r";
            }
        }

    }
}
