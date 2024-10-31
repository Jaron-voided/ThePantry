using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;

namespace ThePantry.Data
{
    //public static class DB
    public class DB
    {
        //private static readonly string _connectionString;
        private readonly string _connectionString;
        
        //static DB()
        public DB(string connectionString)
        {
            /*var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Ensure the appsettings.json is in the correct directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DevelopmentDatabase");*/
            _connectionString = connectionString;
        }
        
        //public static string ConnectionString => _connectionString;
        public string ConnectionString => _connectionString;
        //private static SqlConnection CreateConnection()
        private SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        //internal static void CreateTable(string createTableQuery)
        internal void CreateTable(string createTableQuery)
        {
            try
            {
                Console.WriteLine("Creating table...");
                using var connection = CreateConnection();
                using var createTableCommand = new SqlCommand(createTableQuery, connection);
                //createTableCommand.ExecuteNonQuery();
                var result = createTableCommand.ExecuteNonQuery();
                Console.WriteLine($"Creating table result: {result}");
            }
            //catch (SqlException ex)
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine($"Failed to create table: {ex.Message}");
                throw new Exception("Failed to create table: " + ex.Message, ex);
            }
        }

//        internal static void ExecuteNonQuery(string commandText, SqlParameter[] parameters)
        internal void ExecuteNonQuery(string commandText, SqlParameter[] parameters)
        {
            try
            {
                using var connection = CreateConnection();
                using var command = new SqlCommand(commandText, connection);
                // Only add parameters if they are provided
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                // Log or handle exception
                throw new Exception("Failed to execute non-query: " + ex.Message, ex);
            }
        }

        //internal static T ExecuteScalar<T>(string commandText, SqlParameter[] parameters)
        internal T ExecuteScalar<T>(string commandText, SqlParameter[] parameters)
        {
            try
            {
                using var connection = CreateConnection();
                using var command = new SqlCommand(commandText, connection);
                command.Parameters.AddRange(parameters ?? throw new ArgumentNullException(nameof(parameters)));
                return (T)command.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                // Log or handle exception
                throw new Exception("Failed to execute scalar command: " + ex.Message, ex);
            }
        }

        //internal static SqlDataReader ExecuteReader(string commandText, SqlParameter[] parameters)
        internal SqlDataReader ExecuteReader(string commandText, SqlParameter[] parameters)
        {
            try
            {
                var connection = CreateConnection();
                var command = new SqlCommand(commandText, connection);
                command.Parameters.AddRange(parameters ?? throw new ArgumentNullException(nameof(parameters)));

                // The connection will close when the reader is closed
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                // Log or handle exception
                throw new Exception("Failed to execute reader: " + ex.Message, ex);
            }
        }
    }
}
