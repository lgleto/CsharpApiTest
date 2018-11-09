using System;
using MySql.Data.MySqlClient;
using System.Security.Permissions;

namespace iDomApiTest
{
    public class DBConnection
    {
        private DBConnection()
        {
        }


        public string Password { get; set; }
        
        private MySqlConnection connection = null;
        
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {

               // string connstring = "Server=localhost; database=employees; UID=root; password=12345678" ;
                
                string connstring =
                    "Server=localhost;" +
                    "Database=emploies_mgr;" +
                    "User ID=root;" +
                    "Password=12345678;";
                connection = new MySqlConnection(connstring);
                connection.Open();
           
            return true;
        }

        public void Close()
        {
            
            connection.Close();
            connection = null;
        }        
    }
}