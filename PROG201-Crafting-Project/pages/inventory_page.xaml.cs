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
    /// Interaction logic for inventory_page.xaml
    /// </summary>
    public partial class inventory_page : Page
    {
        public inventory_page()
        { 
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.Player.Inventory.RemoveAt(0);
            MainWindow.UINav.UpdatePage("start");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dtgrd_Inventory.ItemsSource = MainWindow.Game.PlayerInventory;
        }
    }
}
