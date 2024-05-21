using System.Linq.Expressions;
using Book_store_1_.Controllers;
using Book_store_1_.DTOs;
using Book_store_1_.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Book_store_1_.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        // using DbContext
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;



        // adding constructor
        public BaseRepository(ApplicationDbContext context){
            _context = context;
            _dbSet = _context.Set<T>();   
        }
 


        // add logic to find any data from database 
        public T GetById(int id)
        {
            var data = _context.Set<T>().Find(id) ;
               return data;
        }

        // to dispaly all data from database 
        public IEnumerable<T> GetAll()
        {
             return  _context.Set<T>().ToList();   
        }

        // to get data depend on specific experision without using id 
        public T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }

        // to find any data by categoryId 
        // to add new data in database 
        public void Add(T entity)
        {
           _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        
        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }


    }
}