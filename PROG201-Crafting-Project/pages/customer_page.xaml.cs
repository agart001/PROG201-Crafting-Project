using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for customer_page.xaml
    /// </summary>
    public partial class customer_page : Page
    {
        List<TextBlock> GridTextBlocks;
        List<TextBlock> BannerTextBlocks;

        Character Customer = MainWindow.Game.NPCs[1];

        BindingList<Item> CustomerInventory;

        public customer_page()
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

            BannerTextBlocks = new List<TextBlock>
            {
                tb_C_Name,
                tb_XP,
                tb_Gold
            };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CustomerInventory = MainWindow.UINav.BindList(Customer.Inventory);

            MainWindow.UINav.SetBannerSource(MainWindow.Game.Player, BannerTextBlocks);

            MainWindow.UINav.SetGridSource(dtgrd_P_Inventory, MainWindow.Game.PlayerInventory);
            MainWindow.UINav.SetGridSource(dtgrd_C_Inventory, CustomerInventory);

            grd_Sell.Visibility = Visibility.Hidden;
        }

        private void dtgrd_P_Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.UINav.SelectedData(MainWindow.Game.PlayerInventory, dtgrd_P_Inventory, img_Item, GridTextBlocks);
            grd_Sell.Visibility = Visibility.Visible;
        }

        private void btn_Sell_Click(object sender, RoutedEventArgs e)
        {
            Item item = dtgrd_P_Inventory.SelectedItem as Item;
            int amount = Convert.ToInt32(input_Sell.Text);
            MainWindow.Game.Vendor.BuyItem(Customer, MainWindow.Game.Player, item, amount);

            MainWindow.Game.PlayerInventory = MainWindow.UINav.BindList(MainWindow.Game.Player.Inventory);
            CustomerInventory = MainWindow.UINav.BindList(Customer.Inventory);

            MainWindow.UINav.SetGridSource(dtgrd_P_Inventory, MainWindow.Game.PlayerInventory);
            MainWindow.UINav.SetGridSource(dtgrd_C_Inventory, CustomerInventory);

            MainWindow.UINav.SetBannerSource(MainWindow.Game.Player, BannerTextBlocks);

            dtgrd_P_Inventory.SelectedIndex = -1;

            grd_Sell.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.UpdatePage("start");
        }
    }
}
