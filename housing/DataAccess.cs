using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace housing
{
    public class DataAccess
    {
        public static string connStr;

        public List<Housing> fList = new List<Housing>(85);

        private void OpenDBFile()
        {
            connStr = "Server=localhost; Database=housing; Port=3306; User=user; Password=13131313;";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = new MySqlCommand();
            string commandString = "SELECT * FROM housingdata;";
            command.CommandText = commandString;
            command.Connection = conn;
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                fList.Add(new Housing((int)reader["id"], (string)reader["surname"], (string)reader["adress"], (int)reader["area"]));
                i++;
            }
            reader.Close();
        }
        public DataAccess()
        {
            OpenDBFile();
        }
    }
}
