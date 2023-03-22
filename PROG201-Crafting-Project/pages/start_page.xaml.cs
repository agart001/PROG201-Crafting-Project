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
    /// Interaction logic for start_page.xaml
    /// </summary>
    public partial class start_page : Page
    {
        public start_page()
        {
            InitializeComponent();
        }

        private void Craft_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.BindPlayerRecipes();
            MainWindow.UINav.UpdatePage("craft");
        }

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.BindPlayerInventory();
            MainWindow.UINav.UpdatePage("inventory");
        }  
    }
}
