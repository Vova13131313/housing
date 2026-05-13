using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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

namespace housing
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static Authorization loggedUser = new Authorization();
        DataAccess dataConnection = new DataAccess();

        private void InfoHousingForm_Loaded(object sender, RoutedEventArgs e)
        {
            HousingMenuItem.Visibility = Visibility.Hidden;
            HousingMenuItem.Width = 0;
        }
        private void InfoHousingForm_Activated(object sender, EventArgs e)
        {
            if (Authorization.logUser == 2)
            {
                HousingMenuItem.Visibility = Visibility.Visible;
                HousingMenuItem.Width = 50;
            }
            else
            {
                HousingMenuItem.Visibility = Visibility.Hidden;
                HousingMenuItem.Width = 0;
            }
        }

        private void FileMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SaveDataMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            HousingListDG.ItemsSource = dataConnection.fList;
        }

        private void HousingMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AuthMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LogInFormWindow logWnd = new LogInFormWindow();
            logWnd.Show();
        }
    }
}
