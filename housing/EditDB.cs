using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace housing
{
    public class EditDB
    {
        public int housingNum { get; set; }
        public bool housingAdd { get; set; }

        public MainWindow mainWnd;

        public void ChangeDBRow()
        {
            try
            {
                if (mainWnd.editedRow.housingAdd == true)
                {
                    using (MySqlConnection conn = new MySqlConnection(DataAccess.connStr))
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO housingdata (surname, adress, area) VALUES (@surname, @adress, @area)", conn))
                    {
                        cmd.Parameters.AddWithValue("@surname", mainWnd.editedHousing.surname);
                        cmd.Parameters.AddWithValue("@adress", mainWnd.editedHousing.adress);
                        cmd.Parameters.AddWithValue("@area", mainWnd.editedHousing.area);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (MySqlConnection conn = new MySqlConnection(DataAccess.connStr))
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE housingdata SET surname = @surname, adress = @adress, area = @area WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", mainWnd.editedHousing.id);
                        cmd.Parameters.AddWithValue("@surname", mainWnd.editedHousing.surname);
                        cmd.Parameters.AddWithValue("@adress", mainWnd.editedHousing.adress);
                        cmd.Parameters.AddWithValue("@area", mainWnd.editedHousing.area);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка з'єднання з БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteDBRow()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DataAccess.connStr))
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM housingdata WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", mainWnd.editedHousing.id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка видалення з БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
