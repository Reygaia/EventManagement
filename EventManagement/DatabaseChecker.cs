using MongoDB.Driver;
using System.Diagnostics;

namespace EventManagement
{
    public class DatabaseChecker
    {
        private readonly IConfiguration _configuration;

        public DatabaseChecker(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CheckConnection(string databaseName)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("MongoDB");
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);

                // Try accessing a collection or performing a small operation
                var collectionNames = database.ListCollectionNames().ToList();
                Debug.WriteLine($"Connection to database '{databaseName}' established successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to connect to database '{databaseName}'. Error: {ex.Message}");
                throw;
            }
        }
    }
}
