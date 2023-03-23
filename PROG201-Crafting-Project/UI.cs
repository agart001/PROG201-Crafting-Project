using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void SetGridSource(DataGrid grid, BindingList<Item> List) => grid.ItemsSource = List;

        public void SetGridSource(DataGrid grid, BindingList<Recipe> List) => grid.ItemsSource = List;


        public void SelectedGrid(BindingList<Item> List, DataGrid grid, Image image, List<TextBlock> blocks)
        {
            if (grid.SelectedItem != null && grid.Items.Count != 0)
            {
                Item SelectedItem = grid.SelectedItem as Item;

                string rarity = SelectedItem.Rarity.ToString();
                string type = SelectedItem.Type.ToString();

                string value = SelectedItem.Value.ToString();
                string count = SelectedItem.Count.ToString();

                image.Source = SelectedItem.Image;

                blocks[0].Text = $"Name: {SelectedItem.Name}";
                blocks[1].Text = $"Rarity: {rarity}";
                blocks[2].Text = $"Type: {type}";
                blocks[3].Text = $"Value: {value}";
                blocks[4].Text = $"Count: {count}";
                blocks[5].Text = $"Description: {SelectedItem.Desc}";
            }
        }

        public void SelectedGrid(BindingList<Recipe> List, DataGrid grid, Image image, List<TextBlock> blocks)
        {
            if (grid.SelectedItem != null && grid.Items.Count != 0)
            {
                Recipe SelectedRecipe = grid.SelectedItem as Recipe;

                Item ResultItem = SelectedRecipe.Result;

                string rarity = ResultItem.Rarity.ToString();
                string type = ResultItem.Type.ToString();

                string value = ResultItem.Value.ToString();
                string count = ResultItem.Count.ToString();

                image.Source = ResultItem.Image;

                blocks[0].Text = $"Name: {ResultItem.Name}";
                blocks[1].Text = $"Rarity: {rarity}";
                blocks[2].Text = $"Type: {type}";
                blocks[3].Text = $"Value: {value}";
                blocks[4].Text = $"Count: {count}";
                blocks[5].Text = $"Description: {ResultItem.Desc}";
            }
        }
    }
}
