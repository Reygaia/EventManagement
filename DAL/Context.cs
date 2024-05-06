using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DAL
{
    public class Context
    {
        private IMongoDatabase _database;

        public Context(string connectString, string databaseName)
        {
            var client = new MongoClient(connectString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) 
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
