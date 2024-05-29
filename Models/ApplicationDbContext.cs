using Book_store_1_.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;




namespace Book_store_1_.Controllers;

public class ApplicationDbContext : IdentityDbContext<Admin, ApplicationRole, string>
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
}

