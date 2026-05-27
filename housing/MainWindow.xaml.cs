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
            editWnd.mainWnd = this;
            editWnd.editDB = this.editedRow;
            editedRow.mainWnd = this;
        }

        public static Authorization loggedUser = new Authorization();
        public DataAccess dataConnection = new DataAccess();
        public Housing editedHousing;
        public EditDB editedRow = new EditDB();

        EditInfoWindow editWnd = new EditInfoWindow();
        LogInFormWindow logWnd = new LogInFormWindow();

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
        private void EditDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            editedRow.housingAdd = false;

            editWnd.idTextBox.Text = "";
            editWnd.surnameTextBox.Text = "";
            editWnd.adressTextBox.Text = "";
            editWnd.areaTextBox.Text = "";

            editWnd.Show();
        }
        private void AddDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            editedRow.housingAdd = true;

            editedRow.housingNum = dataConnection.fList.Count;
            if (editedRow.housingNum >= 85)
            {
                editedRow.housingAdd = false;
                MessageBox.Show("Кількість записів перевищує ліміт, у вікні редагування видаліть зайві записи.", "Перевищено ліміт", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            editWnd.idTextBox.Text = "";
            editWnd.surnameTextBox.Text = "";
            editWnd.adressTextBox.Text = "";
            editWnd.areaTextBox.Text = "";

            editWnd.Show();
        }
        private void HousingListDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editedHousing = HousingListDG.SelectedItem as Housing;
            if (editedHousing == null) return;

            try
            {
                editWnd.surnameTextBox.Text = editedHousing.surname;
                editWnd.adressTextBox.Text = editedHousing.adress;
                editWnd.areaTextBox.Text = editedHousing.area.ToString();
                editWnd.idTextBox.Text = editedHousing.id.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AuthMenuItem_Click(object sender, RoutedEventArgs e)
        {
            logWnd.Show();
        }

        private void InfoHousingForm_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
