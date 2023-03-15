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
        public static UI UINav;
        public static Game Game;
        public MainWindow()
        {
            InitializeComponent();

            UINav = new UI(Main);
            Game = new Game();

            UINav.UpdatePage("start");
        }

    }
}
