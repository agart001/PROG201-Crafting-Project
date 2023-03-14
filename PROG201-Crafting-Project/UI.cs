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
    internal class UI
    {
        Frame main;

        public UI(Frame frame)
        {
            main = frame;
        }

        //update game page
        public void update_page(string _page)
        {
            string page = "pages/" + _page + "_page.xaml";
            Uri uri = new Uri(page, UriKind.Relative);
            main.Source = uri;
        }

        public void toggle_vis(UIElement element)
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

        public void inventory_info(Character actor, TextBlock textblock)
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

    }
}
