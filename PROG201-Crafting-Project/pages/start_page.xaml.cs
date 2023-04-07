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

            grd_Menu.Visibility = Visibility.Hidden;
        }

        private void Set_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Game.Player.Name=input_Name.Text;

            SetRandomSeed(Convert.ToDouble(input_Seed.Text));

            MessageBox.Show
            (
                "Name Set To:\n\r" +
                "------------------\n\r" +
                $"Name: {MainWindow.Game.Player.Name}\n\r" +
                $"Seed #: {input_Seed.Text}\n\r" +
                "------------------"
            );

            btn_Set.Visibility = Visibility.Hidden;
            grd_Set.Visibility = Visibility.Hidden;
            grd_Menu.Visibility = Visibility.Visible;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UINav.UpdatePage("menu");
        }
    }
}
