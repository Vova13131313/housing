using Microsoft.SqlServer.Server;
using Microsoft.Win32;
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
using Xceed.Words.NET;

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
            selWnd.mainWnd = this;
        }

        public static Authorization loggedUser = new Authorization();
        public DataAccess dataConnection = new DataAccess();
        public Housing editedHousing;
        public EditDB editedRow = new EditDB();

        EditInfoWindow editWnd = new EditInfoWindow();
        LogInFormWindow logWnd = new LogInFormWindow();
        SelectWindow selWnd = new SelectWindow();

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
            var currentData = HousingListDG.ItemsSource as IEnumerable<Housing>;

            if (currentData == null || !currentData.Any())
            {
                MessageBox.Show("Немає даних для збереження", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<Housing> listToExport = currentData.ToList();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word Document (*.docx)|*.docx";
            saveFileDialog.Title = "Зберегти таблицю як...";
            saveFileDialog.FileName = "";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var document = DocX.Create(saveFileDialog.FileName))
                    {
                        document.InsertParagraph("Звіт по житловому фонду")
                            .FontSize(16)
                            .Bold()
                            .Alignment = Xceed.Document.NET.Alignment.center;

                        document.InsertParagraph("");

                        var table = document.InsertTable(listToExport.Count + 1, 4);
                        table.Design = Xceed.Document.NET.TableDesign.TableGrid;

                        table.Rows[0].Cells[0].Paragraphs.First().Append("ID").Bold();
                        table.Rows[0].Cells[1].Paragraphs.First().Append("Прізвище").Bold();
                        table.Rows[0].Cells[2].Paragraphs.First().Append("Адреса").Bold();
                        table.Rows[0].Cells[3].Paragraphs.First().Append("Площа").Bold();

                        for (int i = 0; i < listToExport.Count; i++)
                        {
                            var item = listToExport[i];
                            int rowIndex = i + 1;

                            table.Rows[rowIndex].Cells[0].Paragraphs.First().Append(item.id.ToString());
                            table.Rows[rowIndex].Cells[1].Paragraphs.First().Append(item.surname);
                            table.Rows[rowIndex].Cells[2].Paragraphs.First().Append(item.adress);
                            table.Rows[rowIndex].Cells[3].Paragraphs.First().Append(item.area.ToString());
                        }

                        document.Save();
                    }

                    MessageBox.Show("Дані успішно збережено у файл Word", "Експорт", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при збереженні файлу: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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

        private void SelectXMenuItem_Click(object sender, RoutedEventArgs e)
        {
            selWnd.housingSel = 'X';

            selWnd.selLabel.Content = "Прізвище";
            selWnd.selTextBox.Text = "";

            selWnd.Show();
        }

        private void SelectYMenuItem_Click(object sender, RoutedEventArgs e)
        {
            selWnd.housingSel = 'Y';

            selWnd.selLabel.Content = "Площа     >";
            selWnd.selTextBox.Text = "";

            selWnd.Show();
        }
    }
}
