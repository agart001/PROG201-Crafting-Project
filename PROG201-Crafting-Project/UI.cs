using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        public BindingList<Item> BindList(List<Item> List)
        {
            return new BindingList<Item>(List);
        }

        public BindingList<Recipe> BindList(List<Recipe> List)
        {
            return new BindingList<Recipe>(List);
        }


        public void SetGridSource(DataGrid grid, BindingList<Item> List) => grid.ItemsSource = List;

        public void SetGridSource(DataGrid grid, BindingList<Recipe> List) => grid.ItemsSource = List;

        public void SelectedGrid(DataGrid grid, DataGrid subgrid)
        {
            if (grid.SelectedItem == null && grid.Items.Count == 0) return;

            Recipe SelectedRecipe = grid.SelectedItem as Recipe;

            List<Item> ingredients = SelectedRecipe.Ingredients;

            subgrid.ItemsSource = ingredients;
        }

        void GridItem(Item item, Image image, List<TextBlock> blocks)
        {
            string rarity = item.Rarity.ToString();
            string type = item.Type.ToString();

            string value = item.Value.ToString();
            string count = item.Count.ToString();

            image.Source = item.Image;

            blocks[0].Text = $"Name: {item.Name}";
            blocks[1].Text = $"{rarity}";
            blocks[2].Text = $"{type}";
            blocks[3].Text = $"Value: {value}";
            blocks[4].Text = $"Count: {count}";
            blocks[5].Text = $"Description: {item.Desc}";
        }

        public void SelectedData(BindingList<Item> List, DataGrid grid, Image image, List<TextBlock> blocks)
        {
            if (grid.SelectedItem != null && grid.Items.Count != 0)
            {
                Item SelectedItem = grid.SelectedItem as Item;

                GridItem(SelectedItem, image, blocks);
            }
        }

        public void SelectedData(BindingList<Recipe> List, DataGrid grid, Image image, List<TextBlock> blocks)
        {
            if (grid.SelectedItem != null && grid.Items.Count != 0)
            {
                Recipe SelectedRecipe = grid.SelectedItem as Recipe;

                Item ResultItem = SelectedRecipe.Result;

                GridItem(ResultItem, image, blocks);
            }
        }
    }
}
