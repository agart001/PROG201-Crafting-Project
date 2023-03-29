using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        public void UpdatePage(string _page)
        {
            string page = "pages/" + _page + "_page.xaml";
            Uri uri = new Uri(page, UriKind.Relative);
            main.Source = uri;
        }

        public void SetGridSource(DataGrid grid, BindingList<Item> List) => grid.ItemsSource = List;

        public void SetGridSource(DataGrid grid, BindingList<Recipe> List) => grid.ItemsSource = List;

        public void SetBannerSource(Character character, List<TextBlock> blocks)
        {
            blocks[0].Text = $"Name: {character.Name}";
            blocks[1].Text = $"XP: {character.XP}";
            blocks[2].Text = $"Gold: {character.Gold}";
        }

        void SelectedGrid(DataGrid grid, DataGrid subgrid)
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

        void SelectedData(BindingList<Item> List, DataGrid grid, Image image, List<TextBlock> blocks)
        {
            if (grid.SelectedItem != null && grid.Items.Count != 0)
            {
                Item SelectedItem = grid.SelectedItem as Item;

                GridItem(SelectedItem, image, blocks);
            }
        }

        void SelectedData(BindingList<Recipe> List, DataGrid grid, Image image, List<TextBlock> blocks)
        {
            if (grid.SelectedItem != null && grid.Items.Count != 0)
            {
                Recipe SelectedRecipe = grid.SelectedItem as Recipe;

                Item ResultItem = SelectedRecipe.Result;

                GridItem(ResultItem, image, blocks);
            }
        }


        public void SelectionChanged(Character character, DataGrid datagrid, Grid grid, Image img, List<TextBlock> textblocks)
        {
            SelectedData(character.GetBoundInventory(), datagrid, img, textblocks);
            grid.Visibility = Visibility.Visible;
        }

        public void SelectionChanged(Character character, DataGrid datagrid, DataGrid subgrid, Grid grid, Image img, List<TextBlock> textblocks)
        {
            SelectedData(character.GetBoundRecipes(), datagrid, img, textblocks);
            if (datagrid.SelectedIndex != -1) SelectedGrid(datagrid, subgrid);

            grid.Visibility = Visibility.Visible;
        }
    }
}
