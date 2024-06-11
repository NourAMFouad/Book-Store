using System.ComponentModel;
using System.Linq.Expressions;
using Book_store_1_.DTOs;
using Book_store_1_.Models;

namespace Book_store_1_.Repository{
 
public interface IBaseRepository<T, Dto> where T : class where Dto : class
{
 
    T? GetById(int id);
    T? GetById(byte id);

    List<Dto> GetAll();

    // to adding pagination concept in getall function 
    IEnumerable<T> GetAllInPages(PagesParameter pagesParameter);

    
    List<T> Find(Expression<Func<T,bool>> match);
    
    Dto Add(Dto dto);
    T Add(T t);

    void Update(Dto dto);

    void Delete(T t);

    TResult GetSpecificValue<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector);

    bool UpdateSpecificField<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector, Action<T, TResult> updateAction);
   
    }
}