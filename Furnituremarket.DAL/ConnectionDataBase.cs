using MySql.Data.MySqlClient;

namespace Furnituremarket.DAL
{
    public class ConnectionDataBase
    {
        public class ConnDataBase
        {
            static volatile ConnDataBase Class;
            static object SyncObject = new object();
            public static ConnDataBase GetInstance
            {
                get
                {
                    if (Class == null)
                        lock (SyncObject)
                        {
                            if (Class == null)
                                Class = new ConnDataBase(ConnectionString);
                        }
                    return Class;
                }
            }

            private static string ConnectionString { get; set; }

            public ConnDataBase(string connectionString)
            {
                ConnectionString = connectionString;
            }

            readonly MySqlConnection connection = new MySqlConnection(ConnectionString);

            public MySqlConnection GetConnection()
            {
                return connection;
            }

            public void OpenConnection()
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
            }

            public void CloseConnection()
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
