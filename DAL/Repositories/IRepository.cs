using System.Linq.Expressions;

namespace DAL.Repositories
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        void DeleteByEntity(T entity);
        List<T> GetAll();
        List<T> Get(Expression<Func<T,bool>> expression);
    }
}
