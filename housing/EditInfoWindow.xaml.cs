using Google.Protobuf.WellKnownTypes;
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
using System.Windows.Shapes;

namespace housing
{
    /// <summary>
    /// Логика взаимодействия для EditInfoWindow.xaml
    /// </summary>
    public partial class EditInfoWindow : Window
    {
        public EditInfoWindow()
        {
            InitializeComponent();
        }

        public MainWindow mainWnd;
        public EditDB editDB;

        private void EditInfoForm_Activated(object sender, EventArgs e)
        {
            if (editDB.housingAdd == true)
            {
                idLabel.Visibility = Visibility.Hidden;
                idTextBox.Visibility = Visibility.Hidden;
                deleteButton.Visibility = Visibility.Hidden;
                saveButton.Content = "Додати";
                this.Title = "Додати";
            }
            else
            {
                idLabel.Visibility = Visibility.Visible;
                idTextBox.Visibility = Visibility.Visible;
                deleteButton.Visibility = Visibility.Visible;
                saveButton.Content = "Зберегти";
                this.Title = "Редагувати";
            }
        }

        private void ChangeHousingListData()
        {
            try
            {
                mainWnd.editedHousing.surname = surnameTextBox.Text;
                mainWnd.editedHousing.adress = adressTextBox.Text;
                mainWnd.editedHousing.area = Convert.ToInt32(areaTextBox.Text);
                mainWnd.editedHousing.id = Convert.ToInt32(idTextBox.Text);

                mainWnd.HousingListDG.Items.Refresh();
            }
            catch (FormatException)
            {
                MessageBox.Show("Будь ласка, введіть коректне число у поле площі.", "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddHousingListData()
        {
            try
            {
                int newId = 1;
                if (mainWnd.dataConnection.fList.Count > 0)
                {
                    newId = mainWnd.dataConnection.fList.Max(h => h.id) + 1;
                }

                string newSurname = surnameTextBox.Text;
                string newAdress = adressTextBox.Text;
                int newArea = Convert.ToInt32(areaTextBox.Text);

                mainWnd.editedHousing = new Housing(newId, newSurname, newAdress, newArea);

                mainWnd.dataConnection.fList.Add(mainWnd.editedHousing);
                mainWnd.HousingListDG.Items.Refresh();
            }
            catch (FormatException)
            {
                MessageBox.Show("Будь ласка, введіть коректне число у поле площі.", "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (editDB.housingAdd == true)
            {
                AddHousingListData();
                mainWnd.editedRow.ChangeDBRow();
            }
            else
            {
                if (idTextBox.Text != "")
                {
                    ChangeHousingListData();
                    mainWnd.editedRow.ChangeDBRow();
                }
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Ви дійсно хочете видалити цей запис?", "Підтвердження видалення", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                mainWnd.editedRow.DeleteDBRow();
                mainWnd.dataConnection.fList.Remove(mainWnd.editedHousing);
                mainWnd.HousingListDG.Items.Refresh();
                this.Hide();
            }
        }

        private void EditInfoForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
