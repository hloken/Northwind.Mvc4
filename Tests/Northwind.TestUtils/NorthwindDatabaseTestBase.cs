using System.Data.SqlClient;
using System.Transactions;
using NUnit.Framework;

namespace Northwind.TestUtils
{
    [TestFixture]
    public abstract class NorthwindDatabaseTestBase
    {
        private const string ConnectionString = @"Data Source=(localdb)\v11.0;Integrated Security=true";
        private const string ConnectionStringWithDatabase = ConnectionString + ";Initial Catalog=NORTHWIND";

        private NorthwindDatabaseInitializer _northwindDatabaseInitializer;

        private SqlConnection _sqlConnection;
        private TransactionScope _transactionScope;

        public SqlConnection SqlConnection
        {
            get { return _sqlConnection; }
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _northwindDatabaseInitializer = new NorthwindDatabaseInitializer();
            _northwindDatabaseInitializer.CreateNorthwindFromScript(ConnectionString);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _northwindDatabaseInitializer.CleanupNorthwind(ConnectionString);
        }

        [SetUp]
        public void SetUp()
        {
            _transactionScope = new TransactionScope();

            _sqlConnection = new SqlConnection(ConnectionStringWithDatabase);
            _sqlConnection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            _sqlConnection.Close();
            _sqlConnection.Dispose();

            _transactionScope.Dispose();
        }

    }
}