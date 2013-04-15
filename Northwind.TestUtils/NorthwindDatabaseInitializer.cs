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
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                using (Server server = new Server(new ServerConnection(connection)))
                {
                    
                    using (var sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();

                        CreateDatabase(sqlConnection);

                        string databaseCreationSqlScript;
                        using (var fileStream = new FileStream("Schemas/NorthwindSchema.sql", FileMode.Open))
                        {
                            using (var sr = new StreamReader(fileStream))
                            {
                                databaseCreationSqlScript = sr.ReadToEnd();
                            }
                        }

                        var cleanCreationScript = CleanScript(databaseCreationSqlScript);

                        sqlConnection.Execute(cleanCreationScript);
                    }
            }
        }

        private static string CleanScript(string databaseCreationSqlScript)
        {
            var scriptWithoutGo = databaseCreationSqlScript.Replace("\r\nGO", ";\r\n");
            var scriptWithoutSetAnsiNull = scriptWithoutGo.Replace("SET ANSI_NULLS ON", "\r\n");
            var scriptWithoutEverything = scriptWithoutSetAnsiNull.Replace("SET QUOTED_IDENTIFIER ON",
                                                                           "\r\n");
            return scriptWithoutEverything;
        }

        private void CreateDatabase(SqlConnection sqlConnection)
        {
            const string createSql = @"
                USE [master]
                DROP DATABASE [NORTHWIND]
                CREATE DATABASE [NORTHWIND]
                ALTER DATABASE [NORTHWIND] 
                    SET COMPATIBILITY_LEVEL = 90";

            sqlConnection.Execute(createSql);
        }
    }
}
