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
        protected readonly ApplicationDbContext _context;    
        protected readonly IMapper _mapper;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context, IMapper mapper){
            _context = context;
            _dbSet = _context.Set<T>();   
            _mapper = mapper;
        }
 
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

    
        // public List<T> GetAll()
        // {
        //      return  _context.Set<T>().ToList();   
        // }

        public List<Dto> GetAll()
        {
            var entities = _context.Set<T>().ToList();
            var dtos = _mapper.Map<List<Dto>>(entities);
            return dtos;
        }


        public List<T> Find(Expression<Func<T, bool>> match){

            IQueryable<T> query = _context.Set<T>();
        
            return query.Where(match).ToList();

        }

    
        // make only admins adding new books       pending 
        public Dto Add(Dto dto)
        {
          
            var entity = _mapper.Map<T>(dto);
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            
            return _mapper.Map<Dto>(entity);
        }

   public T Add(T t)
        {
          
            var entity = _mapper.Map<T>(t);
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            
            return _mapper.Map<T>(entity);
        }
        
        public void Delete(T t)
        {
         
            _context.Set<T>().Remove(t);
            _context.SaveChanges();
           
        }


        public void Update(Dto dto)
        {
            var entity = _mapper.Map<T>(dto);

            _context.Set<T>().Update(entity);
          
            _context.SaveChanges();
        }

        public TResult GetSpecificValue<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return _context.Set<T>().Where(filter).Select(selector).FirstOrDefault();
        }


         public bool UpdateSpecificField<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector, Action<T,TResult> updateAction)
        {
            // find entity 
            var entity = _context.Set<T>().Where(filter).FirstOrDefault();
            
            if (entity == null)
            {
                return false;
            }

            var property = selector.Compile()(entity);
            if (property == null)
            {
                return false;
            }

           
            updateAction(entity, property);
            _context.SaveChanges();
            return true;
        }

      
    }
}