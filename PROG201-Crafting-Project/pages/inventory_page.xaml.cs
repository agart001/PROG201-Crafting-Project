using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for inventory_page.xaml
    /// </summary>
    public partial class inventory_page : Page
    {
        List<TextBlock> GridTextBlocks;

        public inventory_page()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.UpdatePage("start");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.SetGridSource(dtgrd_Inventory, MainWindow.Game.PlayerInventory);

            MainWindow.UINav.ToggleVis(grd_Item);
        }

        private void dtgrd_Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.UINav.SelectedGrid(MainWindow.Game.PlayerInventory, dtgrd_Inventory, grd_Item, GridTextBlocks);
        }

        private void dtgrd_Inventory_Selected(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.ToggleVis(grd_Item);
        }
    }
}
