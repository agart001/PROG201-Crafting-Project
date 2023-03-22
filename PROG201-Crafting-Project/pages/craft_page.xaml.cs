using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG201_Crafting_Project.pages
{
    /// <summary>
    /// Interaction logic for craft_page.xaml
    /// </summary>
    public partial class craft_page : Page
    {
        void SetGridSource() => dtgrd_Recipe.ItemsSource = MainWindow.Game.PlayerRecipes;

        void SelectedGridRecipe()
        {
            if (dtgrd_Recipe.SelectedItem != null && dtgrd_Recipe.Items.Count != 0)
            {
                Recipe SelectedRecipe = dtgrd_Recipe.SelectedItem as Recipe;

                Item ResultItem = SelectedRecipe.Result;

                string val = ResultItem.Value.ToString();

                tb_Name.Text = ResultItem.Name;
                tb_Value.Text = val;
                tb_Desc.Text = ResultItem.Desc;
            }
            else
            {
                tb_Name.Text = "default";
                tb_Value.Text = "default";
                tb_Desc.Text = "default";
            }
        }

        public craft_page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetGridSource();
            SelectedGridRecipe();
        }

        private void dtgrd_Recipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedGridRecipe();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.UpdatePage("start");
        }

        private void Craft_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.Crafter.CraftItem(MainWindow.Game.Player.Inventory, dtgrd_Recipe.SelectedIndex);

            MainWindow.Game.BindPlayerRecipes();


            SetGridSource();
            SelectedGridRecipe();
        }
    }
}
