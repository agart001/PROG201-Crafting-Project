using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
    }
}
