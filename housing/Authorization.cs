using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace housing
{
    public class Authorization
    {
        public static int logUser {  get; set; }
        public int LogCheck(string logText, string pswText)
        {
            logUser = 0;
            if ((logText == "Редактор") && (pswText == "1313"))
            {
                logUser = 2;
            }
            return logUser;
        }
    }
}
