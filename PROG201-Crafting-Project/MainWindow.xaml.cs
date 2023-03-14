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
using static PROG201_Crafting_Project.Utility;

namespace PROG201_Crafting_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UI UINav;
        public MainWindow()
        {
            InitializeComponent();
            UINav = new UI(Main);
            UINav.update_page("start");

            Character player = new Character("Alex", 1000, 0, "items", "player");
            Craft crafter = new Craft();

            crafter.CraftItem(player.Inventory, 0);
            int b = 5;
        }

    }
}
