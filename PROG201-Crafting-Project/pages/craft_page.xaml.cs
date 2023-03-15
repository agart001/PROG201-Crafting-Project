using System;
using System.Collections.Generic;
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
        Craft Crafter = new Craft();
        public craft_page()
        {
            InitializeComponent();

            RefreshTextBlocks();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Crafter.CraftItem(MainWindow.Game.Player.Inventory, 0);
            RefreshTextBlocks();
        }

        void RefreshTextBlocks()
        {
            MainWindow.UINav.InventoryInfo(MainWindow.Game.Player, tb_inventory);
            MainWindow.UINav.RecipeInfo(Crafter.CheckRecipes(MainWindow.Game.Player.Inventory), tb_recipe);
        }
    }
}
