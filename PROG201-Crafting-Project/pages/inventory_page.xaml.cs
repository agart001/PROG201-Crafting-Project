﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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

        List<TextBlock> BannerTextBlocks;

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

            BannerTextBlocks = new List<TextBlock>
            {
                tb_C_Name,
                tb_XP,
                tb_Gold
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.UpdatePage("start");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.SetBannerSource(MainWindow.Game.Player, BannerTextBlocks);
            MainWindow.UINav.SetGridSource(dtgrd_Inventory, MainWindow.Game.PlayerInventory);
            grd_Item.Visibility = Visibility.Hidden;
        }

        private void dtgrd_Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grd_Item.Visibility = Visibility.Visible;
            MainWindow.UINav.SelectedData(MainWindow.Game.PlayerInventory, dtgrd_Inventory, img_Item, GridTextBlocks);
        }
    }
}
