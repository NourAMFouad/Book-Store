using Book_store_1_.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_store_1_.Controllers;

public class ApplicationDbContext : DbContext
{
     // constructor 
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
     {

     }

     // adding property 
     public DbSet<Category> Category {set; get;}
     public DbSet<Admin> Admin {set; get;}
     public DbSet<Member> Member {set; get;}
     public DbSet<Book> Book {set; get;}
     public DbSet<Borrowed_books> Borrowed_Books {set; get;}


}

