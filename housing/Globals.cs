using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Input;

namespace housing
{
    public class Housing
    {
        public Housing(int idNum, string sn, string ad, int ar)
        {
            this.id = idNum;
            this.surname = sn;
            this.adress = ad;
            this.area = ar;
        }
        public int id { get; set; }
        public string surname { get; set; }
        public string adress { get; set; }
        public int area { get; set; }
    }
}