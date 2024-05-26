using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Book_store_1_.Repository{

// IBaseRepository<T> where T :class
// T --> class name 
public interface IBaseRepository<T, Dto> where T : class where Dto : class
{
    // To get specific data by ID 
    T? GetById(int id);
    T? GetById(byte id);

    // to get all data from database
    IEnumerable<T> GetAll();

    // return data from database without select id (by expression)
    IEnumerable<T> Find(Expression<Func<T,bool>> match, string[] includes = null);
    
    Dto Add(Dto dto);

  
    void Update(Dto dto);

    void Delete(T t);

    TResult GetSpecificValue<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector);

    bool UpdateSpecificField<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector, Action<T, TResult> updateAction);

}
}