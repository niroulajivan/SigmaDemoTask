using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaCandidateManagement.Business.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;
            
        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CandidateDb");
        }

        public void CreateDatabaseAndTable()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Step 1: Check if the database exists
                var dbName = new SqlConnectionStringBuilder(_connectionString).InitialCatalog;
                var checkDbCommand = new SqlCommand($"SELECT database_id FROM sys.databases WHERE Name = '{dbName}'", connection);

                var dbExists = checkDbCommand.ExecuteScalar() != DBNull.Value;

                if (!dbExists)
                {
                    // Step 2: Create the database if it doesn't exist
                    var createDbCommand = new SqlCommand($"CREATE DATABASE {dbName}", connection);
                    createDbCommand.ExecuteNonQuery();
                }

                // Step 3: Ensure the table exists
                var tableCheckCommand = new SqlCommand($@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Candidates')
                BEGIN
                    CREATE TABLE Candidates (
                        FirstName NVARCHAR(100),
                        LastName NVARCHAR(100),
                        PhoneNumber NVARCHAR(15),
                        Email NVARCHAR(100) PRIMARY KEY,
                        CallTimeInterval NVARCHAR(50),
                        LinkedInProfile NVARCHAR(200),
                        GitHubProfile NVARCHAR(200),
                        Comments NVARCHAR(500)
                    )
                END", connection);

                tableCheckCommand.ExecuteNonQuery();
            }
        }
    }
}
