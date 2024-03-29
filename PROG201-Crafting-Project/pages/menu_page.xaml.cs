﻿using System;
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
    /// Interaction logic for menu_page.xaml
    /// </summary>
    public partial class menu_page : Page
    {
        public menu_page()
        {
            InitializeComponent();
        }

        private void Craft_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.Player.SetBoundRecipes(MainWindow.Game.Crafter);
            MainWindow.UINav.UpdatePage("craft");
        }

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.Player.SetBoundInventory();
            MainWindow.UINav.UpdatePage("inventory");
        }

        private void Supply_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.Player.SetBoundInventory();
            MainWindow.UINav.UpdatePage("supply");
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.Player.SetBoundInventory();
            MainWindow.UINav.UpdatePage("customer");
        }
    }
}
