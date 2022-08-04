using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    public interface IRepository<in TKey, T> where T : class
    {
        T GetById(TKey id);

        List<T> Get();

        void Create(T entity);

        void SaveChanges();

        bool Exists(Expression<Func<T, bool>> expression);
    }
}
