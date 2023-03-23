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
        List<TextBlock> GridTextBlocks;

        public craft_page()
        {
            InitializeComponent();

            GridTextBlocks = new List<TextBlock>
            {
                tb_Name,
                tb_Rarity,
                tb_Type,
                tb_Value,
                tb_Count,
                tb_Desc
            };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.SetGridSource(dtgrd_Recipe, MainWindow.Game.PlayerRecipes);
            grd_Recipe.Visibility = Visibility.Hidden;
            btn_Craft.Visibility = Visibility.Hidden;
        }

        private void dtgrd_Recipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grd_Recipe.Visibility = Visibility.Visible;
            MainWindow.UINav.SelectedData(MainWindow.Game.PlayerRecipes, dtgrd_Recipe, img_Recipe, GridTextBlocks);

            btn_Craft.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.UpdatePage("start");
        }

        private void Craft_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.Crafter.CraftItem(MainWindow.Game.Player.Inventory, dtgrd_Recipe.SelectedIndex);

            MainWindow.Game.BindPlayerRecipes();
            MainWindow.UINav.SetGridSource(dtgrd_Recipe, MainWindow.Game.PlayerRecipes);
            dtgrd_Recipe.SelectedIndex = -1;

            grd_Recipe.Visibility = Visibility.Hidden;
            btn_Craft.Visibility = Visibility.Hidden;
        }
    }
}
