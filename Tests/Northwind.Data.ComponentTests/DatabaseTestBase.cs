using System.Data.SqlClient;
using System.Transactions;
using NUnit.Framework;

namespace Northwind.Data.ComponentTests
{
    // TODO: should be changed to use local database created by/for tests
    [TestFixture]
    public class DatabaseTestBase
    {
        private SqlConnection _sqlConnection;
        private TransactionScope _transactionScopeToRunTestsIn;

        public SqlConnection SqlConnection
        {
            get { return _sqlConnection; }
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _transactionScopeToRunTestsIn = new TransactionScope();

            _sqlConnection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=Northwind;User Id=northwind;Password=northwind123;");
            _sqlConnection.Open();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _sqlConnection.Close();

            _transactionScopeToRunTestsIn.Dispose(); // Should roll back everything inside transaction scope
        }



    }
}