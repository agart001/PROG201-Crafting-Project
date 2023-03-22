using System;
using System.Collections.Generic;
using System.Data;
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
            GridSelectedItem();
        }

        private void dtgrd_Inventory_Selected(object sender, RoutedEventArgs e)
        {
            /*
            DataGrid dtgrd = sender as DataGrid;
            Item rowItem = dtgrd.SelectedItem as Item;

            string _val = rowItem.Value.ToString();

            tb_Name.Text = rowItem.Name;
            tb_Value.Text = _val;
            tb_Desc.Text = rowItem.Desc;
            */
        }

        private void dtgrd_Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridSelectedItem();
        }

        void GridSelectedItem()
        {
            if (dtgrd_Inventory.SelectedItem != null && dtgrd_Inventory.Items.Count > 0)
            {
                Item SelectedItem = dtgrd_Inventory.SelectedItem as Item;

                string val = SelectedItem.Value.ToString();

                tb_Name.Text = SelectedItem.Name;
                tb_Value.Text = val;
                tb_Desc.Text = SelectedItem.Desc;
            }
        }
    }
}
