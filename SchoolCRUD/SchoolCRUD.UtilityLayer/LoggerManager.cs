using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolCRUD.UtilityLayer
{
    public class LogManager
    {
        public static void DecideLogInput(Exception ex)
        {
            if (bool.Parse(ConfigurationManager.AppSettings["logInFile"]))
            {
                LoggerFile.AddData(ex);
            }

            if (bool.Parse(ConfigurationManager.AppSettings["logInDB"]))
            {
                LoggerTable.AddData(ex);
            }
        }
    }
}
