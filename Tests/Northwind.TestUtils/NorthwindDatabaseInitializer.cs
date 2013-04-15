using System.Data;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace Northwind.TestUtils
{
    public class NorthwindDatabaseInitializer
    {
        public void CreateNorthwindFromScript(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            { 
                connection.Open();

                CreateDatabase(connection);
                
                string databaseSchemaCreationSqlScript;
                using (var fileStream = new FileStream("Schemas/NorthwindSchema.sql", FileMode.Open))
                {
                    using (var sr = new StreamReader(fileStream))
                    {
                        databaseSchemaCreationSqlScript = sr.ReadToEnd();
                    }
                }

                var serverConnection = new ServerConnection(connection);
                var server = new Server(serverConnection);
                server.ConnectionContext.ExecuteNonQuery(databaseSchemaCreationSqlScript);
                server.ConnectionContext.Disconnect();
            }
        }

        private void CreateDatabase(IDbConnection connection)
        {
            const string createSql = @"
                IF EXISTS (SELECT name FROM master.sys.databases WHERE name = N'NORTHWIND')
                    DROP DATABASE [NORTHWIND];

                USE [master]
                CREATE DATABASE [NORTHWIND]
                ALTER DATABASE [NORTHWIND] 
                    SET COMPATIBILITY_LEVEL = 90";

            connection.Execute(createSql);
        }

        public void CleanupNorthwind(string connectionString)
        {
            SqlConnection.ClearAllPools(); //  Must do this or DROP DATABASE will fail

            const string cleanupSql = @"
                USE [master]
                DROP DATABASE [NORTHWIND]";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                connection.Execute(cleanupSql);
            }
        }
    }
}
