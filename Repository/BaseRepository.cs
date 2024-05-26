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
        // why we need Dbcontext
        // take instance of Dbcontext to manage database operation
        protected readonly ApplicationDbContext _context;
        // using mapper to ta link between 
        protected readonly IMapper _mapper;

        // Dbset : class represents a collection for a given entity within the context.
        protected readonly DbSet<T> _dbSet;



        // adding constructor
        public BaseRepository(ApplicationDbContext context, IMapper mapper){
            _context = context;
            _dbSet = _context.Set<T>();   
            _mapper = mapper;
        }
 


        // add logic to find any data from database  
        // take parameter with datatype int 
        public T? GetById(int id)
        {
            var data = _context.Set<T>().Find(id);
               return data;
        }

        // to find any data using id 
        //take parameter with datatype byte like category 
        public T? GetById(byte id)
        {
            var data = _context.Set<T>().Find(id);
               return data;
        }

        
        // to dispaly all data from database without Id
        public IEnumerable<T> GetAll()
        {
             return  _context.Set<T>().ToList();   
        }


        // to find data with include the same value
        // using include --> to add values from another related entites 
        public IEnumerable<T> Find(Expression<Func<T, bool>> match, string[] includes = null){

            IQueryable<T> query = _context.Set<T>();
            if(includes != null )
                 foreach (var include in includes)
                       query = query.Include(include);

            return query.Where(match).ToList();

        }

        // to add new data in database 
        // make only admins adding new books       pending 
       public Dto Add(Dto dto)
        {
          
            var entity = _mapper.Map<T>(dto);
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            
             return _mapper.Map<Dto>(entity);
        }

        
        public void Delete(T t)
        {
         
            _context.Set<T>().Remove(t);
            _context.SaveChanges();
           
        }


        public void Update(Dto dto)
        {
            var entity = _mapper.Map<T>(dto);

            // Attach the entity to the context 
            _context.Set<T>().Attach(entity);

            // Save changes to the database
            _context.SaveChanges();
        }

         // method to retrieve a specific value from the database
        public TResult GetSpecificValue<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return _context.Set<T>().Where(filter).Select(selector).FirstOrDefault();
        }

        // method to update specific field of entity 
        // depended on specific query 
         public bool UpdateSpecificField<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector, Action<T,TResult> updateAction)
        {
            // find entity 
            var entity = _context.Set<T>().Where(filter).FirstOrDefault();
            
            if (entity == null)
            {
                return false;
            }

            //??
            //selector.Compile() converts the expression tree into a delegate (callable function).
            //selector.Compile()(entity) calls this function with the entity to get the property value.

            var property = selector.Compile()(entity);
            if (property == null)
            {
                return false;
            }

            // to set value and save changes in database
            updateAction(entity, property);
            _context.SaveChanges();
            return true;
        }
    

        public IEnumerable<Dto> AddRange(IEnumerable<Dto> dto)
        {
            throw new NotImplementedException();
        }



     

    }
}



/*
why use Context and sets?
_context : necessary to manage  lifecycle of database connection and tracking changes in entities

Set<TEntity>
 - dynamically access the entity sets without needing to define a property for each entity in context class.
 -  working with the correct entity type and provides compile-time checking (useful to catch errors early in development process).
================================

why using IEnumerable?
  - Using IEnumerable<T>
1-Hides specific collection implementations (allows the caller to iterate over the collection without needing to know the specific type of collection)
2-Queries are not executed until the collection is iterated over, optimizing performance and allowing dynamic query building. 
3- Ensures consumers cannot modify the collection (because it provides read-only access to the collection).


IEnumarable VS IQueryable
IEnumerable<T> is ideal for in-memory collections with LINQ queries executed in memory.
IQueryable<T> is designed for querying data from external sources, with queries translated into the native query language of the data source and executed remotely.

====================================


*/