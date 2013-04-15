using System.Data.SqlClient;
using System.Transactions;
using NUnit.Framework;

namespace Northwind.TestUtils
{
    // TODO: should be changed to use local database created by/for tests
    [TestFixture]
    public class DatabaseTestBase
    {
        private const string ConnectionString = @"Data Source=(localdb)\v11.0;Integrated Security=true";

        private SqlConnection _sqlConnection;
        private TransactionScope _transactionScope;

        public SqlConnection SqlConnection
        {
            get { return _sqlConnection; }
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var northwindDatabaseInitializer = new NorthwindDatabaseInitializer();
            northwindDatabaseInitializer.CreateNorthwindFromScript(ConnectionString);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            
        }

        [SetUp]
        public void SetUp()
        {
            _transactionScope = new TransactionScope();

            _sqlConnection = new SqlConnection(ConnectionString);
            _sqlConnection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            _sqlConnection.Close();

            _transactionScope.Dispose();
        }

    }
}