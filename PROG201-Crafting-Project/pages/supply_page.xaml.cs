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
    /// Interaction logic for supply_page.xaml
    /// </summary>
    public partial class supply_page : Page
    {
        List<TextBlock> GridTextBlocks;

        Character Trader = MainWindow.Game.NPCs[0];

        BindingList<Item> TraderInventory;

        public supply_page()
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
            TraderInventory = MainWindow.UINav.BindList(Trader.Inventory);

            MainWindow.UINav.SetGridSource(dtgrd_P_Inventory, MainWindow.Game.PlayerInventory);
            MainWindow.UINav.SetGridSource(dtgrd_S_Inventory, TraderInventory);
            grd_Buy.Visibility = Visibility.Hidden;
        }

        private void dtgrd_S_Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.UINav.SelectedData(TraderInventory, dtgrd_S_Inventory, img_Item, GridTextBlocks);

            grd_Buy.Visibility = Visibility.Visible;
        }

        private void btn_Buy_Click(object sender, RoutedEventArgs e)
        {
            Item item = dtgrd_S_Inventory.SelectedItem as Item;
            int amount = Convert.ToInt32(input_Buy.Text);
            MainWindow.Game.Vendor.BuyItem(MainWindow.Game.Player, Trader, item, amount);

            MainWindow.Game.PlayerInventory = MainWindow.UINav.BindList(MainWindow.Game.Player.Inventory);
            TraderInventory = MainWindow.UINav.BindList(Trader.Inventory);

            MainWindow.UINav.SetGridSource(dtgrd_P_Inventory, MainWindow.Game.PlayerInventory);
            MainWindow.UINav.SetGridSource(dtgrd_S_Inventory, TraderInventory);

            dtgrd_S_Inventory.SelectedIndex = -1;

            grd_Buy.Visibility = Visibility.Hidden;
        }
    }
}
