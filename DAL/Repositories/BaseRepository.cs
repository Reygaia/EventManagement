using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public BaseRepository(Context context, string collectionName)
        {
            _collection = context.GetCollection<T>(collectionName);
        }

        public void Delete(object id)
        {
            _collection.DeleteOne(Builders<T>.Filter.Eq("_id", id));
        }
        public void DeleteByEntity(T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", GetIdValue(entity));
            _collection.DeleteOne(filter);
        }

        public List<T> Get(Expression<Func<T, bool>> expression)
        {
            return _collection.Find(expression).ToList();
        }

        public List<T> GetAll()
        {
            return _collection.Find(new BsonDocument()).ToList();
        }

        public void Insert(T entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id",GetIdValue(entity));
            _collection.ReplaceOne(filter, entity);
        }

        private ObjectId GetIdValue(T entity)
        {
            var propertyInfo = typeof(T).GetProperty("Id");
            return (ObjectId)propertyInfo.GetValue(entity, null);
        }
    }
}
