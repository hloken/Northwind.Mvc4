using System.Data;

namespace Northwind.TestUtils.DatabaseSeeders
{
    public class DatabaseSeeder
    {
        public static void SeedDatabase(IDbConnection dbConnection)
        {
            CustomerSeeder.SeedCustomers(dbConnection);
            OrderSeeder.SeedOrdersForCustomers(dbConnection);
        }
    }
}