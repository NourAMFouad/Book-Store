using System.Linq.Expressions;
using AutoMapper;
using Book_store_1_.Controllers;
using Book_store_1_.DTOs;
using Book_store_1_.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Book_store_1_.Repository
{
    public class BaseRepository<T, Dto> : IBaseRepository<T, Dto> where T : class where Dto : class
    {
        // using DbContext
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<T> _dbSet;



        // adding constructor
        public BaseRepository(ApplicationDbContext context, IMapper mapper){
            _context = context;
            _dbSet = _context.Set<T>();   
            _mapper = mapper;

        }
 


        // add logic to find any data from database 
        public T? GetById(int id)
        {
            var data = _context.Set<T>().Find(id);
               return data;
        }

        public T? GetById(byte id)
        {
            var data = _context.Set<T>().Find(id);
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

        //
        public T Find(Expression<Func<T, bool>> match, string[] includes = null){

            IQueryable<T> query = _context.Set<T>();
            if(includes != null )
                 foreach (var include in includes)
                       query = query.Include(include);

            return query.SingleOrDefault(match);

        }

        public IEnumerable<T> FindAll(string[] includes = null)
        {
            return _context.Set<T>().ToList();
        }
        // to find any data by categoryId 
        // to add new data in database 
      
       public Dto Add(Dto dto)
        {
            var entity = _mapper.Map<T>(dto);
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            
             return _mapper.Map<Dto>(entity);
        }

        // T IBaseRepository<T, Dto>.Add(Dto dto)
        // {
        //     throw new NotImplementedException();
        // }

        public IEnumerable<Dto> AddRange(IEnumerable<Dto> dto)
        {
            throw new NotImplementedException();
        }

        public void Update(Dto dto)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public void Delete(byte Id)
        {
            throw new NotImplementedException();
        }

        /*
          var entity = _mapper.Map<T>(dto);
          _context.Set<T>().Add(entity);
          await _context.SaveChangesAsync();
          return _mapper.Map<TDto>(entity); 
        */


    }
}


