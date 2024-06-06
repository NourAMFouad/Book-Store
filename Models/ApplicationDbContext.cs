using Book_store_1_.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;





namespace Book_store_1_.Controllers;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//public class ApplicationDbContext : DbContext
{
     // constructor 
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
     { 

     }


     public DbSet<ApplicationRole> AspNetRoles { get; set; }

     // adding property 
     public DbSet<Category> Category {set; get;}
     public DbSet<Admin> Admin {set; get;}
     public DbSet<Member> Member {set; get;}
     public DbSet<Book> Book {set; get;}
     public DbSet<BorrowedBooks> BorrowedBooks {set; get;}

    internal object Set<T, Dto>()
    {
        throw new NotImplementedException();
    }
}

