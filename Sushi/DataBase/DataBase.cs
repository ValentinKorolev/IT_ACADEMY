using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.DataBase
{
    public static class DataBase
    {
        static SqlConnection connection = new SqlConnection(@"Data Source = DESKTOP-22958VE\SQLEXPRESS;Initial Catalog=SushiMarcet;Integrated Security=True;TrustServerCertificate=true");  

        public static void OpenningConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public static void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public static SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
