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
    public partial class SelectWindow : Window
    {
        public SelectWindow()
        {
            InitializeComponent();
        }

        public MainWindow mainWnd;
        public char housingSel { get; set; }

        private void selButton_Click(object sender, RoutedEventArgs e)
        {
            if (housingSel == 'X')
            {
                string searchText = selTextBox.Text.ToLower();
                var filteredList = mainWnd.dataConnection.fList.Where(h => h.surname.ToLower().Contains(searchText)).ToList();

                mainWnd.HousingListDG.ItemsSource = filteredList;
            }
            else if (housingSel == 'Y')
            {
                try
                {
                    int searchArea = Convert.ToInt32(selTextBox.Text);
                    var filteredList = mainWnd.dataConnection.fList.Where(h => h.area >= searchArea).ToList();

                    mainWnd.HousingListDG.ItemsSource = filteredList;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Введіть число", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Непередбачена помилка", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void selCancelButton_Click(object sender, RoutedEventArgs e)
        {
            mainWnd.HousingListDG.ItemsSource = mainWnd.dataConnection.fList;
            mainWnd.HousingListDG.Items.Refresh();

            selTextBox.Text = "";
        }

        private void SelectForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
