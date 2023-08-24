using System.Linq.Expressions;
using Entities.Abstract;

namespace DataAccess.Abstract;
//  generic constraint
// class : reference type
// IEntity : can be of type IEntity or a class which implements IEntity
// new() : should be able to instantiate it
public interface IEntityRepository<T> where T : class, IEntity, new()
{
    List<T> GetAll(Expression<Func<T, bool>>? filter = null);
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}