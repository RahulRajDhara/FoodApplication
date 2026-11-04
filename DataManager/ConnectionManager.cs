using Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace DataManager
{
    public static class ConnectionManager
    {
        private static string _connectionString = string.Empty;

        // Static constructor to initialize the connection string and logger
        static ConnectionManager()
        {
            _connectionString = GlobalSetting.ConnectionString;
        }

        public static string ConnectionString
        {
            get { return _connectionString; }
        }

        /// <summary>
        /// Establish Connection to the database and Return an open connection.
        /// </summary>
        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("An error occurred while establishing a connection.", ex);
            }
            return conn;
        }

        /// <summary>
        /// Close the connection if it is open or broken.
        /// </summary>
        public static void CloseConnection(SqlConnection conn)
        {
            if (conn != null && (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken))
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }

    // Custom exception for database connection errors
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
