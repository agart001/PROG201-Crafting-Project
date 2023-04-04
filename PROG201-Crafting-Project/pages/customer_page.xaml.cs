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

        List<TextBlock> NPCBanner;

        List<TextBlock> PlayerBanner;

        Character Trader = MainWindow.Game.NPCs[1];

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

            PlayerBanner = new List<TextBlock>
            {
                tb_P_Name,
                tb_P_XP,
                tb_P_Gold
            };

            NPCBanner = new List<TextBlock>
            {
                tb_NPC_Name,
                tb_NPC_XP,
                tb_NPC_Gold
            };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.Vendor.StoreLoaded
                (
                    MainWindow.UINav,
                    Trader,
                    MainWindow.Game.Player,
                    dtgrd_NPC_Inventory,
                    dtgrd_P_Inventory,
                    NPCBanner,
                    PlayerBanner,
                    grd_Item
                );
        }

        private void dtgrd_P_Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.UINav.SelectionChanged
                (
                    MainWindow.Game.Player,
                    dtgrd_P_Inventory,
                    grd_Item,
                    img_Item,
                    GridTextBlocks
                );
        }

        private void btn_TXN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.Vendor.StoreClick
                (
                    MainWindow.UINav,
                    Trader,
                    MainWindow.Game.Player,
                    dtgrd_NPC_Inventory,
                    dtgrd_P_Inventory,
                    NPCBanner,
                    PlayerBanner,
                    grd_Item,
                    ip_Amount
                );
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.UpdatePage("start");
        }
    }
}
