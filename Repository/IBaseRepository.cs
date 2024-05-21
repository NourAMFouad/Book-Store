using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Book_store_1_.Repository{

// IBaseRepository<T> where T :class
// T --> class name 
public interface IBaseRepository<T> where T :class
{
    
    // To get specific data by ID 
    T GetById(int id);

    // to get all data from database
    IEnumerable<T> GetAll();

    // return data from database without select id 
    T Find(Expression<Func<T,bool>> match);
    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);

}
}